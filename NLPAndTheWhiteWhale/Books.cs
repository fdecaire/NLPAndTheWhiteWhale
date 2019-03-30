using System;
using System.Collections.Generic;
using System.Linq;

namespace NLPAndTheWhiteWhale
{
    public class Books
    {
        public List<string> Titles = new List<string>();

        public Books()
        {
            Titles.Add("Moby Dick");
            Titles.Add("The Little Engine That Could");
            Titles.Add("The Whale with the White Fin");
            Titles.Add("The White Fish");
            Titles.Add("The White Car");
            Titles.Add("How to Paint With Black and White");
            Titles.Add("Black and White Photography");
            Titles.Add("My Little Pony");
            Titles.Add("The Whale");
            Titles.Add("Do Androids Dream of Electric Sheep?");
            Titles.Add("Something Wicked This Way Comes");
            Titles.Add("I Was Told There'd Be Cake");
            Titles.Add("The Curious Incident of the Dog in the Night-Time");
            Titles.Add("The Hollow Chocolate Bunnies of the Apocalypse");
            Titles.Add("Eats, Shoots & Leaves: The Zero Tolerance Approach to Punctuation");
            Titles.Add("To Kill a Mockingbird");
            Titles.Add("The Unbearable Lightness of Being");
            Titles.Add("A Clockwork Orange");
            Titles.Add("Midnight in the Garden of Good and Evil");
            Titles.Add("The Perks of Being a Wallflower");
            Titles.Add("The Man Without Qualities");
            Titles.Add("Cloudy With a Chance of Meatballs");
            Titles.Add("Where the Wild Things Are");
            Titles.Add("One Hundred Years of Solitude");
            Titles.Add("The Elephant Tree");
            Titles.Add("One Flew Over the Cuckoo's Nest");
            Titles.Add("Requiem for a Dream");
            Titles.Add("A Wrinkle in Time (Time Quintet, #1)");
            Titles.Add("The Whale");
            Titles.Add("White Whale");
        }

        public void SimpleSearch(string searchText)
        {
            var searchWords = searchText.Split(' ');

            var results = new List<string>();

            foreach (var word in searchWords)
            {
                results.AddRange((from t in Titles where t.ToLower().Contains(word) select t).ToList());
            }

            foreach (var result in results.Distinct())
            {
                Console.WriteLine(result);
            }
        }

        public void SimpleSearchUsingAnd(string searchText)
        {
            var searchWords = searchText.Split(' ');

            var results = new List<string>();

            foreach (var word in searchWords)
            {
                if (results.Count == 0)
                {
                    results = (from t in Titles where t.ToLower().Contains(word) select t).ToList();
                }
                else
                {
                    results = (from t in results where t.ToLower().Contains(word) select t).ToList();
                }
            }

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
