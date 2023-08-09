targetScope = 'resourceGroup'

param friendlyName string
param appId string
param storageName string

// Assigning RBAC to access BLOBs in containers
resource blobContributor 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'ba92f5b4-2d11-453d-a403-e96b0029c9fe'
}

// Assigning RBAC to access BLOBs in containers
resource queueWriter 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'c6a89b2d-59bc-44d0-9896-0f6e12d7b80a'
}

resource storage 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: storageName
}

resource rbacBlobContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: friendlyName
  scope: storage
  properties: {
    principalId: appId
    roleDefinitionId: blobContributor.id
  }
}

resource rbacQueueWriter 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: friendlyName
  scope: storage
  properties: {
    principalId: appId
    roleDefinitionId: queueWriter.id
  }
}
