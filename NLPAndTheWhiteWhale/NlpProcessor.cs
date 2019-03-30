using System;
using System.Collections.Generic;
using System.IO;
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
            return treeFind.FindNounPhrase(tree);
        }

        public class TreeFind
        {
            public string FindNounPhrase(Tree tree)
            {
                if (tree == null)
                {
                    return "";
                }

                foreach (var childNode in tree.children())
                {
                    if (childNode.value() == "NP")
                    {
                        return childNode.toString();
                    }

                    var result = FindNounPhrase(childNode);

                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }

                return "";
            }
        }
    }
}
