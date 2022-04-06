using System;


// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeMadeStatic.Global


namespace CreativeTrager.ImageAugmentator.Demo;
public sealed class ConsolePrinter 
{
	public static ConsolePrinter Create() => new ();
	private ConsolePrinter() { /* empty */ }

	public void PrintDefault(string message) => Print(message, ConsoleColor.White);
	public void PrintSuccess(string message) => Print(message, ConsoleColor.Green);
	public void PrintAccent (string message) => Print(message, ConsoleColor.Yellow);
	public void PrintError  (string message) => Print(message, ConsoleColor.Red);

	public void Print(string message, ConsoleColor color) 
	{
		var prevColor = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ForegroundColor = prevColor;
	}
}