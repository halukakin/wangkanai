﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Microsoft.AspNetCore.Components;

using Wangkanai.Blazor.Components.Reflection;
using Wangkanai.Blazor.Components.Rendering;
using Wangkanai.Internal;

namespace Wangkanai.Blazor.Components;

internal readonly struct CascadingParameterState
{
    private static readonly ConcurrentDictionary<Type, ReflectedCascadingParameterInfo[]> _cachedInfos = new();

    public string LocalValueName { get; }
    public ICascadingValueComponent ValueSupplier { get; }

    public CascadingParameterState(string localValueName, ICascadingValueComponent valueSupplier)
    {
        LocalValueName = localValueName;
        ValueSupplier = valueSupplier;
    }

    public static IReadOnlyList<CascadingParameterState> FindCascadingParameters(ComponentState componentState)
    {
        var componentType = componentState.Component.GetType();
        var infos = GetReflectedCascadingParameterInfos(componentType);

        // For components known not to have any cascading parameters, bail out early
        if (infos.Length == 0)
        {
            return Array.Empty<CascadingParameterState>();
        }

        // Now try to find matches for each of the cascading parameters
        // Defer instantiation of the result list until we know there's at least one
        List<CascadingParameterState>? resultStates = null;

        var numInfos = infos.Length;
        for (var infoIndex = 0; infoIndex < numInfos; infoIndex++)
        {
            ref var info = ref infos[infoIndex];
            var supplier = GetMatchingCascadingValueSupplier(info, componentState);
            if (supplier != null)
            {
                if (resultStates == null)
                {
                    // Although not all parameters might be matched, we know the maximum number
                    resultStates = new List<CascadingParameterState>(infos.Length - infoIndex);
                }

                resultStates.Add(new CascadingParameterState(info.ConsumerValueName, supplier));
            }
        }

        return resultStates ?? (IReadOnlyList<CascadingParameterState>)Array.Empty<CascadingParameterState>();
    }

    private static ICascadingValueComponent? GetMatchingCascadingValueSupplier(in ReflectedCascadingParameterInfo info, ComponentState componentState)
    {
        do
        {
            if (componentState.Component is ICascadingValueComponent candidateSupplier
                && candidateSupplier.CanSupplyValue(info.ValueType, info.SupplierValueName))
            {
                return candidateSupplier;
            }

            componentState = componentState.ParentComponentState;
        } while (componentState != null);

        // No match
        return null;
    }

    private static ReflectedCascadingParameterInfo[] GetReflectedCascadingParameterInfos(
        [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
    {
        if (!_cachedInfos.TryGetValue(componentType, out var infos))
        {
            infos = CreateReflectedCascadingParameterInfos(componentType);
            _cachedInfos[componentType] = infos;
        }

        return infos;
    }

    private static ReflectedCascadingParameterInfo[] CreateReflectedCascadingParameterInfos(
        [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
    {
        List<ReflectedCascadingParameterInfo>? result = null;
        var candidateProps = ComponentProperties.GetCandidateBindableProperties(componentType);
        foreach (var prop in candidateProps)
        {
            var attribute = prop.GetCustomAttribute<CascadingParameterAttribute>();
            if (attribute != null)
            {
                if (result == null)
                {
                    result = new List<ReflectedCascadingParameterInfo>();
                }

                result.Add(new ReflectedCascadingParameterInfo(
                    prop.Name,
                    prop.PropertyType,
                    attribute.Name));
            }
        }

        return result?.ToArray() ?? Array.Empty<ReflectedCascadingParameterInfo>();
    }

    readonly struct ReflectedCascadingParameterInfo
    {
        public string ConsumerValueName { get; }
        public string? SupplierValueName { get; }
        public Type ValueType { get; }

        public ReflectedCascadingParameterInfo(
            string consumerValueName, Type valueType, string? supplierValueName)
        {
            ConsumerValueName = consumerValueName;
            SupplierValueName = supplierValueName;
            ValueType = valueType;
        }
    }
}
