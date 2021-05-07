using System.Text;

namespace sanita.input
{

	/// <summary>
	/// this class manages the key events.
	/// </summary>

	public sealed class KeyManager
	{
		private static readonly int ID_MAX = 2000;
		private bool[] keys;

		/// <summary>
		/// Key manager constructor.
		/// </summary>

		private KeyManager()
		{
			this.keys = new bool[ID_MAX];
		}

        /// <summary>
        /// this Method is due to a singleton and i use to generate a KeyManager.
        /// </summary>
        /// <returns> a KeyManager </returns>

        public static KeyManager Manager { get; } = new KeyManager();

		/// <summary>
		/// This method controls if a key is presse. </summary>
		/// <param name="keyCode"> the key to control </param>
		/// <returns> true if the key is pressed </returns>

		public bool IsKeyPressed(int keyCode)
		{
			return this.keys[keyCode];
		}

		/// <summary>
		/// This method keep track of the pressed keys. </summary>
		/// <param name="e"> the key pressed </param>

		public void KeyPressed(char w)
		{
			int AsciCode = (int)w;
			this.keys[AsciCode] = true;
		}

		/// <summary>
		/// this method keep track of the released keys. </summary>
		/// <param name="e"> the released key </param>

		public void KeyReleased(char w)
		{
			int AsciCode = (int)w;
			this.keys[AsciCode] = false;
		}

	}

}