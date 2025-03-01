using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuUI : MonoBehaviour
{
    public TMP_Text forwardKeyText;
    public TMP_Text backwardKeyText;
    public TMP_Text leftKeyText;
    public TMP_Text rightKeyText;
    public TMP_Text punchKeyText;
    public TMP_Text punch2KeyText;
    public TMP_Text hookPunchKeyText;
    public TMP_Text kickKeyText;
    public TMP_Text jumpKeyText;

    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;
    public Button punchButton;
    public Button punch2Button;
    public Button hookPunchButton;
    public Button kickButton;
    public Button jumpButton;

    private Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

    private string waitingForKey = null;

    private SettingsMenu settingsMenu;

    void Start()
    {
        settingsMenu = FindObjectOfType<SettingsMenu>();

        keybinds["MoveForward"] = KeyCode.W;
        keybinds["MoveBackward"] = KeyCode.S;
        keybinds["TurnLeft"] = KeyCode.A;
        keybinds["TurnRight"] = KeyCode.D;
        keybinds["Punch"] = KeyCode.Alpha6;
        keybinds["Punch2"] = KeyCode.Alpha5;
        keybinds["HookPunch"] = KeyCode.Alpha3;
        keybinds["Kick"] = KeyCode.Alpha4;
        keybinds["Jump"] = KeyCode.Space;

        UpdateUI();
        forwardButton.onClick.AddListener(() => StartRebinding("MoveForward"));
        backwardButton.onClick.AddListener(() => StartRebinding("MoveBackward"));
        leftButton.onClick.AddListener(() => StartRebinding("TurnLeft"));
        rightButton.onClick.AddListener(() => StartRebinding("TurnRight"));
        punchButton.onClick.AddListener(() => StartRebinding("Punch"));
        punch2Button.onClick.AddListener(() => StartRebinding("Punch2"));
        kickButton.onClick.AddListener(() => StartRebinding("Kick"));
        hookPunchButton.onClick.AddListener(() => StartRebinding("HookPunch"));
        jumpButton.onClick.AddListener(() => StartRebinding("Jump"));
    }

    void UpdateUI()
    {
        forwardKeyText.text = keybinds["MoveForward"].ToString();
        backwardKeyText.text = keybinds["MoveBackward"].ToString();
        leftKeyText.text = keybinds["TurnLeft"].ToString();
        rightKeyText.text = keybinds["TurnRight"].ToString();
        punchKeyText.text = keybinds["Punch"].ToString();
        punch2KeyText.text = keybinds["Punch2"].ToString();
        hookPunchKeyText.text = keybinds["HookPunch"].ToString();
        kickKeyText.text = keybinds["Kick"].ToString();
        jumpKeyText.text = keybinds["Jump"].ToString();
    }

    void StartRebinding(string action)
    {
        Debug.Log("Press a key to bind " + action);
        waitingForKey = action;

        settingsMenu.SaveKeybinds();
        UpdateUI();
    }

    public void StartRebindingForward() {StartRebinding("MoveForward");}
    public void StartRebindingBackward() {StartRebinding("MoveBackward");}
    public void StartRebindingLeft() {StartRebinding("TurnLeft");}
    public void StartRebindingRight() {StartRebinding("TurnRight");}
    public void StartRebindingPunch() {StartRebinding("Punch");}
    public void StartRebindingPunch2() {StartRebinding("Punch2");}
    public void StartRebindingHookPunch() {StartRebinding("HookPunch");}
    public void StartRebindingKick() {StartRebinding("Kick");}
    public void StartRebindingJump() {StartRebinding("Jump");}

    void Update()
    {
        if (waitingForKey != null)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    keybinds[waitingForKey] = key;
                    waitingForKey = null;
                    UpdateUI();
                    Debug.Log("Keybind Updated");
                    break;
                }
            }
        }
    }
}
