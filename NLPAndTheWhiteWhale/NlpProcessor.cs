using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.sun.xml.@internal.ws.api;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using StringReader = java.io.StringReader;

namespace NLPAndTheWhiteWhale
{
    public class NlpProcessor
    {
        private static NlpProcessor _nlpProcessor;
        private LexicalizedParser _lexicalizedParser;

        public static NlpProcessor Instance
        {
            get
            {
                if (_nlpProcessor == null)
                {
                    _nlpProcessor = new NlpProcessor();
                }

                return _nlpProcessor;
            }
        }

        public NlpProcessor()
        {
            // this little hack will find the solution directory so we can get the correct path
            // to the stanford parser data directory, which exists in the NLPAndTheWhiteWhale project directory.
            // I did this to make the unit tests work.  In a production environment, the stanford parser data 
            // should reside in the deployment directory.
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            var directories = path.Split(Path.DirectorySeparatorChar);

            // remove the bin/debug (these could have other names depending on your compiler settings, we'll just remove the last to folders in the path)
            var finalPath = "";
            for (var i = 0; i < directories.Length - 3; i++)
            {
                if (finalPath != "")
                {
                    finalPath += @"\";
                }

                finalPath += directories[i];
            }

            // Path to models extracted from `stanford-parser-3.7.0-models.jar`
            var jarRoot = finalPath + @"\NLPAndTheWhiteWhale\stanford-parser-full-2016-10-31\models\";
            var modelsDirectory = jarRoot + @"\edu\stanford\nlp\models";

            // Loading english PCFG parser from file
            _lexicalizedParser = LexicalizedParser.loadModel(modelsDirectory + @"\lexparser\englishPCFG.ser.gz");
        }

        public static string DecodeSentence(string searchText)
        {
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new StringReader(searchText);
            var rawWords = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            sent2Reader.close();
            var tree = Instance._lexicalizedParser.apply(rawWords);

            var treeFind = new TreeFind();
            return treeFind.FindNounPhraseOld(tree).toString();
        }

        public static List<NounPhrase> FindNounePhrases(string searchText)
        {
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new StringReader(searchText);
            var rawWords = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            sent2Reader.close();
            var tree = Instance._lexicalizedParser.apply(rawWords);

            var treeFind = new TreeFind();
            var result = treeFind.FindNounPhrases(tree);

            return result;
        }

        public class TreeFind
        {
            private string _indent = "";
            public string DebugString = "";

            public List<NounPhrase> FindNounPhrases(Tree tree)
            {
                var result = new List<NounPhrase>();
                _indent += "-";

                if (tree == null)
                {
                    return null;
                }

                foreach (var childNode in tree.children())
                {
                    DebugString += _indent + childNode.value() + " " + childNode.firstChild()?.toString() + "\n";
                    if (childNode.value() == "NP")
                    {
                        if (!IsAnyChildNodeANounPhrase(childNode))
                        {
                            // find the noun
                            var noun = FindNoun(childNode);

                            // find all adjectives
                            var adjectives = FindAdjectives(childNode);

                            var nounPhrase = new NounPhrase
                            {
                                Noun = noun,
                                Adjectives = adjectives
                            };

                            result.Add(nounPhrase);
                        }
                    }

                    var previousResult2 = FindNounPhrases(childNode);

                    if (previousResult2 != null)
                    {
                        result.AddRange(previousResult2);
                    }
                }

                return result;
            }

            private List<string> FindAdjectives(Tree childNode)
            {
                var result = new List<string>();

                foreach (var nounPhraseChild in childNode.children())
                {
                    if (nounPhraseChild.value() == "JJ")
                    {
                        result.Add(nounPhraseChild.firstChild().toString());
                    }
                }

                return result;
            }

            private string FindNoun(Tree childNode)
            {
                foreach (var nounPhraseChild in childNode.children())
                {
                    if (nounPhraseChild.value() == "NN" || nounPhraseChild.value() == "NNP")
                    {
                        return nounPhraseChild.firstChild().toString();
                    }
                }

                return "";
            }

            private bool IsAnyChildNodeANounPhrase(Tree childNode)
            {
                // not sure if we need to go deeper than one level
                foreach (var nounPhraseChild in childNode.children())
                {
                    if (nounPhraseChild.value() == "NP")
                    {
                        // skip the current noun phrase, it contains one or more sub-noun phrases
                        return true;
                    }
                }

                return false;
            }

            public Tree FindNounPhraseOld(Tree tree)
            {
                if (tree == null)
                {
                    return null;
                }

                foreach (var childNode in tree.children())
                {
                    if (childNode.value() == "NP")
                    {
                        return childNode;
                    }

                    var result = FindNounPhraseOld(childNode);

                    if (result != null)
                    {
                        return result;
                    }
                }

                return null;
            }
        }
    }
}
