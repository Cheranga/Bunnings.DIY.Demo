param name string
param location string = resourceGroup().location
param queues string
param blobContainers string

var queueArray = empty(queues)? [] : split(queues, ',')
var containerArray = empty(blobContainers)? [] : split(blobContainers, ',')

@allowed([
  'nonprod'
  'prod'
])
param storageType string = 'nonprod'

var storageSku = {
  nonprod: 'Standard_LRS'
  prod: 'Standard_GRS'
}

resource stg 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: name
  location: location
  kind: 'StorageV2'
  sku: {
    name: storageSku[storageType]
  }
}

resource queueService 'Microsoft.Storage/storageAccounts/queueServices@2021-08-01' = if (!empty(queueArray)) {
  parent: stg
  name: 'default'
  resource aaa 'queues' = [for q in queueArray: {
    name: q
  }]
}

resource blobService 'Microsoft.Storage/storageAccounts/blobServices@2021-08-01' = if (!empty(containerArray)) {
  parent: stg
  name: 'default'
  resource aaa 'containers' = [for c in containerArray: {
    name: c
  }]
}
