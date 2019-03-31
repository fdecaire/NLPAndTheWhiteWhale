using System;
using System.Collections.Generic;
using System.Linq;

namespace NLPAndTheWhiteWhale
{
    public class SmartBookSearch
    {
        public List<SmartBook> SmartBooks = new List<SmartBook>();

        public SmartBookSearch()
        {
            SmartBooks.Add(new SmartBook("Moby Dick"));
            SmartBooks.Add(new SmartBook("The Little Engine That Could"));
            SmartBooks.Add(new SmartBook("The Whale with the White Fin"));
            SmartBooks.Add(new SmartBook("The White Fish"));
            SmartBooks.Add(new SmartBook("The White Car"));
            SmartBooks.Add(new SmartBook("How to Paint With Black and White"));
            SmartBooks.Add(new SmartBook("Black and White Photography"));
            SmartBooks.Add(new SmartBook("My Little Pony"));
            SmartBooks.Add(new SmartBook("Do Androids Dream of Electric Sheep?"));
            SmartBooks.Add(new SmartBook("Something Wicked This Way Comes"));
            SmartBooks.Add(new SmartBook("I Was Told There'd Be Cake"));
            SmartBooks.Add(new SmartBook("The Curious Incident of the Dog in the Night-Time"));
            SmartBooks.Add(new SmartBook("The Hollow Chocolate Bunnies of the Apocalypse"));
            SmartBooks.Add(new SmartBook("Eats, Shoots & Leaves: The Zero Tolerance Approach to Punctuation"));
            SmartBooks.Add(new SmartBook("To Kill a Mockingbird"));
            SmartBooks.Add(new SmartBook("The Unbearable Lightness of Being"));
            SmartBooks.Add(new SmartBook("A Clockwork Orange"));
            SmartBooks.Add(new SmartBook("Midnight in the Garden of Good and Evil"));
            SmartBooks.Add(new SmartBook("The Perks of Being a Wallflower"));
            SmartBooks.Add(new SmartBook("The Man Without Qualities"));
            SmartBooks.Add(new SmartBook("Cloudy With a Chance of Meatballs"));
            SmartBooks.Add(new SmartBook("Where the Wild Things Are"));
            SmartBooks.Add(new SmartBook("One Hundred Years of Solitude"));
            SmartBooks.Add(new SmartBook("The Elephant Tree"));
            SmartBooks.Add(new SmartBook("One Flew Over the Cuckoo's Nest"));
            SmartBooks.Add(new SmartBook("Requiem for a Dream"));
            SmartBooks.Add(new SmartBook("A Wrinkle in Time (Time Quintet, #1)"));
            SmartBooks.Add(new SmartBook("The Whale"));
            SmartBooks.Add(new SmartBook("White Whale"));
        }

        public void Search(string searchText)
        {
            var results = new List<TitleRanking>();

            var decodedSearchText = NlpProcessor.FindNounePhrases(searchText);

            // search for all records with the noun, return the count of adjectives for each
            foreach (var title in SmartBooks)
            {
                var totalMatchingAdjectives = title.MatchRecord(decodedSearchText[0].Noun, decodedSearchText[0].Adjectives);
                if (totalMatchingAdjectives > -1)
                {
                    results.Add(new TitleRanking
                    {
                        Title = title.Title,
                        MatchingAdjectives = totalMatchingAdjectives
                    });
                }
            }

            Console.WriteLine(Environment.NewLine+ Environment.NewLine);
            foreach (var result in results.OrderByDescending(n => n.MatchingAdjectives))
            {
                Console.WriteLine(result.Title);
            }
        }
    }
}
