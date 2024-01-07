targetScope = 'subscription'
@description('The name of the resource group that contains all the resources')
param resourceGroupName string = 'C4PHub-rg'

@description('The name of the environment. It will be used to create the name of the resources in the resource group.')
param environmentName string = 'c4phub'

@description('The location of the resource group and resources')
param location string = deployment().location

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}

module resourcesModule 'resources.bicep' = {
  scope: resourceGroup
  name: 'resources'
  params: {
    location: location
    environmentName: environmentName
  }
}

