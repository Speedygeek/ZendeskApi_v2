targetScope = 'subscription'

@description('Location used for subscription deployment metadata.')
param location string = deployment().location

@description('Name of the resource group that will host artifact signing resources.')
param resourceGroupName string

@description('Location of the resource group. Defaults to deployment location.')
param resourceGroupLocation string = location

@description('Azure Artifact Signing account name.')
param codeSigningAccountName string

@description('Certificate profile name used by the signing workflow.')
param certificateProfileName string

@description('Identity validation identifier for the certificate profile. Required only when createCertificateProfile is true.')
param identityValidationId string = ''

@description('When true, deploys the certificate profile. Keep false during bootstrap until manual identity verification is complete.')
param createCertificateProfile bool = false

@allowed([
  'PublicTrust'
  'PublicTrustTest'
  'PrivateTrust'
  'PrivateTrustCIPolicy'
  'VBSEnclave'
])
@description('Type of certificate profile to create.')
param certificateProfileType string = 'PublicTrust'

@description('SKU name for the artifact signing account.')
param signingAccountSkuName string = 'Basic'

@description('Tags applied to managed resources.')
param tags object = {
  project: 'ZendeskApi_v2'
  managedBy: 'github-actions'
  environment: 'prod'
}

resource signingResourceGroup 'Microsoft.Resources/resourceGroups@2025-04-01' = {
  name: resourceGroupName
  location: resourceGroupLocation
  tags: tags
}

module signingResources './modules/signing-resources.bicep' = {
  name: 'signingResourcesDeployment'
  scope: resourceGroup(resourceGroupName)
  params: {
    location: resourceGroupLocation
    codeSigningAccountName: codeSigningAccountName
    certificateProfileName: certificateProfileName
    identityValidationId: identityValidationId
    createCertificateProfile: createCertificateProfile
    certificateProfileType: certificateProfileType
    signingAccountSkuName: signingAccountSkuName
    tags: tags
  }
  dependsOn: [
    signingResourceGroup
  ]
}

output resourceGroupId string = signingResourceGroup.id
output codeSigningAccountName string = signingResources.outputs.codeSigningAccountName
output certificateProfileName string = signingResources.outputs.certificateProfileName
output certificateProfileResourceId string = signingResources.outputs.certificateProfileResourceId