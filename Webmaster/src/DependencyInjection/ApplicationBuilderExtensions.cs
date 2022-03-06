﻿using Microsoft.AspNetCore.Builder;

using Wangkanai;
using Wangkanai.Webmaster;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension method for adding the <see cref="WebmasterMiddleware"/> to the application.
/// </summary>
public static class WebmasterApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebmaster(this IApplicationBuilder app)
    {
        Check.NotNull(app);

        app.Validate();

        return app.UseMiddleware<WebmasterMiddleware>();
    }

    private static void Validate(this IApplicationBuilder app)
    {
        // What should I validate?
    }
}