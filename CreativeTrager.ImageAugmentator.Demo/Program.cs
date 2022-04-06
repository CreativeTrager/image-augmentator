using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Augmentator = CreativeTrager.ImageAugmentator.Model.ImageAugmentator;


// ReSharper disable RedundantAssignment
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable TooWideLocalVariableScope


namespace CreativeTrager.ImageAugmentator.Demo;
internal static class Program 
{
	private static readonly ConsolePrinter Printer;

	static Program() 
	{
		Printer = ConsolePrinter.Create();
	}

	[STAThread]
	public static void Main(string[] args) 
	{
		Printer.PrintDefault(message: $"Please select picture to be augmented...");

		var image = default(Image);
		var filePath = default(string);
		var fileExtension = default(string);
		{
			var getImageSuccess = default(bool);
			while(getImageSuccess is false) 
			{
				filePath = SelectFilePath();
				fileExtension = Path.GetExtension(filePath);
				getImageSuccess = TryGetImageByPath(filePath, out image);
			}
		}

		var imageVariations = new Augmentator().Run(image!);

		var originFileName = Path.GetFileNameWithoutExtension(filePath);
		var originDirectoryPath = Path.GetDirectoryName(filePath);
		var destinationDirectoryPath = Path.Combine(originDirectoryPath!, $"{originFileName}-augmented");

		Directory.CreateDirectory(destinationDirectoryPath);

		foreach(var (variationId, variation) in imageVariations) 
		{
			var fileName = $"{originFileName}-{variationId}";
			var fullFileName = Path.Combine(destinationDirectoryPath, $"{fileName}{fileExtension}");
			variation.Save(fullFileName);
			Thread.Sleep(millisecondsTimeout: 100);
		}

		Printer.PrintDefault(message: $"Augmentation has been successfully completed.");
		Printer.PrintDefault(message: $"Press <Enter> to exit the program...");
		Console.ReadLine();
	}

	private static string SelectFilePath() 
	{
		var filePath = string.Empty;
		var fileSelectSuccess = false;

		while(!fileSelectSuccess) 
		{
			var selectorResult = DialogResult.Cancel;
			var fileSelector = new FileSelector {
				Title = "Open image to augmentation...", Hint = "Image",
				Extensions = new [] { ".png", ".jpg", ".jpeg", ".bmp" }
			};

			try 
			{
				fileSelector.Select();
				selectorResult = fileSelector.Result;
			}
			catch(Exception e) 
			{
				Printer.PrintError(message: $"{e}");
			}

			if( selectorResult != DialogResult.OK &&
				selectorResult != DialogResult.Yes) 
			{
				Printer.PrintError(message: $"Image hasn't been selected.");
				Printer.PrintAccent(message: $"Please select an image to be augmented.");
				Thread.Sleep(millisecondsTimeout: 1000);
			}
			else 
			{
				filePath = fileSelector.SelectedFilePath;
				fileSelectSuccess = true;
			}
		}

		return filePath;
	}
	private static bool TryGetImageByPath(string path, out Image? result) 
	{
		var success = default(bool?);

		try 
		{
			result = GetImageByPath(path);
			success = true;
		}
		catch(Exception) 
		{
			result = null;
			success = false;
		}

		return success.Value;
	}
	private static Image GetImageByPath(string value) 
	{
		var image = default(Image);

		try 
		{
			image = Image.FromFile(value);
		}
		catch(ArgumentException e) 
		{
			Printer.PrintError(message: $"File path error.\n{e}");
			throw;
		}
		catch(FileNotFoundException e) 
		{
			Printer.PrintError(message: $"File not found.\n{e}");
			throw;
		}
		catch(Exception e) 
		{
			Printer.PrintError(message: $"Unexpected error occured.\n{e}");
			throw;
		}

		return image;
	}
}