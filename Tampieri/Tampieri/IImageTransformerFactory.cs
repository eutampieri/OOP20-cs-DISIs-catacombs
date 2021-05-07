using SixLabors.ImageSharp;
using System;

namespace Tampieri.Utils
{
    /// <summary>An abstract factory that returns function that apply the given transformation to an image</summary>
    public interface IImageTransformerFactory
    {
        Func<Image, Image> rotate(double degrees);
        Func<Image, Image> scale(double factor);
        Func<Image, Image> flip(bool flipX, bool flipY);
    }
}
