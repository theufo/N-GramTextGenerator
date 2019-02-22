using System.Collections.Generic;
using System.Linq;

namespace N_GramTextGenerator
{
    public static class FrequencyAnalysis
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            foreach (var str in text)
            {
                for (int i = 0; i < str.Count - 1; i++)
                    AddNewValues(result, str[i], str[i + 1]);

                for (int i = 0; i < str.Count - 2; i++)
                    AddNewValues(result, $"{str[i]} {str[i + 1]}", str[i + 2]);
            }

            return LeaveOnlyMostFrequentWord(result);
        }

        private static void AddNewValues(Dictionary<string, Dictionary<string, int>> result, string key,
            string newValue)
        {
            if (result.ContainsKey(key))
            {
                var valueDictionary = result[key];
                if (valueDictionary.ContainsKey(newValue))
                    valueDictionary[newValue]++;
                else
                    valueDictionary.Add(newValue, 1);
            }
            else
                result.Add(key, new Dictionary<string, int>() { { newValue, 1 } });
        }

        private static Dictionary<string, string> LeaveOnlyMostFrequentWord(
            Dictionary<string, Dictionary<string, int>> dictionary)
        {
            var result = new Dictionary<string, string>();
            foreach (var keyValuePair in dictionary)
            {
                var valuesDictionary = keyValuePair.Value.OrderByDescending(x => x.Value)
                    .ToDictionary(x => x.Key, x => x.Value);
                var res = valuesDictionary.FirstOrDefault();

                foreach (var valuePair in valuesDictionary.Where(x => x.Value == res.Value).ToList())
                {
                    var compareResult = string.CompareOrdinal(res.Key, valuePair.Key);
                    if (compareResult > 0)
                        res = valuePair;
                }

                result.Add(keyValuePair.Key, res.Key);
            }

            return result;
        }
    }
}