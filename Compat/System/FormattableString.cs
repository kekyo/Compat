////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if NET20 || NET35 || NET40 || NET45 || NET452

namespace System;

public abstract class FormattableString : IFormattable
{
    protected FormattableString()
    {
    }

    public abstract int ArgumentCount { get; }
    public abstract string Format { get; }

    public abstract object? GetArgument(int index);
    public abstract object?[] GetArguments();

    public override string ToString() =>
        this.ToString(null);

    public abstract string ToString(IFormatProvider? formatProvider);

    string IFormattable.ToString(string? format, IFormatProvider? formatProvider) =>
        this.ToString(formatProvider);
}

#endif
