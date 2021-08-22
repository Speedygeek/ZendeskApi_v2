// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

//[assembly: NUnit.Framework.LevelOfParallelism(0)]
//[assembly: NUnit.Framework.Parallelizable(NUnit.Framework.ParallelScope.Fixtures)]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "SEC0112:Path Tampering Unvalidated File Path", Justification = "Unit Test", Scope = "type", Target = "~T:Tests.UserTests")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "SEC0112:Path Tampering Unvalidated File Path", Justification = "Unit Test", Scope = "type", Target = "~T:Tests.TicketTests")]
