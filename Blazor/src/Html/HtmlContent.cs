﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Web;

internal class HtmlContent : IHtmlContentProvider, IComponent, IDisposable
{
    private HtmlRegistry _registry = default!;

    [Parameter] public string         Name         { get; set; } = default!;
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;

    RenderFragment IHtmlContentProvider.Content => ChildContent;

    public void Attach(RenderHandle renderHandle)
    {
        _registry = HtmlRegistry.GetRegistry(renderHandle);
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        _registry.SetConnect(Name, ChildContent);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (!string.IsNullOrEmpty(Name))
            _registry.SetConnect(Name, null);
    }
}