using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLRayTracer
{
    /// <summary>
    /// Class for managing user input
    /// </summary>
    class InputManager
    {

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static InputManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }
        private static InputManager _instance;

        /// <summary>
        /// Stores if each key is pressed or not
        /// </summary>
        private Dictionary<Keys, bool> _pressed;

        public InputManager()
        {
            _pressed = new Dictionary<Keys, bool>();

            var values = Enum.GetValues(typeof(Keys));
            foreach (var key in values)
            {
                if (_pressed.ContainsKey((Keys)key) == false)
                {
                    _pressed.Add((Keys)key, false);
                }
            }
        }

        /// <summary>
        /// Returns if a key is pressed
        /// </summary>
        /// <param name="key">the key to check if it's pressed</param>
        /// <returns>if the key is pressed</returns>
        public bool IsKeyPressed(Keys key)
        {
            return _pressed[key];
        }

        /// <summary>
        /// Called when a key is pressed down, updates input manager to reflect state
        /// </summary>
        /// <param name="key">What Key is pressed</param>
        public void ProcessKeyDown(Keys key)
        {
            _pressed[key] = true;
        }

        /// <summary>
        /// Called when a key is pressed up, updates input manager to reflect state
        /// </summary>
        /// <param name="key">What Key is pressed</param>
        public void ProcessKeyUp(Keys key)
        {
            _pressed[key] = false;
        }

    }
}
