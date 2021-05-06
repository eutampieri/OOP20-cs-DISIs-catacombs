using System;
using SixLabors.ImageSharp;

namespace Tampieri.Utils
{
    public interface IImageTransformerFactory
    {
        Func<Image, Image> rotate(double degrees);
        Func<Image, Image> scale(double factor);
        Func<Image, Image> flip(bool flipX, bool flipY);
    }
}
