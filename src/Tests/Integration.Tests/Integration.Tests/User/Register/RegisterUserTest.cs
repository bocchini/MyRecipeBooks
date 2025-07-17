using CommonTestUtilities.Requests;
using Integration.Tests.Bases;
using Integration.Tests.InlineData;
using MyRecipeBook.Exceptions;
using Shouldly;
using System.Net.Http.Json;
using System.Text.Json;

namespace Integration.Tests.User.Register;
public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RegisterUserTest(CustomWebApplicationFactory factory) => _client = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await _client.PostAsJsonAsync("User", request);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var nameResponse = responseData.RootElement.GetProperty("name").GetString();

        nameResponse.ShouldSatisfyAllConditions(
            () => nameResponse.ShouldNotBeNullOrEmpty(),
            () => nameResponse.ShouldBe(request.Name)
        );
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        
        if(_client.DefaultRequestHeaders.Contains("Accept-Language"))
            _client.DefaultRequestHeaders.Remove("Accept-Language");

        _client.DefaultRequestHeaders.Add("Accept-Language", culture);

        var response = await _client.PostAsJsonAsync("User", request);
        
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errorMessages = responseData.RootElement.GetProperty("errors").EnumerateArray()
            .Select(e => e.GetString()).ToList();
        var expectedMessage = ResourceMessagesException.ResourceManager
            .GetString("NAME_EMPTY", new System.Globalization.CultureInfo(culture));

        errorMessages.ShouldContain(expectedMessage);
    }
}
