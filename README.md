
<p align='center'>
  </a>&nbsp;&nbsp;
  <a href="https://www.facebook.com/GrupaNETPg">
    <img src="https://img.shields.io/badge/Facebook-1877F2?style=for-the-badge&logo=facebook&logoColor=white" />        
  </a>&nbsp;&nbsp; 
</p>

# Easy CLI
Sample project with better CLI development experience using `Cocona` library having sample features implemented to show usage examples.  Codebase is commented with simple descriptions of performed actions, remarks about specific types/classes (with links to Microsoft docs) and [documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments) for methods.

## Table of Contents
* [Getting started](#getting-started)
* [Debugging](#debugging)
* [Additional resources](#additional-resources)
* [Alternative libraries](#alternative-libraries)
* [License](#license)

## Getting started
To use application build project using your IDE/.NET CLI and go to 
* `<project directory>/bin/Debug/net6.0` for Debug mode
* `<project directory>/bin/Release/net6.0` for Release mode

Open command line/terminal inside this directory and run
* `./EasyCli` using Linux
* `EasyCli.exe` using Windows Command Prompt/cmd
* `./EasyCli.exe` using Windows PowerShell

For example, executing addition using Windows PowerShell would look like this:
`./EasyCli.exe calc add 1 2`

## Debugging
When developing your app you might want/need to use debugger to inspect what exactly is happening while executing code. While `Cocona` currently doesn't support debugging commands and simply running debugger executes only root command, custom `debug` flag exists in all configured commands. 

For example, debugging addition using Windows PowerShell woud look like this:
`./EasyCli.exe calc add 1 2 --debug`
Application will print message that contains process Id and name to look for when attaching debugger:
`Attach debugger to process with Id 33976 (EasyCli). Press any key to continue...`

To attach debugger using Visual Studio go to *Debug -> Attach to process..., look for EasyCli process in *Available processes* list, click *Attach* and *voilà*, you are debugging your application. Now you can come back to your app, press any key to continue and debugger will pause execution on breakpoints!


### Remove debug flag in Release mode
To allow using debug flag only in debug mode you can edit commands to look like example below:
```
...
commandsBuilder.AddCommand("add", (
    [Argument(Name = "Augend", Description = "Augend")]
    double x,
    [Argument(Name = "Addend", Description = "Addend")]
    double y
#if DEBUG
    ,bool? debug
#endif
    ) =>
{
#if DEBUG
    AllowDebugIfSelected(debug);
#endif
...
```
This is called conditional compilation. With these preprocessor directives, ``bool? debug`` parameter and ``AllowDebugIfSelected(debug);`` method call will be compiled **ONLY** when compilation is set to **Debug** mode. Otherwise they will be ignored by compiler.
For more information about preprocessor directives and conditional compilation see [Microsoft docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives).

---

To change between Debug/Release in Visual Studio open dropdown in the menu on top.
![test](https://user-images.githubusercontent.com/48659621/156812246-e4f30a3d-55d9-4215-8751-de99529ddcac.png)

## Additional resources
* [Cocona GitHub repository](https://github.com/mayuki/Cocona)

## Alternative libraries
* [System.CommandLine](https://github.com/dotnet/command-line-api)
* [Spectre.Console.Cli](https://spectreconsole.net/cli/)
* [CommandLineParser](https://github.com/commandlineparser/commandline)

## License
This repository is licensed under the [MIT](LICENSE) license.

[facebook-shield]: https://img.shields.io/badge/Facebook-1877F2?style=for-the-badge&logo=facebook&logoColor=white
[facebook-url]: https://www.facebook.com/GrupaNETPg