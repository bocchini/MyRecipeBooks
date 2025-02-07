using System.Globalization;

namespace MyRecipeBook.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
            _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("pt_BR");
        if (string.IsNullOrWhiteSpace(requestCulture) == false && supportedLanguages.All(c => c.Equals(requestCulture)))
        {
            cultureInfo = new CultureInfo(requestCulture);
        }

        CultureInfo.CurrentCulture = new CultureInfo(requestCulture);
        CultureInfo.CurrentUICulture = new CultureInfo(requestCulture);

        await _next(context);
    }
}
