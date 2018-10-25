using System;
using System.Collections.Generic;

// Note: Currently the three files beginning 
// with the name "TargetFramework" 
// are being reused from other projects 
// by using "add link as" ...
// since it is currently considered as overkill
// to create a new project to become reused 
// only for these few files ...

namespace Programmerare.ShortestPaths.Test.Utils
{
    internal class TargetFramework
    {
        private readonly TargetFrameworkEnum _targetFramework;

        internal TargetFramework(TargetFrameworkEnum targetFramework)
        {
            Console.WriteLine("Detected targetFramework for the assembly: " + targetFramework); 
            this._targetFramework = targetFramework;
        }

        internal bool IsSupportingFileStreamReader()
        {
            // TODO refactor this HashSet into a static variable instead of creating local variable as below ...
            var targetFrameworksNotSupportingFileStreamReader = new HashSet<TargetFrameworkEnum>
            {
                TargetFrameworkEnum.NETSTANDARD1_0,
                TargetFrameworkEnum.NETSTANDARD1_1,
                TargetFrameworkEnum.NETSTANDARD1_2,
                TargetFrameworkEnum.NETSTANDARD1_3,
                TargetFrameworkEnum.NETSTANDARD1_4,
                TargetFrameworkEnum.NETSTANDARD1_5,
                TargetFrameworkEnum.NETSTANDARD1_6
            };
            // assume the others are supporting StreamReader
            // (at least all others being used/targeted within this Visual Studio solution)
            return !targetFrameworksNotSupportingFileStreamReader.Contains(_targetFramework);
        }
    }
}
