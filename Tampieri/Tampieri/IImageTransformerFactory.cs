using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace Tampieri
{
    interface IImageTransformerFactory
    {
        Func<Image, Image> rotate(double degrees);
        Func<Image, Image> scale(double factor);
        Func<Image, Image> flip(bool flipX, bool flipY);
    }
}
