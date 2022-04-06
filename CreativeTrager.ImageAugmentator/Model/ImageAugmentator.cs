using System.Drawing;
using CreativeTrager.ImageAugmentator.Extensions;


namespace CreativeTrager.ImageAugmentator.Model;
public sealed class ImageAugmentator 
{
	public Dictionary<string, Image> Run(Image image) 
	{
		var imageVariations = new Dictionary<string, Image>() {
			{ "rotateNone_flipNone", ((Image)image.Clone()).AddRotateFlip(RotateFlipType.RotateNoneFlipNone) },
			{ "rotate90_flipNone",   ((Image)image.Clone()).AddRotateFlip(RotateFlipType.Rotate90FlipNone)   },
			{ "rotate180_flipNone",  ((Image)image.Clone()).AddRotateFlip(RotateFlipType.Rotate180FlipNone)  },
			{ "rotate270_flipNone",  ((Image)image.Clone()).AddRotateFlip(RotateFlipType.Rotate270FlipNone)  },

			{ "rotateNone_flipX",    ((Image)image.Clone()).AddRotateFlip(RotateFlipType.RotateNoneFlipX)    },
			{ "rotate90_flipX",      ((Image)image.Clone()).AddRotateFlip(RotateFlipType.Rotate90FlipX)      },
			{ "rotate180_flipX",     ((Image)image.Clone()).AddRotateFlip(RotateFlipType.Rotate180FlipX)     },
			{ "rotate270_flipX",     ((Image)image.Clone()).AddRotateFlip(RotateFlipType.Rotate270FlipX)     }
		};

		foreach(var (variationId, variation) in imageVariations) 
		{
			new TextOnImageWriter(
				image: variation,
				text: variationId,
				color: "#FFFFFF"
			).Write();
		}

		return imageVariations;
	}
}