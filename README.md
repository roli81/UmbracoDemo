A Demo solution for a machine dashboard based on the Umbraco 10 opensource CMS:  https://umbraco.com/

To run locally do the following steps:

1. make sure .NET 6 is installed on your Machine
2. clone the repo into a empty folder
3. make sure that the value of the "umbracoDbDSN" connection string is empty in /UmbracoDemo/appsettings.json and /UmbracoDemo/appsettings.Development.json
4. setup an empty sql database and setup the 'MachineDomain'-conectionString in  /UmbracoDemo/appsettings.json
5. startup the webapp
    - cd UmbracoDemo
    - dotnet run

6. follow the screens to perform the Umbraco Installation
7. after installation you will redirected to the backoffice go to Settings -> Usync and perform a full import (content and settings)

Now the demo solution should be up and running.

DEMO runs now without sql-server installation
