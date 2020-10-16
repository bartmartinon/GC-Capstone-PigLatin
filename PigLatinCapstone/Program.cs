using System;
using System.Linq;

namespace PigLatinCapstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Pig Latin Translator!");

            // Application Loop
            bool done = false;
            while (!done)
            {
                // Prompt User to enter a sentence into the console
                string sentence;
                Console.Write("Please enter text to translate: ");
                sentence = Console.ReadLine();
                if (string.IsNullOrEmpty(sentence))
                {
                    MakeLineSpace(1);
                    Console.WriteLine("Looks like you didn't enter anything. How about an example?");
                    sentence = "I will make sure to enter a proper sentence next time.";
                }
                Console.WriteLine("Entry: " + sentence);
                MakeLineSpace(1);

                // Split the sentence into individual words as strings
                string[] words = sentence.Split(" ");

                // Word Translation Loop
                // Take each word and translate into its Pig Latin equivalent, and add the word to a translated list of words.
                string[] pigWords = new string[words.Length];
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];
                    string pigWord = TranslateWord(word);
                    pigWords[i] = pigWord; // Add translated word to array
                }

                // Set up a new variable to store the individual words to create the new translated sentence.
                string pigSentence = "";
                foreach (string s in pigWords)
                {
                    pigSentence = pigSentence + s + " ";
                }
                pigSentence = pigSentence.Trim();

                // Display Results
                Console.WriteLine("Translation complete!");
                MakeLineSpace(1);
                Console.WriteLine("Original:");
                Console.WriteLine(sentence);
                MakeLineSpace(1);
                Console.WriteLine("Translated:");
                Console.WriteLine(pigSentence);
                MakeLineSpace(1);

                // Prompt user if they want to continue using the program. 
                // If yes, then let the loop iterate. Otherwise, stop the loop by setting done to true.
                Console.Write($"Would you like to translate something else? (y/n) ");
                if (!(Console.ReadLine().ToLower().Trim()).Equals("y"))
                {
                    Console.WriteLine($"Thank you for using the Pig Latin Translator! Have a nice day!");
                    done = true;
                }
                else { Console.WriteLine("Reseting!"); }
                MakeLineSpace(1);
            }
        }

        // Takes a word as input and returns the appropriately translated word
        public static string TranslateWord(string word)
        {
            string pigWord = word;
            char firstLetter = word[0]; // Grab the first letter of the current word
            char lastChar = word[word.Length - 1]; // Grab the last character of the current word

            // Check if the last character is a punctuation mark
            if (Char.IsPunctuation(lastChar))
            {
                word = word.Substring(0, word.Length - 1); // Remove the punctuation mark, it will be returned later
            }

            // Check if the string contains either a numerical value (digits) or any symbols that warrant the translation to be
            // ignored to avoid scrambling information such as numbers, email addresses and prices.
            if (word.Any(char.IsDigit) || word.Any(char.IsSymbol) || word.Contains('@'))
            {
                pigWord = word;
            }
            // Check if the first letter of the word is a vowel. If so, add "way" to the end
            else if (IsAVowel(Char.ToLower(firstLetter)))
            {
                pigWord = word + "way";

                // If a punctuation mark was found and removed, add it back to the translated word
                if (Char.IsPunctuation(lastChar))
                {
                    pigWord += lastChar;
                }
            }
            else
            {
                // Convert the word string into a char array
                char[] wordChars = word.ToCharArray();

                // Convert the original word to lowercase
                word = word.ToLower();

                // Check if the first letter is capitalized
                bool isFirstLetterUpper = Char.IsUpper(wordChars[0]);

                // Vowel Check Loop
                // Go through the character array and keep track of the index number of the first vowel found
                for (int j = 0; j < wordChars.Length; j++)
                {
                    if (IsAVowel(Char.ToLower(wordChars[j])))
                    {
                        // Once the first vowel is found, split the original string into two substrings around that index.
                        // Add "ay" at the end of the new latter substring.
                        string sub1 = word.Substring(0, j) + "ay";
                        string sub2 = word.Substring(j);

                        // If the first letter of the original string was capitalized, capitalize the first latter of the new former substring.
                        if (isFirstLetterUpper)
                        {
                            sub2 = sub2.First().ToString().ToUpper() + sub2.Substring(1);
                        }

                        // Form the new translated word, and then break the Vowel Check Loop
                        pigWord = sub2 + sub1;

                        // If a punctuation mark was found and removed, add it back to the translated word
                        if (Char.IsPunctuation(lastChar))
                        {
                            pigWord += lastChar;
                        }
                        break; // Break the loop, no longer need to check other letters (if not there, would cause if multiple vowels are found)
                    }
                }
            }
            // Return the translated word
            return pigWord;
        }
        
        // Return true if input is a vowel, otherwise return false.
        public static bool IsAVowel(char letter)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            foreach ( char c in vowels )
            {
                if (letter.Equals(c))
                {
                    return true;
                }
            }
            return false;
        }

        // Displays empty lines in the console, for the sake of formatting and visuals
        public static void MakeLineSpace(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine(" ");
            }
        }
    }
}
