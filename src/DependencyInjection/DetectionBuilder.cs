// Copyright (c) 2014-2021 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper functions for configuring detection services.
/// </summary>
public class DetectionBuilder : IDetectionBuilder
{
    /// <summary>
    /// Creates a new instance of <see cref="IDetectionBuilder" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
    public DetectionBuilder(IServiceCollection services)
        => Services = services ?? 
                      throw new ArgumentNullException(nameof(services));

    /// <summary>
    /// Gets the <see cref="IServiceCollection" /> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection" /> services are attached to.
    /// </value>
    public IServiceCollection Services { get; }
}