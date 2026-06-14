# CI/CD and Signing

Build, package, signing, and publishing are managed in GitHub Actions:

1. `.github/workflows/ci-release.yml`
2. `.github/workflows/deploy-infrastructure.yml`

The legacy Azure DevOps pipeline in `ci/azure-pipelines.yml` is disabled (`trigger: none`, `pr: none`) and retained only as historical reference.

## Required Signing Secrets

Configure these repository or environment secrets for Azure Artifact Signing with federated identity:

1. `AZURE_TENANT_ID`
2. `AZURE_CLIENT_ID`
3. `AZURE_SUBSCRIPTION_ID`
4. `AZURE_TRUSTED_SIGNING_ENDPOINT`
5. `AZURE_TRUSTED_SIGNING_ACCOUNT`
6. `AZURE_TRUSTED_SIGNING_PROFILE`

The workflows use OIDC via `azure/login@v2`. Configure a federated credential on the Microsoft Entra application backing `AZURE_CLIENT_ID` for this repository/environment.

## Package Publish Secrets

1. `NUGET_API_KEY` (stable tag releases)
2. `GITHUB_FEED_URL` and `GITHUB_FEED_API_KEY` (prerelease feed)
3. `MYGET_FEED_URL` and `MYGET_API_KEY` (prerelease feed)

## Test Secrets

Integration tests require:

1. `ADMIN_ID`
2. `ADMIN_EMAIL`
3. `ADMIN_API_TOKEN`

If test secrets are missing, the workflow skips integration tests and logs a notice.

## Related Docs

Infrastructure-specific documentation lives in `infra/README.md`.