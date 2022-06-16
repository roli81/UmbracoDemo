A Demo solution for a machine dashboard based on the Umbraco 9 opensource CMS:  https://umbraco.com/

To run locally do the following steps:

1. make sure .NET 5 is installed on your Machine
2. clone the repo into a empty folder
3. setup a empty SQL Database named: 'umb_machineDomain' with a dbo-user named 'machineDomainUser' and pwd 'test'
    (you could set up your own connection string in /UmbracoDemo/appsettings.json)
4. make sure that the value of the "umbracoDbDSN" connection string is empty in /UmbracoDemo/appsettings.json and /UmbracoDemo/appsettings.Development.json
5. startup the webapp
    - cd UmbracoDemo
    - dotnet run
6. follow the screens to perform the Umbraco Installation
7. after installation you will redirected to the backoffice go to Settings -> Usync and perform a full import (content and settings)

Now the demo solution should be up and running.
