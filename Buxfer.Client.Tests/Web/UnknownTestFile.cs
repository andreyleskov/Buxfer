using Xunit;

namespace Buxfer.Client.Tests.Web
{
    public class UnknownTestFile
    {
        private const string TestFileName = "testFileName";

        [Fact]
        public void WhenParsingxUnit2ResultsFile()
        {
            // Replace "testFileName" with TestFileName
            var testFile = File.ReadAllText(TestFileName);
            
            // Rest of the code
        }
    }
}
