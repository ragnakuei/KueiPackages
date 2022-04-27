using System;
using System.Collections.Generic;
using System.Linq;

namespace KueiPackages.Services;

public class RandomString
{
    private readonly List<Char> _availableChars;
    private readonly Random     _random;

    public RandomString(bool   useLowerCase    = false,
                        bool   userUpperCase   = false,
                        char[] additionalChars = null)
    {
        _availableChars = new List<char>();
        _random         = new Random();


        if (useLowerCase)
        {
            var lowerCaseCharStartNo = 97;
            _availableChars.AddRange(Enumerable.Range(0, 26)
                                               .Select(i => (char)(i + lowerCaseCharStartNo)));
        }

        if (userUpperCase)
        {
            var upperCaseCharStartNo = 65;
            _availableChars.AddRange(Enumerable.Range(0, 26)
                                               .Select(i => (char)(i + upperCaseCharStartNo)));
        }

        if (additionalChars?.Length > 0)
        {
            _availableChars.AddRange(additionalChars);
        }
    }

    public string Generate(int length)
    {
        return new string(GenerateRandomChars(length).ToArray()).Trim();
    }

    private IEnumerable<char> GenerateRandomChars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var randomIndex = _random.Next(0, _availableChars.Count);
            yield return _availableChars[randomIndex];
        }
    }
}
