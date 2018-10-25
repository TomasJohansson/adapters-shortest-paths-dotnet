This .Net Core project was added just to verify that 
there are no compilation nor runtime failures with 
the tests when running from .NET Core.

No new tests have been added to this project
but instead just some tests from other projects 
have been added as links, i.e. reused source code files.

Note that to be able to run these NUnit tests from Visual Studio 
it was necessary to add the following dependency in the project file:
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="15.9.0" />

When this project was initially added and succeded with the three added test files,
it was configured as a .NET Core 1.0 project, 
and the tested code (i.e. the non-test assembiles)
had been compiled with the target .NET Standard 1.6.
Regarding the support for .NET Standard versions from 
.NET Core and .NET Framework  see the table below:

https://docs.microsoft.com/en-us/dotnet/standard/net-standard
Standard version.
.NET Standard 					1.0 	1.1 	1.2 	1.3 	1.4 	1.5 	1.6 	2.0
.NET Core 						1.0 	1.0 	1.0 	1.0 	1.0 	1.0 	1.0 	2.0
.NET Framework 1 				4.5 	4.5 	4.5.1 	4.6 	4.6.1 	4.6.1 	4.6.1 	4.6.1