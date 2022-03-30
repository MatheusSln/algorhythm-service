using System;
using System.Reflection;

namespace AlgorhythmService.Shared.UnitTest
{
    public class UnitTestDetector
    {
        private static bool _isRunningUnitTest = false;

        static UnitTestDetector()
        {
            foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assem.FullName.ToLowerInvariant().StartsWith("xunit"))
                {
                    _isRunningUnitTest = true;
                    break;
                }
            }
        }

        public static bool IsRunningUnitTest
        {
            get { return _isRunningUnitTest; }
        }
    }
}
