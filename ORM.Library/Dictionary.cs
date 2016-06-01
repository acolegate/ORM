using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using ORM.Library.HelperFunctions;

namespace ORM.Library
{
	internal static class Dictionary
	{
		private const RegexOptions CaseInsensitiveRegexOptions = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;
		private const string ContiguousLettersPattern = "[a-z]+";
		private const string ReservedWordSuffix = "SafeName";
		private static Dictionary<byte, HashSet<string>> _customWordsByLength;
		private static byte _largestCustomWordLength;
		private static byte _largestStandardWordLength;
		private static HashSet<string> _reservedWords;
		private static byte _smallestCustomWordLength;
		private static byte _smallestStandardWordLength;
		private static Dictionary<byte, HashSet<string>> _standardWordsByLength;

		static Dictionary()
		{
			PopulateWordCollections();
		}

      /// <summary>Makes the name of the CLR.</summary>
      /// <param name="text">The text.</param>
      /// <returns>The CLR Name</returns>
		public static string MakeClrName(string text)
		{
			if (Settings.Instance.ProjectSettings.ForcePascalCase)
			{
				// force to pascalcase

				text = text.Trim().ToLower();
				text = FilterChars(text);

				byte lengthOfLongestContiguousWord = FindLengthOfLongestContiguousWord(text);

				int replacementNumber = 0;
				HashSet<string> replacements = new HashSet<string>();

				bool foundAWord;

				byte currentWordSize = lengthOfLongestContiguousWord > _largestCustomWordLength ? _largestCustomWordLength : lengthOfLongestContiguousWord;
				while (currentWordSize >= _smallestCustomWordLength)
				{
					foundAWord = false;

					if (_customWordsByLength.ContainsKey(currentWordSize))
					{
						foreach (string word in _customWordsByLength[currentWordSize])
						{
							if (text.Contains(word))
							{
								text = text.Replace(word, "{" + replacementNumber + "}");
								replacements.Add(Text.CapitaliseFirstLetter(word));
								currentWordSize = FindLengthOfLongestContiguousWord(text);
								replacementNumber++;
								foundAWord = true;

								if (currentWordSize < _smallestCustomWordLength)
								{
									break;
								}
							}
						}

						if (foundAWord == false)
						{
							currentWordSize--;
						}
					}
					else
					{
						// there are no words with this number of letters - try the next size down
						currentWordSize--;
					}
				}

				currentWordSize = lengthOfLongestContiguousWord > _largestStandardWordLength ? _largestStandardWordLength : lengthOfLongestContiguousWord;
				while (currentWordSize >= _smallestStandardWordLength)
				{
					foundAWord = false;

					// custom dictionary words - process these first as they have priority
					if (_customWordsByLength.ContainsKey(currentWordSize))
					{
						foreach (string word in _customWordsByLength[currentWordSize])
						{
							if (text.Contains(word))
							{
								text = text.Replace(word, "{" + replacementNumber + "}");
								replacements.Add(Text.CapitaliseFirstLetter(word));
								currentWordSize = FindLengthOfLongestContiguousWord(text);
								replacementNumber++;
							}
						}
					}

					// standard dictionary words
					if (_standardWordsByLength.ContainsKey(currentWordSize))
					{
						foreach (string word in _standardWordsByLength[currentWordSize])
						{
							if (text.Contains(word))
							{
								text = text.Replace(word, "{" + replacementNumber + "}");
								replacements.Add(Text.CapitaliseFirstLetter(word));
								currentWordSize = FindLengthOfLongestContiguousWord(text);
								replacementNumber++;
								foundAWord = true;

								if (currentWordSize < _smallestStandardWordLength)
								{
									break;
								}
							}
						}

						if (foundAWord == false)
						{
							currentWordSize--;
						}
					}
					else
					{
						// there are no words with this number of letters - try the next size down
						currentWordSize--;
					}
				}

				// Now capitalise all remaining contiguous Words
				text = CapitaliseAllContiguousWords(text);

				// now put capitalised versions of the words back into place
				text = string.Format(text, replacements.ToArray());

				if (_reservedWords.Contains(text.ToLower()))
				{
					// This is a reserved word
					// suffix it with something to make it valid
					text += ReservedWordSuffix;
				}
			}
			else
			{
				// don't force to pascalcase - just remove the invalid chars
				text = FilterChars(text);
			}

			return text;
		}

