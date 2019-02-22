using System;
using System.IO;
using System.Net.Mime;

namespace N_GramTextGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "";
            try
            {
                text = File.ReadAllText("Text.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("No input file found. Place Text.txt file in application folder. \n Press any key to exit...");
                Console.Read();
                Environment.Exit(0);
            }
            var sentences = SentencesParser.ParseSentences(text);
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences);

            while (true)
            {
                Console.WriteLine("Enter first part of the phrase");
                var beginning = Console.ReadLine();

                Console.WriteLine("How long should be this phrase?");
                int.TryParse(Console.ReadLine(), out var sentenceLength);

                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGenerator.ContinuePhrase(frequency, beginning.ToLower(), sentenceLength);
                Console.WriteLine(phrase);
            }
        }
    }
}