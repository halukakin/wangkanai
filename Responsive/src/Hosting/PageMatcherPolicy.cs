// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;

using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Hosting;

internal class ResponsivePageMatcherPolicy : MatcherPolicy, IEndpointComparerPolicy, IEndpointSelectorPolicy
{
    public ResponsivePageMatcherPolicy() => Comparer = EndpointMetadataComparer<IResponsiveMetadata>.Default;

    public IComparer<Endpoint> Comparer { get; }

    public override int Order => 10000;

    public bool AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
    {
        Check.NotNull(endpoints);

        return endpoints.Any(endpoint => endpoint.Metadata.GetMetadata<IResponsiveMetadata>() != null);
    }

    public Task ApplyAsync(HttpContext context, CandidateSet candidates)
    {
        Check.NotNull(context);
        Check.NotNull(candidates);

        var device = context.GetDevice();

        for (var i = 0; i < candidates.Count; i++)
        {
            var endpoint = candidates[i].Endpoint;
            var metadata = endpoint.Metadata.GetMetadata<IResponsiveMetadata>();
            if (metadata is null)
                continue;
            // This endpoint is not a match for the selected device.
            if (metadata?.Device != null && device != metadata.Device)
                candidates.SetValidity(i, false);
        }

        return Task.CompletedTask;
    }
}