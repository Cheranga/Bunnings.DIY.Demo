targetScope = 'subscription'

param appName string
param version string
@allowed([
  'dev'
  'staging'
  'prod'
])
param environment string
param location string

var envType = {
  dev: 'nonprod'
  staging: 'prod' // note: since this is used mainly for SKUs, we might change it later.
  prod: 'prod'
}

var appNameWithEnvironment = '${appName}-${environment}'
var rgName = 'bun-rg-${appNameWithEnvironment}'
var funcAppName = 'bun-fn-${appNameWithEnvironment}'
var sgName = take(replace('bunsg${appNameWithEnvironment}', '-', ''), 24)
var appInsName = 'bun-ins-${appNameWithEnvironment}'
var aspName = 'bun-asp-${appNameWithEnvironment}'
var kvName = take(replace('bunkv${appNameWithEnvironment}', '-', ''), 24)

module rg 'resourcegroup/template.bicep' = {
  scope: subscription()
  name: '${version}-rg'
  params: {
    location: location
    name: rgName
  }
}

module storageAccount 'storageaccount/template.bicep' = {
  name: '${version}-sg'
  scope: resourceGroup(rgName)
  params: {
    name: sgName
    location: location
    queues: 'inputs'
    blobContainers: 'input'
    storageType: envType[environment]
  }
  dependsOn: [
    rg
  ]
}


module appInsights 'appinsights/template.bicep' = {
  name: '${version}-ins'
  scope: resourceGroup(rgName)
  params: {
    name: appInsName
    location: location
  }
  dependsOn: [
    rg
  ]
}

module appServicePlan 'appserviceplan/template.bicep' = {
  name: '${version}-asp'
  scope: resourceGroup(rgName)
  params: {
    name: aspName
    location: location
    category: envType[environment]
  }
  dependsOn: [
    rg
  ]
}