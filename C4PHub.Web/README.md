# C4PHub.Web

This project contains the web part of the platform.

## Configuration

To correctly configure the website it is necessary to add the various configuration snippets of the modules used and in particular:

- `StorageAccountTablePersistance`: persistence in the storage account (configuration);
- `SessionizeC4PExtractor`: module for extracting information from calls for papers from Sessionize (configuration);
- `OpenAIC4PExtractor`: module for extracting information from calls for papers from other websites (no Sessionize) (configuration).

The configuration file is the following:

``` json
{
  "Logging": {
    ...
  },
  "AllowedHosts": "*",
  "RssFeedUrl": "<rss feed generator URL>",
  "StorageAccount": {
    ...
  },
  "OpenAIService": {
    ...
  },
  "EventGridNotification": {
    ...
  }
}
```

The RssFeedUrl field contains the URL of the RssFeed generator.
For the other blocks see the configuration sections of the respective modules.
