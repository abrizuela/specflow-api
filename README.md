# SpecFlow API

Framework to automate API testing using SpecFlow

To start to work in Automation you should add the following extension:

- **Speckflow** integration for visual studio.

## Run the tests from Visual Studio

You can run the tests created from Test Explorer (View -> Test Explorer) right click and select option 'Run'.

## Run the tests from a command line

```bash
.\automation-run.cmd
```

This command will ask you to enter in wich enviroment and tag you want to run.

After the run finishes, the folder containing the report is opened.

## Running the tests from command line

You can run the tests form command line with

```bash
dotnet test SpecFlowAPI.sln
```

### **Choosing environment**

You can target an specific environment file using

```bash
--settings [path to .runsettings file]
```

### **Selecting a tag to run**

Also you can just run the tests under a tag

```bash
--filter Category=[TagName]
```

### **Creating a report**

You can create an html report using

```bash
--logger html
```

This command creates a report in the folder `[path_to_repo_folder]/SpecFlowAPI/TestResults`.

The report name is like `TestResult_[UserName]\_[MachineName]_yyyymmdd_HHmmss`.

### **Example**

Example to run all the ***smoke*** tests in ***dev*** with the ***public*** endpoints and creating a report

```bash
dotnet test SpecFlowAPI.sln --settings SpecFlowAPI\local.runsettings --filter Category=get --logger html
```
