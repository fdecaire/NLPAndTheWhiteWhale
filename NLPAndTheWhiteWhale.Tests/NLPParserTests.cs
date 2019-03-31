using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLPAndTheWhiteWhale.Tests
{
    [TestClass]
    public class NLPParserTests
    {
        [TestMethod]
        public void ParseTest1()
        {
            var decodedTitle = NlpProcessor.FindNounePhrases("The Whale with the White Fin");

            Assert.AreEqual(2, decodedTitle.Count);
        }

        [TestMethod]
        public void ParseTest2()
        {
            var decodedTitle = NlpProcessor.FindNounePhrases("Where the Wild Things Are");

            Assert.AreEqual(1, decodedTitle.Count);
        }
    }
}
