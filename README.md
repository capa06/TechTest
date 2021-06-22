VisualStudio 2019 and .Net 5.0 are required 

1. There is a service implemented to retrieve results from Google.co.uk and Bing.com and returns the combined results.
2. Views are added for an index page to enter the user's keyword and run the service.
3. Data Models for search and results are added.
4. The search results are retrieved by http requests to the search engines so that the using of API is avoided, tools like chrome developper tools and postman are used for this purpose.
5. To parse the Html response, a third party api is used: Html Agility Pack. 
6. After examining the html response, the pattern of returned links and titles can be found and then parsed.
