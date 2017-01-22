using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenuHandler : MonoBehaviour
{
    GameStateManager GameStateManager;

    public Sprite GreenWon;
    public Sprite RedWon;
    public Sprite YellowWon;
    public Sprite BlueWon;

    void Start()
    {
        GameStateManager = FindObjectOfType<GameStateManager>();

        // Swap panel for correct one
        switch (GameStateManager.Winner.playerIndex)
        {
            case 0:
                Debug.Log("Green won");
                break;
            case 1:
                Debug.Log("Red won");
                break;
            case 2:
                Debug.Log("Yellow won");
                break;
            case 3:
                Debug.Log("Blue won");
                break;
        }
    }

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
