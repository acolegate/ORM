using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ORM.Library.HelperFunctions
{
   public static class Text
   {
      /// <summary>Capitalises the first letter.</summary>
      /// <param name="word">The word.</param>
      /// <returns>A capitalised version of the word</returns>
      [DebuggerStepThrough]
      public static string CapitaliseFirstLetter(string word)
      {
         return char.ToUpper(word[0]) + word.Substring(1);
      }

      /// <summary>Lowercases the first letter.</summary>
      /// <param name="text">The text.</param>
      /// <returns>The text with the first letter lowercased</returns>
      [DebuggerStepThrough]
      public static string LowercaseFirstLetter(string text)
      {
         return text[0].ToString().ToLower() + text.Substring(1);
      }

      /// <summary>Makes the text safe for an attribute description.</summary>
      /// <param name="text">The text.</param>
      /// <returns>The safe text</returns>
      public static string MakeSafeForAttributeDescription(string text)
      {
         return text.Trim().Replace("\"", "'");
      }

      /// <summary>
      /// Makes a unique name within the supplied list of existing names.
      /// </summary>
      /// <param name="existingNames">The existing names.</param>
      /// <param name="duplicateName">Name of the duplicate.</param>
      /// <returns>A unique name with a numeric suffix</returns>
      public static string MakeUniqueName(IEnumerable<string> existingNames, string duplicateName)
      {
         const string NumericSufficPattern = "^(?<prefix>.+?)(?<suffix>[0-9]+)?$";

         Match originalMatch = Regex.Match(duplicateName, NumericSufficPattern);
         string originalPrefix = originalMatch.Success ? originalMatch.Groups["prefix"].Value : string.Empty;

         int highestNumericSuffix = 1;

         Match columnMatch;
         string columnPrefix;
         string columnSuffix;

         int number;

         foreach (string existingName in existingNames)
         {
            columnMatch = Regex.Match(existingName, NumericSufficPattern);
            if (columnMatch.Success)
            {
               columnPrefix = columnMatch.Groups["prefix"].Value;
               columnSuffix = columnMatch.Groups["suffix"].Value;

               if (columnPrefix == originalPrefix && string.IsNullOrEmpty(columnSuffix) == false)
               {
                  number = int.Parse(columnSuffix);
                  if (number > highestNumericSuffix)
                  {
                     highestNumericSuffix = number;
                  }
               }
            }
         }

         return originalPrefix + (highestNumericSuffix + 1);
      }
   }
}
