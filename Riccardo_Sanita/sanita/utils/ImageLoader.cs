using System;
using System.Drawing;
using System.IO;

namespace sanita.utils
{

	/// <summary>
	/// This class loads image from a path.
	/// </summary>
	public sealed class ImageLoader
	{

		private ImageLoader()
		{

		}

		/// <summary>
		/// This method loads image from a path. </summary>
		/// <param name="path"> the path to find the image </param>
		/// <returns> an optional of Buffered image </returns>

		public static Image LoadImage(string path)
		{
			try
			{
				StreamReader file = new StreamReader(path);
				{
					if (file == null)
					{
						return null;
					}
					try
					{
						using (Image image = Image.FromFile(path))
						{
							return image;
						}
					}
					catch (IOException)
					{
						return null;
					}
				}
			}
			catch (IOException inputStreamException)
			{
				Console.WriteLine(inputStreamException.ToString());
				Console.Write(inputStreamException.StackTrace);
				return null;
			}

		}

	}

}
