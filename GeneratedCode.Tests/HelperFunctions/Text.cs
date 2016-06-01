using System;
using System.Text;

namespace GeneratedCode.Tests.HelperFunctions
{
	internal static class Text
	{
		/// <summary>Generates a random string</summary>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentOutOfRangeException">length;length cannot be less than zero.</exception>
		/// <exception cref="System.ArgumentException">allowedChars may not be empty.</exception>
		public static string RandomString(int length)
		{
			char[] allowedCharSet = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			int allowedCharSetHighestIndex = allowedCharSet.Length;

			Random random = new Random();
			StringBuilder result = new StringBuilder(string.Empty);
			for (int i = 0; i < length; i++)
			{
				result.Append(allowedCharSet[random.Next(allowedCharSetHighestIndex)]);
			}
			return result.ToString();
		}
	}
}