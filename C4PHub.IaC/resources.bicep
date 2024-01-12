@description('The location wher you want to create the resources.')
param location string = resourceGroup().location

@description('The name of the environment. It will be used to create the name of the resources in the resource group.')
param environmentName string = 'c4phub'

var dataStorageAccountName = toLower('${environmentName}datastore')
var appServicePlanName = toLower('${environmentName}plan')
var appServiceName = toLower('${environmentName}')
var appServiceNameRss = toLower('${environmentName}feed')
var openAiServiceName = toLower('${environmentName}openai')
var logAnalyticsWorkspaceName = toLower('${environmentName}ws')
var applicationInsightsName = toLower('${environmentName}ai')
var eventGridTopicName = toLower('${environmentName}topic')

//------------------------------------------------------------
// Create data storage account and table for C4Ps
//------------------------------------------------------------
resource dataStorageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: dataStorageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
  }
}

resource tableService 'Microsoft.Storage/storageAccounts/tableServices@2023-01-01' = {
  name: 'default'
  parent: dataStorageAccount
}

resource c4pTable 'Microsoft.Storage/storageAccounts/tableServices/tables@2023-01-01' = {
  name: 'c4p'
  parent: tableService
}
//------------------------------------------------------------

//------------------------------------------------------------
// Create Azure Open AI service
//------------------------------------------------------------
resource openAiService 'Microsoft.CognitiveServices/accounts@2023-10-01-preview' = {
  name: openAiServiceName
  location: location
  sku: {
    name: 'S0'
  }
  kind: 'OpenAI'
  properties: {
    apiProperties: {
      publicNetworkAccess: 'Enabled'
    }
  }
}
//------------------------------------------------------------

//------------------------------------------------------------
// Create log analytics workspace for log
//------------------------------------------------------------
resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties:{
    sku: {
      name: 'PerGB2018'
    }
  }
}
//------------------------------------------------------------

//------------------------------------------------------------
// Create application insights for log
//------------------------------------------------------------
resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: applicationInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    IngestionMode: 'LogAnalytics'
    WorkspaceResourceId: logAnalyticsWorkspace.id
  }
}
//------------------------------------------------------------

//------------------------------------------------------------
//  Create app service plan and app service
//------------------------------------------------------------
resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
  }
}

resource appService 'Microsoft.Web/sites@2022-09-01' = {
  name: appServiceName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
  }
}

resource appServiceRss 'Microsoft.Web/sites@2022-09-01' = {
  name: appServiceNameRss
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
  }
}

resource appSettings 'Microsoft.Web/sites/config@2022-09-01' = {
  name: 'appsettings'
  parent: appService
  properties: {
    'OpenAIService:Endpoint': openAiService.properties.endpoint
    'OpenAIService:Key': openAiService.listKeys().key1
    'OpenAIService:Modelname': 'mainModel'
    'StorageAccount:ConnectionString': 'DefaultEndpointsProtocol=https;AccountName=${dataStorageAccountName};EndpointSuffix=${environment().suffixes.storage};AccountKey=${dataStorageAccount.listKeys().keys[0].value}'
    'StorageAccount:TableName': 'c4p'
    APPINSIGHTS_INSTRUMENTATIONKEY: applicationInsights.properties.InstrumentationKey
    APPLICATIONINSIGHTS_CONNECTION_STRING: 'InstrumentationKey=${applicationInsights.properties.InstrumentationKey}'

  }
}

resource appSettingsRss 'Microsoft.Web/sites/config@2022-09-01' = {
  name: 'appsettings'
  parent: appServiceRss
  properties: {
    'StorageAccount:ConnectionString': 'DefaultEndpointsProtocol=https;AccountName=${dataStorageAccountName};EndpointSuffix=${environment().suffixes.storage};AccountKey=${dataStorageAccount.listKeys().keys[0].value}'
    'StorageAccount:TableName': 'c4p'
    APPINSIGHTS_INSTRUMENTATIONKEY: applicationInsights.properties.InstrumentationKey
    APPLICATIONINSIGHTS_CONNECTION_STRING: 'InstrumentationKey=${applicationInsights.properties.InstrumentationKey}'

  }
}
//------------------------------------------------------------

//------------------------------------------------------------
// EventGrid custom topic
//------------------------------------------------------------
resource eventGridTopic 'Microsoft.EventGrid/topics@2021-06-01-preview' = {
  name: eventGridTopicName
  location: location
  sku: {
    name: 'Basic'
  }
}


//------------------------------------------------------------
