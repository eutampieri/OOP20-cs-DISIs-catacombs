using System.Drawing;


namespace sanita
{
	namespace gamefx
	{

		/// <summary>
		/// this class manages the loading and the cutting of the single frame.
		/// </summary>

		public sealed class GameSheet
		{

			/// <summary>
			/// GameSheet constructor. </summary>
			/// <param name="path"> the path of the image file </param>

			public GameSheet(in string path)
			{
				this.Picture = utils.ImageLoader.LoadImage(path);
			}

			/// <summary>
			/// This method returns the image required. </summary>
			/// <returns> the image required </returns>

			public Image Picture { get; }
		}

	}
}
