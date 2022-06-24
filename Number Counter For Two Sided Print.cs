using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Program
{
	enum PaperSize{A2, A3, A4, A5, A6, letter, legal, tabloid, statement}
	
	private static int FinalNum;
	private static int FirstNum;
	private static List<int> EvenNumbers = new List<Int32>(0);
	private static List<int> OddNumbers = new List<Int32>(0);
	private static string _printer = "";
	private static bool canPrintFile = false;
	private static string oddPages = "";
	private static string evenPages = "";
	private static string oneSicePage = "";
	private static string paperSize = "";
	private static string filePath = "";

	private static int OneSicePageNumber = 0;
	static void Main()
	{
		GetValue();
		SetValues();
		ShowValues();
		if(canPrintFile){PrintFile();}
		RestApp();
		
	}
	
	static void PrintFile(){
		PrintOddPages();
		PrintEvenPages();
		PrintOneSicePage();
	}
	
	static void PrintOddPages(){
		WaitForEnter("Press enter to print odd pages...");
		ProcessToPrint(oddPages);
	}
	
	static void PrintEvenPages(){
		WaitForEnter("Press enter to print even pages...");
		ProcessToPrint(evenPages);
	}
	
	static void PrintOneSicePage(){
		if(oneSicePage == "None"){
			return;
		}
		
		WaitForEnter("Press enter to print sliced page...");
		ProcessToPrint(oneSicePage);
	}
	
	static void ProcessToPrint(string pagesToPrint){
		Process proc = new Process();
            	proc.StartInfo.FileName = "SumatraPDF-3.3.3-64.exe";
            	//proc.StartInfo.Verb = "runas";
            	proc.StartInfo.Arguments = string.Format("-print-to \"{0}\" -print-settings \"paper={1},{2}\" \"{3}\"",_printer,paperSize,pagesToPrint,filePath);
           	proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
           	//proc.StartInfo.RedirectStandardError = true;
           	proc.Start();
	}
	
	static void WaitForEnter(string message){
		Console.WriteLine(message);
		Console.ReadLine();
	}

	static void RestApp()
	{
		Console.WriteLine("Press Enter to Restart App...");
		Console.ReadLine();
		Console.Clear();
		RestValues();
		Main();
	}

	static void RestValues()
	{
		FinalNum = 0;
		FinalNum = 0;
		EvenNumbers = new List<Int32>(0);
		OddNumbers = new List<Int32>(0);
		_printer = "";
		canPrintFile = false;
		OneSicePageNumber = 0;
		oddPages = "";
		evenPages = "";
		oneSicePage = "";
		paperSize = "";
		filePath = "";
	}

	static void ShowValues()
	{
		oddPages = ShowOddNumberValuse();
		evenPages = ShowEvenNumberValuse();
		oneSicePage = ShowOneSicePageNumber();
	}

	static string ShowEvenNumberValuse()
	{
		string StringToShow = string.Empty;
		foreach (int CurrentItem in EvenNumbers)
		{
			StringToShow += CurrentItem.ToString();
			if(EvenNumbers[EvenNumbers.Count-1] != CurrentItem){
				StringToShow += ",";
			}
		}

		Console.WriteLine("\nEvenValues:");
		Console.WriteLine("\t" + StringToShow);
		
		return StringToShow;
	}
	
	static string ShowOddNumberValuse()
	{
		string StringToShow = string.Empty;
		foreach (int CurrentItem in OddNumbers)
		{
			StringToShow += CurrentItem.ToString();
			if(OddNumbers[OddNumbers.Count-1] != CurrentItem){
				StringToShow += ",";
			}
		}
		Console.WriteLine("\nOddValues:");
		Console.WriteLine("\t" + StringToShow);
		return StringToShow;
	}

	static string ShowOneSicePageNumber()
	{
		string StringToShow = string.Empty;
		if(OneSicePageNumber == 0)
			return "None";
		StringToShow = OneSicePageNumber.ToString();
		Console.WriteLine("\nOneSicePageNumber:");
		Console.WriteLine("\t" + StringToShow);
		
		return StringToShow;
	}

	static void SetValues()
	{
		SetValuesOddNumber();
		SetValuesEvenNumber();
	}

	static void SetValuesEvenNumber()
	{
		
		for (int i = FinalNum; i >= FirstNum-1; i--)
		{
			if(!CheckEvenNumber(i))
				continue;
			int NumberToAdd = i;
			if (NumberToAdd <= FinalNum)
			{
				if(NumberToAdd != 0)
					EvenNumbers.Add(NumberToAdd);
			}
			
		}
	}
	
	static void SetValuesOddNumber()
	{
		for (int i = FirstNum-1; i < FinalNum; i++)
		{
			if(!CheckOddNumber(i))
				continue;
			int NumberToAdd = i;
			if (NumberToAdd < FinalNum)
			{
				if (NumberToAdd != 0)
				{
					OddNumbers.Add(NumberToAdd);
				}
			}
			if (NumberToAdd == FinalNum && (FinalNum % 2) !=0)
			{
				OddNumbers.Remove(NumberToAdd);
				OneSicePageNumber = NumberToAdd;
				break;
			}
			else if (NumberToAdd == FinalNum)
				break;
		}
	}

	static bool CheckOddNumber(int k)
	{
		if(k % 2 == 1)
			return true;
		return false;
	}
	
	static bool CheckEvenNumber(int k)
	{
		if(k % 2 == 0)
			return true;
		return false;
	}

	static void GetValue()
	{
		while(true){
			FirstNum = ForCleanInputInt.GetValue("Enter The Fist Number...");
			if(FirstNum > 0){
				break;
			}
			Console.WriteLine("This value is incorrect, enter value higher than 0.");
		}
		FinalNum = ForCleanInputInt.GetValue("Enter The Final Number...");
		canPrintFile = ForCleanInputBool.GetBoolean("Are you want to print specific pdf file?");
		
		if(canPrintFile){
			_printer = GetPrinter();
			paperSize = GetPageSize();
			
			Console.WriteLine("Enter PDF File Path");
			filePath = Console.ReadLine();
		}
	}
	
	static string GetPrinter(){
		List<string> printers = new List<string>(0);
		int numberPrinter = 0;
		foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
		{
			printers.Add(printer);
		}
		
		while(true){
			numberPrinter = ForCleanInputInt.GetValue("Enter the printer number...\n" + GetArrayToStringList(printers.ToArray()));
			if(printers.Count > numberPrinter){
				break;
			}
			
			Console.WriteLine("This printer number is unavailable");
		}
		return printers[numberPrinter];
	}
	
	static string GetArrayToStringList(string[] value){
		string toReturn = string.Empty;
		
		for(int i=0 ;i < value.Length;i++){
			toReturn += i.ToString() + "- " + value[i] + "\n";
		}
		
		return toReturn;
	}
	
	static string GetPageSize(){
		int paperNumber = ForCleanInputInt.GetValue("Enter paper size number\n"+ShowEnumList());
		
		return ((PaperSize)paperNumber).ToString();
	}
	
	/// <summary>
	/// Show list of items available in enum Mode.
	/// </summary>
	/// <returns></returns>
	private static string ShowEnumList()
	{
		string[] enumList = Enum.GetNames(typeof(PaperSize));
		string returnString = string.Empty;
		
		for (int i = 0; i < enumList.Length; i++) {
			returnString += String.Format("{0}:{1}\n", i, enumList[i]);
		}

		return returnString;
	}
}