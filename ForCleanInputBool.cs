using System;
public class ForCleanInputBool
{
	/// <summary>
	/// To set value by user without error
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	public static bool GetBoolean(string message)
	{
		Console.WriteLine(message);
		while (true)
		{
			string inputString = Console.ReadLine().ToLower();
			switch (inputString)
			{
				case "false":
					return false;
				case "true":
					return true;
				case "1":
					return true;
				case "0":
					return false;
				default:
					Console.WriteLine("The value is incorrect!");
					break;
                    }
                }
                
	}
}