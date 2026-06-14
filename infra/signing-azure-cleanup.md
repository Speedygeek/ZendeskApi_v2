# Azure Signing Resource Cleanup

This document tracks Azure resources that supported legacy package signing and can be removed after successful cutover to GitHub Actions plus Azure Artifact Signing.

## Cutover Prerequisites

1. GitHub workflow `CI and Release` has successfully produced at least one signed package.
2. Existing Azure pipeline is disabled in both YAML (`trigger: none`, `pr: none`) and Azure DevOps UI.
3. Release validation confirms no dependency on `ci/sign-package.ps1` and `ci/appsettings.json`.

## Resource Inventory

| Resource | Source Evidence | Classification | Owner | Dependency Check | Earliest Safe Removal |
|---|---|---|---|---|---|
| Legacy signing service endpoint `https://speedygeeksign.azurewebsites.net` (App Service or equivalent) | `ci/appsettings.json` | review | Release engineering | Confirm no active callers in Azure DevOps, scripts, or external automation | After 2 successful signed releases from GitHub Actions |
| Azure AD application / service principal `ClientId: 76cedf52-1079-4f5a-a168-f884babbfcc9` | `ci/appsettings.json` | review | Identity admin | Verify not reused by other apps, pipelines, or internal tools | After endpoint decommission decision and dependency sign-off |
| Legacy SignService resource identifier `https://SignService/11a1f02b-fc10-4ff8-a769-b3682801653e` | `ci/appsettings.json` | review | Release engineering | Resolve backing Azure resource and verify no remaining usage | After endpoint and app identity are retired |
| Azure DevOps signing secrets (`speedygeek.signClientUser`, `speedygeek.signClientSecret`) | `ci/azure-pipelines.yml` | remove | Azure DevOps admin | Ensure no active pipeline references remain | Immediately after pipeline disablement confirmation |

## Candidate Dependent Resources to Evaluate

These are common resources attached to signing services. Do not delete them until ownership and shared usage are verified.

1. App Service plan hosting legacy signing endpoint.
2. Resource group containing legacy signing service.
3. Key Vault certificate/keys used by legacy signing service.
4. Application Insights / Log Analytics workspace connected to the signing service.
5. Storage accounts used by the signing service.

Mark each as `remove`, `retain`, or `review` with the same columns as the inventory table before decommission work starts.

## Pre-Delete Validation Checklist

1. Confirm `ci/azure-pipelines.yml` has no active triggers.
2. Confirm Azure DevOps pipeline definition is disabled.
3. Confirm GitHub Actions signing secrets are configured:
   1. `AZURE_TENANT_ID`
   2. `AZURE_CLIENT_ID`
   3. `AZURE_SUBSCRIPTION_ID`
   4. `AZURE_TRUSTED_SIGNING_ENDPOINT`
   5. `AZURE_TRUSTED_SIGNING_ACCOUNT`
   6. `AZURE_TRUSTED_SIGNING_PROFILE`
4. Run release workflow for a prerelease or tag and confirm package signing succeeds.
5. Confirm no systems outside this repository call the legacy endpoint.

## Removal Steps

1. Disable ingress/traffic to legacy signing service.
2. Rotate or revoke credentials for legacy signing identity.
3. Delete signing-specific secrets from Azure DevOps variable groups.
4. Remove or delete signing-dedicated Azure resources that are marked `remove`.
5. For resources marked `review`, obtain owner sign-off before action.

## Rollback Plan

1. If GitHub signing fails after cleanup, stop releases.
2. Restore access to any removed dependency only if needed and if recovery is possible.
3. Re-run release workflow once signing configuration is corrected.
4. Document incident details and update this file with final disposition.

## Post-Removal Verification

1. Execute at least one signed release in GitHub Actions.
2. Confirm package signature validation and successful publish destination.
3. Confirm no alerts or errors from removed legacy resources.
4. Close cleanup work item with evidence links.