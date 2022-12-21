////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if NET20 || NET35 || NET40 || NET45 || NET452

namespace System.Runtime.CompilerServices;

public static class FormattableStringFactory
{
    public static FormattableString Create(string format, params object?[] arguments) =>
        new DefaultFormattableString(format, arguments);

    private sealed class DefaultFormattableString : FormattableString
    {
        private readonly string format;
        private readonly object?[] arguments;

        public DefaultFormattableString(string format, object?[] arguments)
        {
            this.format = format;
            this.arguments = arguments;
        }

        public override int ArgumentCount =>
            this.arguments.Length;
        public override string Format =>
            this.format;

        public override object? GetArgument(int index) =>
            this.arguments[index];
        public override object?[] GetArguments() =>
            this.arguments;

        public override string ToString(IFormatProvider? formatProvider) =>
            string.Format(formatProvider, this.format, this.arguments);
    }
}

#endif
