////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if !NET6_0_OR_GREATER

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
public sealed class InterpolatedStringHandlerArgumentAttribute : Attribute
{
    public readonly string[] ParameterNames;

    public InterpolatedStringHandlerArgumentAttribute(params string[] parameterNames) =>
        this.ParameterNames = parameterNames;
}

#endif
