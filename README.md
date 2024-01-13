# C4PHub

This is the repository of the C4PHub project which allows you to share calls for papers with the community.

The platform allows you to enter the URL of the web page that contains the call for paper and tries to extract the information from it automatically.

The components of the platform are:

- [C4PHub.Core](C4PHub.Core/README.md) : contains classes and interfaces used by the other modules;
- [C4PHub.Web](C4PHub.Web/README.md) : contains the web site;
- [C4PHub.RssApi](C4PHub.RssApi/README.md) : contains the RSS feed generator module (exposed as WebApi);
- [C4PHub.EventGrid](C4PHub.Eventgrid/README.md) : contains the implementations for Event Grid (the notification module for custom topic);
- [C4PHub.OpenAI](C4PHub.OpenAI/README.md) : contains the implementation of the extractor that uses Azure OpenAI;
- [C4PHub.Sessionize](C4PHub.Sessionize/README.md) : contains the implementation of the Sessionize extractor;
- [C4PHub.StorageAccount](C4PHub.StorageAccount/README.md) : contains the implementatipon of the persistance module based on Storage Account Tables.

