# C4PHub.Core

This project contains all the core classes and interfaces of the project.

## Interfaces

### [IC4PExtractor Interface](Interfaces/IC4PExtractor.cs)

This interface defines the contract of the modules that extract C4P information from web pages.

- `CanManagedC4PAsync` : this method checks whether the URL of the C4P (the Url property of the [`C4PInfo`](Entities/C4PInfo.cs) class passed as an argument) can be parsed by the module or not. The return value true indicates that the module is able to extract information from the C4P web page, while the value false indicates that the module is not able to process the web page;
- `FillC4PAsync` : this method analyzes the C4P web page and fills in the information contained in the [`C4PInfo`](Entities/C4PInfo.cs) class argument of the method. The return value indicates whether the method was successful.

### [IC4PExtractorFactory Interface](Interfaces/IC4PExtractorFactory.cs)

The interface defines the contract of the factory class which, given an instance of the [`C4PInfo`](Entities/C4PInfo.cs) class, returns the instance of the module that implements the `IC4PExtractor` interface capable of analyzing the web page.

- `GetExtractorAsync` : returns the instance of the class that can analyze the C4P web page. Returns `null` if there are no modules are able to manage the web page.

### [IC4PManager Interface](Interfaces/IC4PManager.cs)

The interface defines the contract that must implement the class used to fill the [`C4PInfo`](Entities/C4PInfo.cs)  object starting from the URL provided by the user.

- `CreateC4PFromUrlAsync` : Creates a C4P from a given URL. 

### [IC4PPersistance Interface](Interfaces/IC4PPersistance.cs)

The interface defines the contract that the class that deals with the persistence of the calls for paper entered by the user must implement.

- `ExistsC4PAsync` : Checks if a C4PInfo object exists;
- `SaveC4PAsync` : Saves a C4PInfo object;
- `GetOpenedC4PsAsync` : Gets a collection of opened C4PInfo objects.

### [IFeedService Interface](Interfaces/IFeedService.cs)

The interface defines the contract that the class that generates the RSS feed for calls for papers must implement.

- `GenerateFeedAsync` : Generates the RSS feed.