using sanita.gamefx;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace sanita
{

	/// <summary>
	/// This class is used to provide the images for the animation of the entities and for the assets of the map.
	/// </summary>

	public sealed class AssetManager
	{

		private readonly IDictionary<string, Image> image = new Dictionary<string, Image>();

		/// <summary>
		/// private constructor for the Singleton of the AssetManager.
		/// </summary>
		private AssetManager()
		{
			Load();
		}

        /// <summary>
        /// private constructor for the Singleton of the AssetManager. </summary>
        /// <returns> AssetManager </returns>
        public static AssetManager AManager { get; } = new AssetManager();

		/// <summary>
		/// This method return a single image specified by the parameter.
		/// </summary>
		/// <param name="key"> a string that specify the action </param>
		/// <returns> the image specified by parameter </returns>
		public Image GetImage(in string key)
		{
			try
			{
				return this.image[key];
			}
			catch(KeyNotFoundException)
			{
				return null;
			}
		}

		/// <summary>
		/// This method loads all the images in two separates List of images and animations.
		/// </summary>
		private void Load()
		{
			var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			string filePath1 = Path.Combine(projectPath, "Resources/playersheet.png");

			image.Add("Player", new GameSheet(filePath1).Picture);
		}
	}
}