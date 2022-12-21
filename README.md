# Compat

![Compat](Images/Compat.100.png)

Compat - Totally makes compatibility layer for .NET platforms.

[![Project Status: WIP – Initial development is in progress, but there has not yet been a stable, usable release suitable for the public.](https://www.repostatus.org/badges/latest/wip.svg)](https://www.repostatus.org/#wip)

## NuGet

| Package  | NuGet                                                                                                                |
|:---------|:---------------------------------------------------------------------------------------------------------------------|
| Compat | [![NuGet Compat](https://img.shields.io/nuget/v/Compat.svg?style=flat)](https://www.nuget.org/packages/Compat) |

----

## What is this?

* TODO: Still under construction...

Totally makes compatibility layer for .NET platforms.

.NET 7 to older versions of the .NET BCL, such as .NET Framework 3.5 BCL, with as few dependencies as possible.
For example, `net35` does not have a `System.String.IsNullOrWhitespace()` method.
`System.StringEx.IsNullOrWhitespace()` method to provide an alternative for such incompatible code.

In particular, if there is a definition already provided by a third-party library that is a major references to such libraries will occur.
For example, the `Task` and `TaskEx` classes in `net40` use `Microsoft.Bcl.Async` package.

In other words, this library is not a complete backporting library, but aims to provide a better environment for porting.

* Porting should be possible only by recompiling as much as possible.
* If C# or CLR limitations prevent full porting, some modifications may be necessary.

----

## Target Platform

* .NET 7, 6, 5
* .NET Core 3.1, 3.0, 2.2, 2.1, 2.0
* .NET Standard 2.1, 2.0, 1.6, 1.3
* .NET Framework 4.8, 4.6.2, 4.6.1, 4.5.2, 4.5, 4.0, 3.5

----

## Porting status

The list is not completed minor status.

|Members|Status|
|:----|:----|
|`System.ValueTuple`|Added 4 type argument versions.|
|`System.Threading.Tasks.Task`|`WaitAsync` methods.|
|`System.Threading.Tasks.TaskEx`|Ports with `AsyncBridge` and `Microsoft.Bcl.Async`. Added some lack members.|
|`System.Threading.Tasks.ValueTask`|Supported async method builders (async-awaitable)|
|String interpolation features|`FormattedString` and `DefaultInterpolatedStringHandler` types.|
|`System.Linq` operators|`Append`, `Prepend`, `TakeLast`, `SkipLast` and `ToHashSet` methods.|

Lack some `Task` members in earlier third-party library.

* `System.Threading.Tasks.TaskEx.FromException()` is not defined in third-party library.
  * Use `Task.Factory.FromException()` instead.
  * Mostly useful `From{...}` type method. So defined `Task.Factory.FromResult()` in near place.
  * Mostly useful `CompletedTask` property. So defined `Task.Factory.CompletedTask()` method in near place.

----

## Tips

### Build .NET Framework assembly on Linux or others

When you want to build .NET Framework project in Linux and other non-Windows environment,
that package supports ability for referencing of .NET Framework assemblies:

```xml
<ItemGroup>
  <PackageReference
    Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.3"
    PrivateAssets="All" />
</ItemGroup>
```

----

## Backports are welcome

Backport PRs are welcome. The following is our backporting policy:

* Basically, target under the `System` namespace of the BCL library.
* If a class, structure, or other type does not exist in the first place, define a new one.
* Use extended methods when porting instance methods.
* When porting instance properties or static members, define a type with `Ex` at the end and define members there.
  * This is to conform to existing conventions, such as `TaskEx` for `Task`.
* If you refer to a third-party library, there must be no alternative to this library or it must be well known.
  * Example: [AsyncBridge package](https://www.nuget.org/packages/AsyncBridge), which was a community-based pre-distribution in TPL.
  * Example: [Microsoft.Bcl.Async package](https://www.nuget.org/packages/Microsoft.Bcl.Async), which provides backward compatibility of `Task` with .NET Framework 4.0.
* When referencing third-party libraries, do not generate unnecessary references.
  * For example, `net6.0` is nearly up-to-date, and references to libraries should not be included when using `net6.0`. To achieve this, define the `PackageReference` to be excluded by `Condition`.
* Increase the TFM if it is to fill an incompatibility with a TFM that is not currently included.
* Do not include non-backporting any extensions.
* Write some kind of unit test. Of course, a comprehensive and exhaustive test code would be perfect.

----

## License

Apache-v2.

----

## History

* 0.1.0:
  * Added `ValueTask`, `ValueTuple` and some minor members.
  * Adjust dependencies.
* 0.0.1:
  * Initial public release.
