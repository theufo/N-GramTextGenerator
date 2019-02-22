using System.Collections.Generic;
using System.Linq;

namespace N_GramTextGenerator
{
    public static class SentencesParser
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = text.Split(new char[] {'.', '!', '?', ';', ':', '(', ')'}).ToList();
            foreach (var sentence in sentences)
            {
                var words = sentence.Split(new char[] {' '});

                var wordResult = ParseWords(words);

                if (wordResult.Count > 0)
                    sentencesList.Add(wordResult);
            }

            return sentencesList;
        }

        private static List<string> ParseWords(string[] words)
        {
            var result = new List<string>();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                var splitChars = new List<char>();

                foreach (var letter in word)
                    if (!char.IsLetter(letter) && letter != '\'')
                        splitChars.Add(letter);
                var tempWords = word.Split(splitChars.ToArray())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x.ToLower()).ToList();

                if (tempWords.Count < 1)
                    continue;

                result.AddRange(tempWords);
            }

            return result;
        }
    }
}