using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenuHandler : MonoBehaviour
{
    void Update()
    {
        CheckForStartPressed();
    }

    void CheckForStartPressed()
    {
        for (int i = 0; i < 4; i++)
        {
            x360_Gamepad pad = GamepadManager.Instance.GetGamepad(i + 1);
            if (!pad.IsConnected) continue;
            bool pressed = pad.GetButton("Start");
            if (pressed)
            {
                SceneManager.LoadScene("MainMenu");
                return;
            }
        }
    }
}
