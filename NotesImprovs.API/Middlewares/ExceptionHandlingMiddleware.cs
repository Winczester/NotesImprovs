using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NotesImprovs.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerSettings _jsonSettings;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        if (next == null)
            throw new ArgumentNullException(nameof(next));
        _next = next;
        
        _jsonSettings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var errorMessage = $"An exception was thrown.  {_formatExceptionToJsonString(ex)}";

            context.Response.Clear();

            FillResponseOptions(context);

            var json = JsonConvert.SerializeObject(
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    { ReasonPhrase = ex.Message.Replace(Environment.NewLine, "; ") }, _jsonSettings);
            await context.Response.WriteAsync(json);
        }
    }

    private string _formatExceptionToJsonString(Exception ex)
    {
        object o = ex.ToString();
        return JsonConvert.SerializeObject(o);
    }

    private void FillResponseOptions(HttpContext context)
    {
        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json";
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}