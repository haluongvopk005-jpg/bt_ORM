using Microsoft.AspNetCore.Http;

namespace StudentManagementMVC.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // ===== PHẦN 1: LẤY THÔNG TIN REQUEST =====

        var startTime = DateTime.Now;

        var method = context.Request.Method;

        var path = context.Request.Path.ToString();

        // Ghi thời gian bắt đầu request
        Console.WriteLine(
            $"[{startTime:yyyy-MM-dd HH:mm:ss.fff}] " +
            $"Method: {method} - Path: {path}"
        );


        // ===== PHẦN 2: KIỂM TRA ID KHÔNG HỢP LỆ =====

        if (path.Equals("/Students/Details/0",
                       StringComparison.OrdinalIgnoreCase)
            ||
            path.Equals("/Students/Details/-1",
                       StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsync(
                "Student id khong hop le"
            );

            return;
        }


        // ===== PHẦN 3: CHO REQUEST ĐI TIẾP =====

        await _next(context);


        // ===== PHẦN 4: GHI STATUS CODE =====

        Console.WriteLine(
            $"Status Code: {context.Response.StatusCode}"
        );
    }
}