using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;

namespace Tampieri.Utils
{
    public class ImageTransformerFactory : IImageTransformerFactory
    {
        public Func<Image, Image> flip(bool flipX, bool flipY)
        {
            return image =>
            {
                Image result = image;
                if (flipX)
                {
                    result.Mutate(x => x.Flip(FlipMode.Vertical));
                }
                if (flipY)
                {
                    result.Mutate(x => x.Flip(FlipMode.Horizontal));
                }
                return result;
            };
        }

        public Func<Image, Image> rotate(double degrees)
        {
            return image =>
            {
                Image result = image;
                result.Mutate(x => x.Rotate((float)degrees));
                return result;
            };
        }

        public Func<Image, Image> scale(double factor)
        {
            return image =>
            {
                int finalWidth = (int)(image.Width * factor);
                int finalHeight = (int)(image.Height * factor);
                Image result = image;
                result.Mutate(x => x.Resize(finalWidth, finalHeight));
                return result;
            };
        }
    }
}
