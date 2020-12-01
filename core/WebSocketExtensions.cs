using Microsoft.AspNetCore.Builder;

namespace QuizApp.Core.core
{
    public static class WebSocketExtensions
{
    public static IApplicationBuilder UseCustomWebSocketManager(this IApplicationBuilder app)
    {
       return app.UseMiddleware<CustomWebSocketManager>();
    }
}
}