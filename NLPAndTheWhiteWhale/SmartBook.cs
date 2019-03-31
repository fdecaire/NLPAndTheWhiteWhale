using System.Collections.Generic;

namespace NLPAndTheWhiteWhale
{
    public class SmartBook
    {
        public string Title { get; set; }
        public List<NounPhrase> NounPhrases { get; set; }

        public SmartBook(string title)
        {
            Title = title;

            // feed the title into NLP processor and generate the subject and adjectives
            NounPhrases = NlpProcessor.FindNounPhrases(Title.ToLower());
        }

        public int MatchRecord(string noun, List<string> adjectives)
        {
            foreach (var nounPhrase in NounPhrases)
            {
                if (noun == nounPhrase.Noun)
                {
                    var adjectiveCount = 0;

                    foreach (var adjective in adjectives)
                    {
                        foreach (var localAdjective in nounPhrase.Adjectives)
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

            return -1;
        }
    }
}