		/// <summary>Adds the word to in memory custom dictionary.</summary>
		/// <param name="word">The word.</param>
		private static void AddWordToInMemoryCustomDictionary(string word)
		{
			byte wordLength = (byte)word.Length;

			if (wordLength > 0)
			{
				if (wordLength > _largestCustomWordLength)
				{
					_largestCustomWordLength = wordLength;
				}

				if (wordLength < _smallestCustomWordLength)
				{
					_smallestCustomWordLength = wordLength;
				}

				if (_customWordsByLength.ContainsKey(wordLength) == false)
				{
					_customWordsByLength.Add(wordLength, new HashSet<string>());
				}

				_customWordsByLength[wordLength].Add(word);
			}
		}

		/// <summary>Adds the word to in memory standard dictionary.</summary>
		/// <param name="word">The word.</param>
		private static void AddWordToInMemoryStandardDictionary(string word)
		{
			byte wordLength = (byte)word.Length;
			if (wordLength > 0)
			{
				if (wordLength > _largestStandardWordLength)
				{
					_largestStandardWordLength = wordLength;
				}

				if (wordLength < _smallestStandardWordLength)
				{
					_smallestStandardWordLength = wordLength;
				}

				if (_standardWordsByLength.ContainsKey(wordLength) == false)
				{
					_standardWordsByLength.Add(wordLength, new HashSet<string>());
				}

				_standardWordsByLength[wordLength].Add(word);
			}
		}

		/// <summary>Capitalises all contiguous words.</summary>
		/// <param name="text">The text.</param>
		/// <returns>A string with all contiguous words capitalised</returns>
		/// [DebuggerStepThrough]
		private static string CapitaliseAllContiguousWords(string text)
		{
			const string ValidCharsPatters = ContiguousLettersPattern;

			string value;
			foreach (Match match in Regex.Matches(text, ValidCharsPatters, CaseInsensitiveRegexOptions))
			{
				value = match.Value;

				text = text.Substring(0, match.Index) + Text.CapitaliseFirstLetter(value) + text.Substring(match.Index + value.Length);
			}

			return text;
		}

		/// <summary>Filters the chars.</summary>
		/// <param name="text">The text.</param>
		/// <returns>Only valid chars</returns>
		/// [DebuggerStepThrough]
		private static string FilterChars(string text)
		{
			const string InvalidLeadingCharsPattern = "^[^a-z]+";
			const string InvalidCharsPattern = "[^a-z0-9]";

			// remove invalid leading chars
			text = Regex.Replace(text, InvalidLeadingCharsPattern, string.Empty, CaseInsensitiveRegexOptions);

			// remove invalid middle chars
			text = Regex.Replace(text, InvalidCharsPattern, string.Empty, CaseInsensitiveRegexOptions);

			if (text.Length == 0)
			{
				// there are no valid characters in this string
				// use a guid instead
				text = Guid.NewGuid().ToString("D");
			}

			return text;
		}

		/// <summary>Finds the length of the longest contiguous word.</summary>
		/// <param name="text">The text.</param>
		/// <returns>The length of the longest contiguous word</returns>
		/// [DebuggerStepThrough]
		private static byte FindLengthOfLongestContiguousWord(string text)
		{
			return (byte)(from Match match in Regex.Matches(text, ContiguousLettersPattern, CaseInsensitiveRegexOptions)
			              select match.Value.Length).Concat(new[] { 0 }).Max();
		}

		/// <summary>Populates the word collections.</summary>
		/// [DebuggerStepThrough]
		private static void PopulateWordCollections()
		{
			_standardWordsByLength = new Dictionary<byte, HashSet<string>>();
			_customWordsByLength = new Dictionary<byte, HashSet<string>>();
			_reservedWords = new HashSet<string>();

			_largestStandardWordLength = byte.MinValue;
			_smallestStandardWordLength = byte.MaxValue;
			_largestCustomWordLength = byte.MinValue;
			_smallestCustomWordLength = byte.MaxValue;

			// STANDARD WORDS
			using (StreamReader file = new StreamReader(Settings.Instance.ProjectSettings.MainDictionaryFilePathAndFilename))
			{
				string word;

				while ((word = file.ReadLine()) != null)
				{
					AddWordToInMemoryStandardDictionary(word);
				}

				file.Close();
			}

			// CUSTOM DICTIONARY
			using (StreamReader file = new StreamReader(Settings.Instance.ProjectSettings.CustomDictionaryFilePathAndFilename))
			{
				string word;

				while ((word = file.ReadLine()) != null)
				{
					AddWordToInMemoryCustomDictionary(word.Trim().ToLower());
				}

				file.Close();
			}

			// RESERVED WORDS
			using (StreamReader file = new StreamReader(Settings.Instance.ProjectSettings.ReservedWordsFilePathAndFilename))
			{
				string word;

				while ((word = file.ReadLine()) != null)
				{
					_reservedWords.Add(word);
				}

				file.Close();
			}
		}
	}
}