using System.Collections.Generic;
using System.Linq;

namespace N_GramTextGenerator
{
    public static class TextGenerator
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning,
            int wordsCount)
        {
            var resultWords = new List<string>();
            resultWords.AddRange(phraseBeginning.Split(' ').ToList());

            if (resultWords.Count == 0)
                return "";

            for (int i = 0; i < wordsCount; i++)
            {
                string searchKey;
                if (resultWords.Count > 1)
                {
                    searchKey = $"{resultWords[resultWords.Count - 2]} {resultWords[resultWords.Count - 1]}";

                    if (nextWords.ContainsKey(searchKey))
                    {
                        resultWords.Add(nextWords[searchKey]);
                        continue;
                    }
                }

                searchKey = resultWords[resultWords.Count - 1];

                if (nextWords.ContainsKey(searchKey))
                    resultWords.Add(nextWords[searchKey]);
                else
                    break;
            }

            return string.Join(" ", resultWords);
        }
    }
}