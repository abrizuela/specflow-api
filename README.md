# Running the Bill360 Platform Automation
To start to work in Automation you should add the following extension:
- **Speckflow** integration for visual studio.

We use ChromeDriver to run Selenium tests:
- **ChromeDriver** Integrated with a NuGet package (upgrade when there is a new version).

### Refresh Token
The automation has set a refresh_token to access Gmail and retreive email's information (such as link of registration or payment).
That refresh_token is already set in appsetings.json, but in case a new one needs to configured, follow the following steps:

1. Load this URL in the browser: ```https://accounts.google.com/o/oauth2/v2/auth?scope=https://mail.google.com/&include_granted_scopes=true&response_type=code&redirect_uri=urn:ietf:wg:oauth:2.0:oob&client_id={client_id}```.
2. Login with the `testbill360@gmail.com` account and grant all the permissions asked. It will return a **[code]** which will be used in the next step.
3. Make a POST call to the following URL (for example with Postman): ```https://oauth2.googleapis.com/token?code={code}&client_id={clientId}&client_secret={clientSecret}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&grant_type=authorization_code```.
4. Once the json is returned with the `refresh_token` update the appsettings.json file.

The ```{client_id}``` and ```{client_secret}``` variables and the gmail credentials can be found in the appsettings.json file, in the automation project.


## To run the tests:

You can run the ATs created from Test Explorer (View -> Test Explorer) right click and select option 'Run'.

## Another way to run the ATs is:

Simply run the command
```bash
$ .\automation-run.cmd
```
This command will ask you to enter in wich enviroment and tag you want to run.
For simplicity, are created some files with enviroment and tag already predefined
```bash
$ .\automation-run-[environment]-[tag].cmd
```
*Example*
```bash
$ .\automation-run-dev-smoke.cmd
```
After the run finishes, the folder containing the report is opened.

## Running the tests from command line

You can run the tests form command line with
```bash
$ dotnet test Bill360.Automation.sln
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
This command creates a report in the folder `[path_to_repo_folder]/test/Bill360.Automated.Tests/TestResults`.

The report name is like `TestResult_[UserName]\_[MachineName]_yyyymmdd_HHmmss`.

### **Example**
Example to run all the ***smoke*** tests in ***dev*** with the ***public*** endpoints and creating a report
```bash
$ dotnet test Bill360.Automation.sln --settings test\Bill360.Automated.Tests\dev.public.runsettings --filter Category=smoke --logger html
```
## Running the tests in the Pipeline
There is a pipeline for run automation tests under the tag ```smoke``` and it runs every 4 hours in QA and in UAT.

But if you want to run it manually, you can set the environment under wich you want to run them.

Here is  the [Platform - Run Smoke Automation Tests](https://dev.azure.com/bill360/Platform/_build?definitionId=30&_a=summary) pipeline.

To run it manually, hit the button **Run pipeline** at top right corner.
A pop up is opened where you can choose the **Branch** (e.g. develop, uat or any branch of yours).
Also you can set **Variables**, inside this menu you will find the ```target``` variable.

Valid options for ```target``` variable:
- dev.runsettings
- qa.runsettings
- uat.runsettings
- prd.runsettings

Once the variable is set, go back to the ***Run pipeline*** pop up and hit ***Run***

_Note: The default value for ```target``` variable is ```dev.runsettings```_

### Example
If you want to test your branch in the pipeline in UAT, you should select your branch (e.g. ```feature/my-cool-branch```) and then set the Variable ```target = UAT```.