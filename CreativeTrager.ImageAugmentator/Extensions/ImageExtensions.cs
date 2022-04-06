using System.Drawing;


namespace CreativeTrager.ImageAugmentator.Extensions;
internal static class ImageExtensions 
{
	internal static Image AddRotateFlip(this Image image, RotateFlipType type)
	{
		image.RotateFlip(type);
		return image;
	}
}