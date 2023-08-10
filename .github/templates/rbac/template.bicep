targetScope = 'resourceGroup'

param friendlyName string
param appId string
param storageName string

var blobDataOwner = guid('${resourceGroup().name}-${friendlyName}-blobowner')
var queueContributor = guid('${resourceGroup().name}-${friendlyName}-queuecont')
var queueMessageSender = guid('${resourceGroup().name}-${friendlyName}-queuesender')
var blobContributor = guid('${resourceGroup().name}-${friendlyName}-blobcont')
var blobReader = guid('${resourceGroup().name}-${friendlyName}-blobreader')
var storageAccContributor = guid('${resourceGroup().name}-${friendlyName}-stgacccont')



resource storageBlobDataOwner 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b'
}

resource storageBlobContributor 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'ba92f5b4-2d11-453d-a403-e96b0029c9fe'
}

resource storageBlobReader 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: '2a2b9908-6ea1-4ae2-8e65-a410df84e7d1'
}

resource storageQueueDataContributor 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: '974c5e8b-45b9-4653-ba55-5f855dd0fb88'
}

resource storageQueueDataMessageSender 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'c6a89b2d-59bc-44d0-9896-0f6e12d7b80a'
}

resource storageAccountContributor 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: '17d1049b-9a84-46fb-8f53-869881c3d3ab'
}

resource storage 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: storageName
}

resource rbacStorageBlobDataOwner 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: blobDataOwner
  scope: storage
  properties: {
    principalId: appId
    principalType:'ServicePrincipal'
    roleDefinitionId: storageBlobDataOwner.id
  }
}

resource rbacStorageBlobContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: blobContributor
  scope: storage
  properties: {
    principalId: appId
    principalType:'ServicePrincipal'
    roleDefinitionId: storageBlobContributor.id
  }
}

resource rbacStorageBlobReader 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: blobReader
  scope: storage
  properties: {
    principalId: appId
    principalType:'ServicePrincipal'
    roleDefinitionId: storageBlobReader.id
  }
}

resource rbacStorageQueueDataContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: queueContributor
  scope: storage
  properties: {
    principalId: appId
    principalType:'ServicePrincipal'
    roleDefinitionId: storageQueueDataContributor.id
  }
}

resource rbacStorageQueueDataMessageSender 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: queueMessageSender
  scope: storage
  properties: {
    principalId: appId
    roleDefinitionId: storageQueueDataMessageSender.id
  }
}

resource rbacStorageAccountContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: storageAccContributor
  scope: storage
  properties: {
    principalId: appId
    principalType:'ServicePrincipal'
    roleDefinitionId: storageAccountContributor.id
  }
}
