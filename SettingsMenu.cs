using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
   public static SettingsMenu Instance;

   private Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

   private void Awake()
   {
        LoadKeybinds();
   }

   public void SetKeybind(string action, KeyCode newKey)
   {
        if (keybinds.ContainsKey(action))
        {
            keybinds[action] = newKey;
            SaveKeybinds();
        }
   }

   public KeyCode GetKeybind(string action)
   {
        return keybinds.ContainsKey(action) ? keybinds[action] : KeyCode.None;
   }

   public void SaveKeybinds()
   {
        foreach (var key in keybinds)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
   }

   private void LoadKeybinds()
   {
        // keybinds["MoveForward"] = (KeyCode) PlayerPrefs.GetInt("MoveForward", (int)KeyCode.W);
        // keybinds["MoveBackward"] = (KeyCode) PlayerPrefs.GetInt("Movebackward", (int)KeyCode.S);
        // keybinds["TurnLeft"] = (KeyCode) PlayerPrefs.GetInt("TurnLeft", (int)KeyCode.A);
        // keybinds["TurnRight"] = (KeyCode) PlayerPrefs.GetInt("TurnRight", (int)KeyCode.D);
        // keybinds["Punch"] = (KeyCode) PlayerPrefs.GetInt("Punch", (int)KeyCode.Alpha6);
        // keybinds["Punch2"] = (KeyCode) PlayerPrefs.GetInt("Punch2", (int)KeyCode.Alpha5);
        // keybinds["HookPunch"] = (KeyCode) PlayerPrefs.GetInt("HookPunch", (int)KeyCode.Alpha3);
        // keybinds["Kick"] = (KeyCode) PlayerPrefs.GetInt("Kick", (int)KeyCode.Alpha4);
        // keybinds["Jump"] = (KeyCode) PlayerPrefs.GetInt("Jump", (int)KeyCode.Space);

        foreach (var key in new List<string>(keybinds.Keys))
        {
            if (PlayerPrefs.HasKey(key))
            {
                keybinds[key] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(key));
            }
        }
   }
}
