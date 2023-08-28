# Hacker News RESTful Sample

API provides **GetBestStories** method that returns *n* best stories from the Hacker News API as determined by their score, where *n* is specified by the caller to the API.

### Requrements

- .NET 6.0
- Visual Studio

### Building
The easiest way to build is to load HackerNewsRestSample solution and build it in VisualStudio

### Testing
Testing can be done with Swagger UI by running it from VisualStudio (F5)

### Future Enhancements
- Add unit tests for GetBestStoriesController - story service can be easily mocked
- Create nant build script to simplify CI/CD integration (parametrization, versioning etc.)
- Add integration tests for HackerNewsService
- Add support for docker to increase load scalability
