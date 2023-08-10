targetScope = 'resourceGroup'

param name string
param location string = resourceGroup().location

@allowed([
    'nonprod'
    'prod'
  ]
)
param category string = 'nonprod'

var sku = {
  nonprod: 'Y1'
  prod: 'Y1'
}

resource asp 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: name
  location:location
  sku: {
    name: sku[category]
    tier: sku[category]
  }
}
