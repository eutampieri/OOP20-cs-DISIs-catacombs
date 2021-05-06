using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace Tampieri
{
    interface IImageTransformer
    {
        Image transform(Image i);
    }
}
