using System;
using System.Collections.Generic;

namespace NLPAndTheWhiteWhale
{
    public class SmartBook
    {
        public string Title { get; set; }
        public List<string> Adjectives { get; set; }
        public string Noun { get; set; }

        public SmartBook(string title)
        {
            Title = title;

            // feed the title into NLP processor and generate the subject and adjectives
            var decodedTitle = NlpProcessor.DecodeSentence(title);

            var temp = NlpProcessor.FindNounePhrases("The big white Whale with the white Fin");

            // extract all JJ and NN words
            var basicStringParser = new BasicStringParser();
            var nouns = basicStringParser.ExtractWords(decodedTitle, "NN");
            var adjectives = basicStringParser.ExtractWords(decodedTitle, "JJ");

            if (nouns.Count > 0)
            {
                Noun = nouns[0];
            }

            // check for proper noun
            nouns = basicStringParser.ExtractWords(decodedTitle, "NNP");
            if (nouns.Count > 0)
            {
                Noun = nouns[0];
            }

            Adjectives = adjectives;
        }

        public int MatchRecord(string noun, List<string> adjectives)
        {
            if (noun != Noun)
            {
                Console.WriteLine("bad match:" + Title + " noun:" + Noun);
                return -1;
            }

            

            var adjectiveCount = 0;

            foreach (var adjective in adjectives)
            {
                foreach (var localAdjective in Adjectives)
                {
                    if (adjective.ToLower() == localAdjective)
                    {
                        adjectiveCount++;
                        break;
                    }
                }
            }

            return adjectiveCount;
        }
    }
}
