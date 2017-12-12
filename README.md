# catLoader
Simple code example project

2 and a half projects here:
cat loader - main console application, that loads people data from the service and then structures it in a required manner.

Splitted into 3 parts:
* CatLoaderService - main part of the application. Controls the workflow of other parts and displays the renders the results to the console;
* AggregationService - transforms the raw data into a render-ready format. In a generic manner!
* PersonProvider - wrapper around HttpClient to get the data from the service call. Potentially can be replaced with a DB reader or something else.

cat loader tests - small xunit tests for all the main parts of the app.

helper service in node + express - to provide the testing service during the development, plus one of the tests is reading from this service (http://localhost:3000 by default).
to run the service go to \catLoaderTests and run "node testService.js"

Used technologies: .net core 2, System.Net.Http, Newtonsoft.Json, AutoFac, xUnit, Moq.
