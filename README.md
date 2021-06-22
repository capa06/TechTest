1. There is a service implemented to retrieve results from Google.co.uk and Bing.com and returns the combined results.
2. Views are added for an index page to enter user's keyword and run the service.
3. Data Models for search and results are added.
4. The serach results are retrieved by http requests to the serach engines so that the using of API is avoided.
5. To parse the Html response, a third party api is used: Html Agility Pack. 
6. After exampling the html repsonse, the pattern of returned links and titles can be found and then parsed.
