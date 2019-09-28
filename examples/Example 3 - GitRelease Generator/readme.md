# GitReleaseNote Generator

![logo](resources/logo.png)

This dotnet global command line tool has the purpose to generate a release note based on the JIRA ticket placed in the commit. 

The convention in the Bitbucket's Ingenico is TICKET-Comment (e.a. : ENTO-1234 Commit). 

The result is a csv file (Excel openable) generated and sorted on the tag version. 

## How to install

For the following instruction, you have to set up Artifactory as a registred nuget package provider. 
Open a command line an run the following line 
```bash
dotnet tool install --global GitReleaseNote.Generator
```

In case where you can add Artifactory as registred nuget package, you can download from Artifactory the package and where it was downloaded, you can run the following command line : 

```bash
dotnet tool install --global --add-source . GitReleaseNote.Generator
```
## How to unistall

Launch a command line application and run the following command line 

```bash  
dotnet tool uninstall --global GitReleaseNote.Generator
```

## Help 

The command line use the CommandLineParser utility to generate help the user to get a complete and easy help. 

In order to the the help, you can use the following command : 

```bash
release-note --help

GitReleaseNote.Generator 1.0.0
Copyright (C) 2019 GitReleaseNote.Generator

  -v, --verbose       Set output to verbose messages.

  -s, --source        Defines the git directory source. Default value: current directory.

  -b, --branch        Defines the git branch. Default value: master

  -d, --detail        Active detailled view with all commit. Is set false we collapse all JIRA ticket in one row

  -f, --fileOutput    Defines the name of the file output. Default value: output.csv

  -r, --range         Range to list commit from a tag to another one, separated with two dot '..'. Example : 1.0.0..2.0.0.

  --help              Display this help screen.

  --version           Display version information.
```

### Issue possible with Artifactory (local dev mode)

If you are an issue with the nuget can retrieve the package, you should disabled Artifactory and try to install again the global tool. For this you have to have a nuget.exe in the folder where you want to execute the command line or have a nuget.exe referenced in your global Window path.

- Disable Artifactory
```bash
nuget source -Name "Artifactory" disable
```

- Enable Artifactory after the global tool installed.
```bash
nuget source -Name "Artifactory" enable
```