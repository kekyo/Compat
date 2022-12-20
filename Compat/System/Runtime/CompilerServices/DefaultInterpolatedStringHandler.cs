////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if !NET6_0_OR_GREATER

using System.Text;

namespace System.Runtime.CompilerServices;

[InterpolatedStringHandler]
public readonly struct DefaultInterpolatedStringHandler
{
    private readonly StringBuilder sb;
    private readonly IFormatProvider? provider;

    public DefaultInterpolatedStringHandler(
        int literalLength, int formattedCount)
    {
        this.sb = new StringBuilder(literalLength + formattedCount * 10);
    }

    public DefaultInterpolatedStringHandler(
        int literalLength, int formattedCount, IFormatProvider? provider)
    {
        this.sb = new StringBuilder(literalLength + formattedCount * 10);
        this.provider = provider;
    }

    public override string ToString() =>
        this.sb.ToString();

    public string ToStringAndClear()
    {
        var result = this.sb.ToString();
        this.sb.Length = 0;
        return result;
    }

    public void AppendLiteral(string value) =>
        this.sb.Append(value);

    public void AppendFormatted(string? value) =>
        this.sb.Append(value);

    public void AppendFormatted<T>(T value)
    {
        if (value is IFormattable f)
        {
            this.sb.Append(f.ToString(null, this.provider));
        }
        else
        {
            this.sb.Append(value);
        }
    }

    public void AppendFormatted<T>(T value, string? format)
    {
        if (value is IFormattable f)
        {
            this.sb.Append(f.ToString(format, this.provider));
        }
        else
        {
            this.sb.Append(value);
        }
    }
}

#endif
