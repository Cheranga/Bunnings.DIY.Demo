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