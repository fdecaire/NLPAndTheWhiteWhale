using System.Collections.Generic;
using System.Linq;

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

        public int MatchRecord(List<string> noun, List<string> adjectives)
        {
            var scoreList = new List<int>();
            foreach (var nounPhrase in NounPhrases)
            {
                var nounCount = 0;
                foreach (var nounItem in nounPhrase.Nouns)
                {
                    if (noun.Contains(nounItem))
                    {
                        nounCount++;
                    }
                }

                if (nounCount > 0)
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

                    scoreList.Add(nounCount * 100 + adjectiveCount);
                }
            }

            if (!scoreList.Any())
            {
                return -1;
            }

            var largest = scoreList.Max();
            if (largest == 0)
            {
                return -1;
            }

            return largest;
        }
    }
}
