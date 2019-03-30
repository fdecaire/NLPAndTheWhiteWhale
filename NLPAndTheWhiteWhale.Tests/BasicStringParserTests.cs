using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLPAndTheWhiteWhale.Tests
{
    [TestClass]
    public class BasicStringParserTests
    {
        [TestMethod]
        public void NounPhraseTest1()
        {
            var basicStringParser = new BasicStringParser();
            var nouns = basicStringParser.ExtractWords("(NP (NNP Moby) (NNP Dick))", "NN");
            //var adjectives = basicStringParser.ExtractWords("", "JJ");

            Assert.Equals(0, nouns.Count);
        }

        [TestMethod]
        public void NounPhraseTest2()
        {
            var basicStringParser = new BasicStringParser();
            var nouns = basicStringParser.ExtractWords("(NP (NP (DT The) (NNP Little) (NNP Engine)) (NP (DT That) (NN Could)))", "NN");

            //The Whale with the White Fin
        }
    }
}
