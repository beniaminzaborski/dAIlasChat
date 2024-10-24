@description('Environment name')
param environment string

@description('Azure region')
param location string = resourceGroup().location
var shortLocation = substring(location, 0, 6)

@description('Project name')
var projectName = 'dAIlasChat'

var createdBy = 'Beniamin'

resource openAI 'Microsoft.CognitiveServices/accounts@2024-06-01-preview' = {
    name: 'oai-${projectName}-${environment}-${shortLocation}'
    location: location
    identity: {
        type: 'SystemAssigned'
    }
    sku: {
        name: 'S0'
    }
    kind: 'OpenAI'
    properties: {
        publicNetworkAccess: 'Enabled'
    }
    tags: {
        environment: environment
        createdBy: createdBy
  }
}

resource computerVision 'Microsoft.CognitiveServices/accounts@2024-06-01-preview' = {
    name: 'cv-${projectName}-${environment}-${shortLocation}'
    location: location
    identity: {
        type: 'SystemAssigned'
    }
    sku: {
        name: 'S1'
    }
    kind: 'ComputerVision'
    properties: {
        publicNetworkAccess: 'Enabled'
    }
    tags: {
        environment: environment
        createdBy: createdBy
  }
}

resource speach 'Microsoft.CognitiveServices/accounts@2024-06-01-preview' = {
    name: 'spch-${projectName}-${environment}-${shortLocation}'
    location: location
    identity: {
        type: 'SystemAssigned'
    }
    sku: {
        name: 'S0'
    }
    kind: 'SpeechServices'
    properties: {
        publicNetworkAccess: 'Enabled'
    }
    tags: {
        environment: environment
        createdBy: createdBy
  }
}
