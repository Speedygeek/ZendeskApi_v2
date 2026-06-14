# Infrastructure Documentation

This folder contains all infrastructure-as-code and operations notes for production deployment and artifact-signing lifecycle.

## Deployment Scope

Production infrastructure deployment is manual-only through `.github/workflows/deploy-infrastructure.yml` and uses:

1. `infra/main.bicep`
2. `infra/modules/signing-resources.bicep`
3. `infra/parameters/prod.parameters.json`

The deployment workflow runs subscription-scope deployment and does the following:

1. Registers `Microsoft.CodeSigning` resource provider.
2. Creates or updates the configured production resource group.
3. Creates or updates Azure Artifact Signing account and certificate profile.
4. Executes `az deployment sub what-if` before `az deployment sub create`.

## Phased Deployment Model

The deployment workflow supports explicit phases to handle human identity-verification dependencies:

1. `bootstrap`
	1. Creates or updates resource group and artifact signing account.
	2. Does not create certificate profile.
2. `finalize`
	1. Creates or updates certificate profile after identity verification is completed by a human.
	2. Requires `identity_validation_id` workflow input.
3. `full`
	1. Executes bootstrap and finalize behavior in one run.
	2. Requires `identity_validation_id` workflow input.

Default safe behavior is bootstrap-only. `infra/parameters/prod.parameters.json` sets `createCertificateProfile` to `false` and `identityValidationId` to empty.

## Authentication Model

Infrastructure and signing workflows use OIDC federation via `azure/login@v2`.

Required Azure-related repository or environment secrets:

1. `AZURE_TENANT_ID`
2. `AZURE_CLIENT_ID`
3. `AZURE_SUBSCRIPTION_ID`
4. `AZURE_TRUSTED_SIGNING_ENDPOINT`
5. `AZURE_TRUSTED_SIGNING_ACCOUNT`
6. `AZURE_TRUSTED_SIGNING_PROFILE`

Configure a federated credential on the Microsoft Entra application backing `AZURE_CLIENT_ID` for this repository/environment.

## Legacy Signing Cleanup

Use `infra/signing-azure-cleanup.md` to track and remove no-longer-needed Azure resources from the retired signing implementation.