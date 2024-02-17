# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.1.0] / 2024-02-17
### Features
- Support net core plugin.
### Added
- Add `CodeAnalysisCodeDom` to support net code.
- Update `CodeAnalysisCodeDomService` to work with multiple source code.
### Tests
- Test `GistGithubUtils` download string.
- Add `Newtonsoft.Json` to support `GistGithubUtils` download string.

## [1.0.6] / 2024-01-27
### Features
- Using `ricaun.Revit.UI.Tasks`
### Tests
- Add `CodeDom` simple test.
### Updated
- Add `ICodeDomService` interface
- Add `CodeDomFactory` class
### Remove
- Remove `Revit.Async`

## [1.0.5] / 2023-05-05
### Features
- Support C# version 7.3 in Revit 2021+ with `DotNetCompilerPlatform`.
- Gist Download Files and compile.
- Support `CodeDomService` with Defines - `Revit20$$` and `REVIT20$$`.
### Updated
- Update `InfoCenterUtils` to show download update.

## [1.0.4] / 2023-02-03
### Updated
- Update example `Command` to `Revit Version`

## [1.0.3] / 2023-02-02
### Updated
- Remove Version in the `Release` folder

## [1.0.2] / 2023-02-02
### Fixed
- Fix `GithubRequestService` repository

## [1.0.1] / 2023-02-02
### Added
- Add Debug color Panel Title Background
### Fixed
- Fix Image load problem

## [1.0.0] / 2023-02-02
### Features
- [x] Compile multiple `IExternalCommand` UI
- [x] Code Compiler
- [x] Add Command to Ribbon
- [x] AutoUpdater

[vNext]: ../../compare/1.0.0...HEAD
[1.1.0]: ../../compare/1.0.6...1.1.0
[1.0.6]: ../../compare/1.0.5...1.0.6
[1.0.5]: ../../compare/1.0.4...1.0.5
[1.0.4]: ../../compare/1.0.3...1.0.4
[1.0.3]: ../../compare/1.0.2...1.0.3
[1.0.2]: ../../compare/1.0.1...1.0.2
[1.0.1]: ../../compare/1.0.0...1.0.1
[1.0.0]: ../../compare/1.0.0