targetScope = 'resourceGroup'

@description('Location for artifact signing resources.')
param location string = resourceGroup().location

@description('Azure Artifact Signing account name.')
param codeSigningAccountName string

@description('Certificate profile name used by the signing workflow.')
param certificateProfileName string

@description('Identity validation identifier for the certificate profile subject. Required only when createCertificateProfile is true.')
param identityValidationId string = ''

@description('When true, deploys certificate profile resources.')
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
param tags object = {}

resource codeSigningAccount 'Microsoft.CodeSigning/codeSigningAccounts@2025-10-13' = {
  name: codeSigningAccountName
  location: location
  tags: tags
  properties: {
    sku: {
      name: signingAccountSkuName
    }
  }
}

resource certificateProfile 'Microsoft.CodeSigning/codeSigningAccounts/certificateProfiles@2025-10-13' = if (createCertificateProfile) {
  parent: codeSigningAccount
  name: certificateProfileName
  properties: {
    identityValidationId: identityValidationId
    profileType: certificateProfileType
  }
}

output codeSigningAccountName string = codeSigningAccount.name
output certificateProfileName string = createCertificateProfile ? certificateProfile.name : ''
output certificateProfileResourceId string = createCertificateProfile ? certificateProfile.id : ''