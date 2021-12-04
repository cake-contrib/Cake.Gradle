# Cake.Gradle

[![standard-readme compliant][]][standard-readme]
[![Build][buildimage]][build]
[![Codecov Report][codecovimage]][codecov]
[![NuGet package][nugetimage]][nuget]
[![All Contributors][all-contributors-badge]](#contributors)

Aliases to assist with running Gradle builds from Cake build scripts.

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Compatibility](#compatibility)
- [Motivation](#motivation)
- [A Word of caution](#a-word-of-caution)
- [Discussion](#Discussion)
- [Maintainer](#maintainer)
- [Contributing](#contributing)
  - [Contributors](#contributors)
- [License](#license)
  - [LitJson](#litjson)

## Install

```cs
#addin nuget:?package=Cake.Gradle
```

Additionally, either `gradle` has to be installed on the system or the project needs a gradle-wrapper (`gradlew`)

## Usage

```cs
 #addin nuget:?package=Cake.Gradle

// Run 'gradle --version'
Task("Gradle-Version")
    .Does(() =>
{
    Gradle.WithArguments("--version").Run();
});

// Run 'gradle hello' in a specific folder
// Note: if you have a gradle wrapper setup in the specified path, this one will be used
Task("Gradle-Hello")
    .Does(() =>
{
    Gradle.FromPath("./example").WithTask("hello").Run();
});


// Run 'gradle hello' in a specific folder with default log level
// Note: if no log level is set, it is derived from the Cake verbosity (which is set to 'verbose' in build.ps1)
Task("Gradle-Hello-WithDefaultLogLevel")
    .Does(() =>
{
    Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Default).Run(); 
});

// Run 'gradle --offline --build-file build.gradle hello' in a specific folder
Task("Gradle-Hello-WithArguments")
    .Does(() =>
{
    Gradle.FromPath("./example").WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
});

```

## Compatibility

Cake 0.33.0 and later.

## Motivation

Allow Cake users to orchestrate a complex build including a Gradle-based Java build.
Works similar to the [cake-gulp](https://github.com/Philo/cake-gulp) addin.

## A Word of caution

Cake and Gradle are both task runners. I consider it bad practice to call one task runner out of another. 
It would be better to only have one tool per concern (i.e. task running) - but sometimes this is not feasible.

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Maintainer

[Nils Andresen @nils-a][maintainer]

## Contributing

Cake.Gradle follows the [Contributor Covenant][contrib-covenant] Code of Conduct.

We accept Pull Requests.
Please see [the contributing file][contributing] for how to contribute to Cake.Gradle.

Small note: If editing the Readme, please conform to the [standard-readme][] specification.

This project follows the [all-contributors][] specification. Contributions of any kind welcome!

### Contributors

Thanks goes to these wonderful people ([emoji key][emoji-key]):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://github.com/abeggchr"><img src="https://avatars1.githubusercontent.com/u/1616011?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Christian Abegg</b></sub></a><br /><a href="https://github.com/cake-contrib/cake.gradle/commits?author=abeggchr" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/nils-a"><img src="https://avatars3.githubusercontent.com/u/349188?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Nils Andresen</b></sub></a><br /><a href="https://github.com/cake-contrib/cake.gradle/commits?author=nils-a" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

## License

[MIT License © Christian Abegg, Nils Andresen][license]

Cake.Gradle includes third-party code which is licensed under its own respective license.

### LitJSON

License: Unlicense, https://github.com/LitJSON/litjson/blob/develop/COPYING

[all-contributors]: https://github.com/all-contributors/all-contributors
[all-contributors-badge]: https://img.shields.io/github/all-contributors/cake-contrib/cake.gradle.svg?color=orange&style=flat-square
[build]: https://github.com/cake-contrib/Cake.Gradle/actions/workflows/build.yml
[buildimage]: https://github.com/cake-contrib/Cake.Gradle/actions/workflows/build.yml/badge.svg
[codecov]: https://codecov.io/gh/cake-contrib/cake.gradle
[codecovimage]: https://img.shields.io/codecov/c/github/cake-contrib/cake.gradle.svg?logo=codecov&style=flat-square
[contrib-covenant]: https://www.contributor-covenant.org/version/1/4/code-of-conduct
[contributing]: CONTRIBUTING.md
[emoji-key]: https://allcontributors.org/docs/en/emoji-key
[maintainer]: https://github.com/nils-a
[nuget]: https://nuget.org/packages/Cake.Gradle
[nugetimage]: https://img.shields.io/nuget/v/Cake.Gradle.svg?logo=nuget&style=flat-square
[license]: LICENSE.txt
[standard-readme]: https://github.com/RichardLitt/standard-readme
[standard-readme compliant]: https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square
