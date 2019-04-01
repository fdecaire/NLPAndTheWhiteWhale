using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLPAndTheWhiteWhale.Tests
{
    [TestClass]
    public class NLPParserTests
    {
        [TestMethod]
        public void ParseTest1()
        {
            var decodedTitle = NlpProcessor.FindNounPhrases("the whale with the white fin");

            Assert.AreEqual(2, decodedTitle.Count);
        }

        [TestMethod]
        public void ParseTest2()
        {
            var decodedTitle = NlpProcessor.FindNounPhrases("where the wild things are");

            Assert.AreEqual(1, decodedTitle.Count);
        }

        [TestMethod]
        public void ParseTest3()
        {
            var decodedTitle = NlpProcessor.FindNounPhrases("humpback whale");

            Assert.AreEqual(1, decodedTitle.Count);
            Assert.IsTrue(decodedTitle[0].Nouns.Contains("whale"));
        }
    }
}
