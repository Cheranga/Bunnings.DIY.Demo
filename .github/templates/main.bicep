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
var rgName = 'vcs-rg-${appNameWithEnvironment}'
var funcAppName = 'vcs-fn-${appNameWithEnvironment}'
var sgName = take(replace('vcssg${appNameWithEnvironment}', '-', ''), 24)
var appInsName = 'vcs-ins-${appNameWithEnvironment}'
var aspName = 'vcs-asp-${appNameWithEnvironment}'
var kvName = take(replace('vcskv${appNameWithEnvironment}', '-', ''), 24)

module rg 'resourcegroup/template.bicep' = {
  scope: subscription()
  name: '${version}-rg'
  params: {
    location: location
    name: rgName
  }
}