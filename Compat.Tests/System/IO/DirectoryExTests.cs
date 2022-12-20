////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace System.IO;

public sealed class DirectoryExTests
{
    [Test]
    public void EnumerateFiles()
    {
        var basePath = Guid.NewGuid().ToString("N");

        Directory.CreateDirectory(basePath);
        try
        {
            var expected = new List<string>();
            for (var index = 0; index < 10; index++)
            {
                var fileName = $"test{index}.txt";

                using var tw = File.CreateText(Path.Combine(basePath, fileName));
                tw.WriteLine("test");
                tw.Flush();

                expected.Add(fileName);
            }
            expected.Sort();

            var actual =
                DirectoryEx.EnumerateFiles(basePath, "*", SearchOption.AllDirectories).
                Select(path => Path.GetFileName(path)).
                OrderBy(fileName => fileName).
                ToArray();

            Assert.AreEqual(expected, actual);
        }
        finally
        {
            Directory.Delete(basePath, true);
        }
    }
}
