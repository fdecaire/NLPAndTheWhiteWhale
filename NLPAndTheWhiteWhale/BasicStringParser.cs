using System;
using System.Collections.Generic;

namespace NLPAndTheWhiteWhale
{
    public class BasicStringParser
    {
        /// <summary>
        /// Extract a list of words that match the parts of speech as specified by speechPart (JJ, NN, etc.)
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="speechPart"></param>
        /// <returns></returns>
        public List<string> ExtractWords(string phrase, string speechPart)
        {
            var words = WordsInPhrase(phrase);

            var result = new List<string>();
            foreach (var word in words)
            {
                if (!word.StartsWith(speechPart + " ")) continue;

                var processedWord = word.Replace(speechPart + " ", "").ToLower();
                result.Add(processedWord);
            }

            return result;
        }

        public string[] WordsInPhrase(string phrase)
        {
            //(NP (DT the) (JJ great) (JJ white) (NN whale))
            phrase = phrase.Replace("(NP (", "");

            // remove the 2 end parenthesis
            if (phrase.Length > 2)
            {
                phrase = phrase.Remove(phrase.Length - 2);
            }

            //(DT the) (JJ great) (JJ white) (NN whale)
            return phrase.Split(new[] { ") (" }, StringSplitOptions.None);
        }
    }
}
