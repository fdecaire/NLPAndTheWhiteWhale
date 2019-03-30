using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLPAndTheWhiteWhale.Tests
{
    [TestClass]
    public class NLPParserTests
    {
        [TestMethod]
        public void ParseTest1()
        {
            var decodedTitle = NlpProcessor.DecodeSentence("The Whale with the White Fin");
        }
    }
}
