using System;
using System.Collections.Generic;
using System.Linq;

//https://stanfordnlp.github.io/CoreNLP/
namespace NLPAndTheWhiteWhale
{
    class Program
    {
        static void Main(string[] args)
        {
            var books = new Books();
            //books.SimpleSearch("white whale");
            //books.SimpleSearchUsingAnd("white whale");

            var smartBooks = new SmartBookSearch();
            smartBooks.Search("white whale");

            Console.ReadKey();
        }
    }
}
