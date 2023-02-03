# RevitAddin.CommandLoader

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

`RevitAddin.CommandLoader` project compiles `IExternalCommand` with Revit open using `CodeDom.Compiler` and creates a `PushButton` on the Revit ribbon.

This project was generated by the [AppLoader](https://ricaun.com/apploader/) Revit plugin.

## Features

* Compile multiple `IExternalCommand` at once with Revit opened.
* Generate `PushButton` with the compiled `IExternalCommand` with `IExternalCommandAvailability`.
* AutoUpdate plugin using [ricaun.Revit.Github](https://github.com/ricaun-io/ricaun.Revit.Github).

## Resources
* [ricaun.Revit.UI](https://github.com/ricaun-io/ricaun.Revit.UI)
* [ricaun.Revit.Mvvm](https://github.com/ricaun-io/ricaun.Revit.Mvvm)
* [ricaun.Revit.Github](https://github.com/ricaun-io/ricaun.Revit.Github)
* [Revit.Async](https://github.com/KennanChan/Revit.Async)

## Installation

* Download and install [RevitAddin.CommandLoader.exe](../../releases/latest/download/RevitAddin.CommandLoader.zip)

## Video

Videos in portuguese with the creation of this project.

[![VideoIma1]][Video1]

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!

[Video1]: https://youtu.be/4oVJWDRhrRs
[VideoIma1]: https://img.youtube.com/vi/4oVJWDRhrRs/mqdefault.jpg
