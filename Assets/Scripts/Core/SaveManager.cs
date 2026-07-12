using UnityEngine;

namespace MiniGameWorld.Core
{
    public class SaveManager
    {
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }

    }

}