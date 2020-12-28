using Cake.Core.IO;

using Xunit;

namespace Cake.Gradle.Tests.FactAttributes
{
    public class FactOnWindowsAttribute: FactAttribute {

        public FactOnWindowsAttribute() {
            if(IsNotRunningOnWindows()) {
                // ReSharper disable once VirtualMemberCallInConstructor
                Skip = "This test only runs on Windows.";
            }
        }

        private static bool IsNotRunningOnWindows()
        {
            return new DirectoryPath("C:\\").IsRelative;
        }
    }
}
