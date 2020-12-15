# Cake.Gradle

[![standard-readme compliant][]][standard-readme]
[![Appveyor build][appveyorimage]][appveyor]
[![Codecov Report][codecovimage]][codecov]
[![NuGet package][nugetimage]][nuget]
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

Aliases to assist with running Gradle builds from Cake build scripts.

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Compatibility](#compatibility)
- [Motivation](#motivation)
- [A Word of caution](#a-word-of-caution)
- [Maintainer](#maintainer)
- [Contributing](#contributing)
  - [Contributors](#contributors)
- [License](#license)

## Install

```cs
#addin nuget:?package=Cake.Gradle
```

## Usage

```cs
 #r "artifacts/build/Cake.Gradle.dll"

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

Developed and tested with Cake 0.15.2

## Motivation

Allow Cake users to orchestrate a complex build including a Gradle-based Java build.
Works similar to the [cake-gulp](https://github.com/Philo/cake-gulp) addin.

## A Word of caution

Cake and Gradle are both task runners. I consider it bad practice to call one task runner out of another. 
It would be better to only have one tool per concern (i.e. task running) - but sometimes this is not feasible.

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
    <td align="center"><a href="https://github.com/abeggchr"><img src="https://avatars1.githubusercontent.com/u/1616011?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Christian Abegg</b></sub></a><br /><a href="https://github.com/cake-contrib/cake-gradle/commits?author=abeggchr" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

## License

[MIT License © Christian Abegg][license]

[all-contributors]: https://github.com/all-contributors/all-contributors
[all-contributorsimage]: https://img.shields.io/github/all-contributors/cake-contrib/Cake-Gradle.svg?color=orange&style=flat-square
[appveyor]: https://ci.appveyor.com/project/cakecontrib/cake-gradle
[appveyorimage]: https://img.shields.io/appveyor/ci/cakecontrib/cake-gradle.svg?logo=appveyor&style=flat-square
[codecov]: https://codecov.io/gh/cake-contrib/Cake-Gradle
[codecovimage]: https://img.shields.io/codecov/c/github/cake-contrib/Cake-Gradle.svg?logo=codecov&style=flat-square
[contrib-covenant]: https://www.contributor-covenant.org/version/1/4/code-of-conduct
[contributing]: CONTRIBUTING.md
[emoji-key]: https://allcontributors.org/docs/en/emoji-key
[maintainer]: https://github.com/nils-a
[nuget]: https://nuget.org/packages/Cake.Gradle
[nugetimage]: https://img.shields.io/nuget/v/Cake.Gradle.svg?logo=nuget&style=flat-square
[license]: LICENSE.txt
[standard-readme]: https://github.com/RichardLitt/standard-readme
[standard-readme compliant]: https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square
