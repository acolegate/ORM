using System.IO;

namespace ORM.Library.HelperFunctions
{
	public static class Io
	{
		public static void WriteTextFile(string filename, string fileContents)
		{
			using (StreamWriter streamWriter = new StreamWriter(filename, false))
			{
				streamWriter.Write(fileContents);
				streamWriter.Flush();
				streamWriter.Close();
			}
		}
	}
}