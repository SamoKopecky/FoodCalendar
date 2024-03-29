﻿using System;
using System.Collections.Generic;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class OptionsHandler
    {
        private readonly Dictionary<string, string> _optionsDictionary = new Dictionary<string, string>();

        public OptionsHandler(List<string> options)
        {
            BuildOptions(options);
        }


        /// <summary>
        /// Builds the option dictionary for a specific list of options.
        /// Option dictionary can then be used to choose from the given options
        /// by typing the corresponding character of the option.
        /// </summary>
        /// <param name="options">List of options to generate a dictionary from.</param>
        public void BuildOptions(List<string> options)
        {
            var alphabetKeys = GenerateOptions(options.Count);
            using var keysEnumerator = alphabetKeys.GetEnumerator();
            foreach (var option in options)
            {
                keysEnumerator.MoveNext();
                _optionsDictionary.Add($"{keysEnumerator.Current}", option);
            }
        }

        /// <summary>
        /// Gets the chosen option from the user and then returns the
        /// chosen option.
        /// </summary>
        /// <param name="actionDescription">Description of the action the user is doing.</param>
        /// <returns>The chosen option.</returns>
        public string HandleOptions(string actionDescription)
        {
            Console.Clear();
            if (actionDescription != "")
            {
                Console.WriteLine(actionDescription);
                Console.WriteLine(new string('-', actionDescription.Length));
            }

            foreach (var (key, value) in _optionsDictionary)
            {
                Console.WriteLine($"{key}) {value}");
            }

            Console.Write("\nChoose your option: ");
            var selectedOption = Console.ReadLine();
            while (selectedOption == null || !_optionsDictionary.ContainsKey(selectedOption))
            {
                Console.WriteLine("Option not found, please enter again.");
                Console.Write("Choose your option: ");
                selectedOption = Console.ReadLine();
            }

            return _optionsDictionary[selectedOption];
        }

        /// <summary>
        /// Generate a required number of alphabet characters to
        /// then later bind with the actual options.
        /// </summary>
        /// <param name="length">Number of characters to generate.</param>
        /// <returns>The generated characters.</returns>
        private IEnumerable<string> GenerateOptions(int length)
        {
            var options = new string[length];
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            using var enumerator = alphabet.GetEnumerator();
            var singleCharOptLen = length;
            if (length > 26) singleCharOptLen = 26;
            for (var i = 0; i < singleCharOptLen; i++)
            {
                options[i] = $"{alphabet[i]}";
            }

            if (length < 26) return options;
            for (var i = 26; i < length; i++)
            {
                if (i % 26 == 0) enumerator.MoveNext();
                options[i] = $"{enumerator.Current}{alphabet[i % 26]}";
            }

            return options;
        }
    }
}