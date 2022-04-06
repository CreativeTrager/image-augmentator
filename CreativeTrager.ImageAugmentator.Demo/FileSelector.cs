using System;
using System.Collections.Generic;
using System.Windows.Forms;


// ReSharper disable ArrangeAccessorOwnerBody


namespace CreativeTrager.ImageAugmentator.Demo;
public sealed class FileSelector 
{
	private string _title;
	private string _hint;
	private string[] _extensions;
	private string _selectedFilePath;

	private readonly OpenFileDialog _fileDialog;
	private DialogResult _result;

	public string Title 
	{
		set 
		{
			_title = value;
			_fileDialog.Title = value;
		}
	}
	public string Hint 
	{
		set 
		{
			_hint = value;
			_fileDialog.Filter = FormatFilter(value, _extensions);
		}
	}
	public string[] Extensions 
	{
		set 
		{
			_extensions = value;
			_fileDialog.Filter = FormatFilter(_hint, value);
		}
	}

	public DialogResult Result 
	{
		get => _result;
	}
	public string SelectedFilePath 
	{
		get => _selectedFilePath;
	}

	public FileSelector() 
	{
		this._title = $"Open file...";
		this._hint = $"All files";
		this._extensions = new[] { ".*" };

		this._result = DialogResult.Cancel;
		this._fileDialog = new () {
			Title = _title, Filter = FormatFilter(_hint, _extensions)
		};

		this._selectedFilePath = null!;
	}
	public void Select() 
	{
		try 
		{
			_result = _fileDialog.ShowDialog();
			_selectedFilePath = _fileDialog.FileName;
		}
		catch(Exception) 
		{
			_result = DialogResult.Retry;
			throw;
		}
	}

	private static string FormatFilter(string hint, IEnumerable<string> extensions) 
		=> $"{hint}|{FormatExtensions(extensions)}";

	private static string FormatExtensions(IEnumerable<string> value) 
		=> $"*{string.Join("; *", value)}";
}