using System.Drawing;


// ReSharper disable MemberCanBePrivate.Global


namespace CreativeTrager.ImageAugmentator.Model;
public sealed class TextOnImageWriter 
{
	private readonly Image _image;
	private readonly Graphics _graphics;
	private readonly string _text;
	private Color _color;

	public TextOnImageWriter(Image image, string text) : this(image, text, Color.Black) { }
	public TextOnImageWriter(Image image, string text, string color) : this(image, text, ColorTranslator.FromHtml(color)) { }
	public TextOnImageWriter(Image image, string text, Color color) 
	{
		this._image = image;
		this._graphics = Graphics.FromImage(image);
		this._text = text;
		this._color = color;
	}

	public string HexColor 
	{
		get => ColorTranslator.ToHtml(_color);
		set => _color = ColorTranslator.FromHtml(value);
	}
	public void Write() 
	{
		var fontSize = Math.Min(_image.Width*0.05f, 20);
		var font = new Font(familyName: "arial", emSize: fontSize, FontStyle.Regular);
		var brush = new SolidBrush(_color);
		var position = new PointF(x: 10, y: 10);
		_graphics.DrawString($"{_text}-{fontSize}", font, brush, position);
	}
}