# .NET Version Upgrade - Scenario Instructions

## Scenario Overview
**Scenario**: .NET Version Upgrade  
**Target Framework**: .NET 8.0 (LTS)  
**Description**: Upgrade solution from .NET 6/.NET Framework 4.6.2/.NET Standard 2.1 to .NET 8.0 (LTS)

## Strategy
**Selected**: All-At-Once  
**Rationale**: 3 projects (well under 30-project threshold), low complexity upgrade with straightforward TFM updates and package upgrades, minimal breaking changes (2 API calls), all SDK-style projects with clear dependency structure.

### Execution Constraints
- Single atomic upgrade — all projects updated together in one operation
- No tier ordering or phased rollout — all work happens simultaneously
- Validate full solution build after upgrade (all target frameworks must compile)
- Testing comes after atomic upgrade completes successfully
- One pass for build fixes — fix all compilation errors in a single bounded pass, not iterative retry loop

### Commit Strategy
**Default**: Single Commit at End  
One atomic upgrade operation = one commit after all validation passes.

## Preferences

### Flow Mode
**Mode**: Automatic  
Run end-to-end, only pause when blocked or needing user input. Surface assessment, plan, and progress without waiting for approval at each stage.

### Source Control
- **Source Branch**: main
- **Working Branch**: upgrade-to-NET8
- **Repository Root**: C:\_git\personal\Speedygeek\ZendeskApi_v2

### Technical Preferences
- **Target Framework**: .NET 8.0 (LTS) - Support until November 2026

## Custom Instructions
*(User-specific preferences and constraints will be added here as they are expressed)*

## Key Decisions Log
*(Major decisions made during the upgrade will be recorded here with timestamps)*
