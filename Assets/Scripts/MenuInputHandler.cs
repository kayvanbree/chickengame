using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputHandler : MonoBehaviour
{
    int numberPlayers = 4;
    Lobby Lobby;
    public GameObject background;

    void Start()
    {
        Lobby = FindObjectOfType<Lobby>();

        if(FindObjectsOfType<MenuBackground>().Length > 1)
        {
            Destroy(background);
        } 
    }

    void Update()
    {
        CheckForNewPlayers();
        CheckForLeavingPlayers();
        CheckForStartPressed();
    }

    void CheckForStartPressed()
    {
        if (Lobby.NumberOfPlayers() < 1) return;

        for (int i = 0; i < numberPlayers; i++)
        {
            x360_Gamepad pad = GamepadManager.Instance.GetGamepad(i + 1);
            if (!pad.IsConnected) continue;
            bool pressed = pad.GetButtonDown("Start");
            if (pressed)
            {
                Lobby.StartGame();
                return;
            }
        }
    }

    void CheckForNewPlayers()
    {
        for (int i = 0; i < numberPlayers; i++)
        {
            // Player already active
            if (Lobby.IsPlayerActive(i)) continue;

            x360_Gamepad pad = GamepadManager.Instance.GetGamepad(i + 1);
            if (!pad.IsConnected) continue;
            bool pressed = pad.GetButton("A");
            if (pressed)
            {
                Lobby.AddPlayer(i);
            }
        }
    }

    void CheckForLeavingPlayers()
    {
        for (int i = 0; i < numberPlayers; i++)
        {
            // Player already active
            if (!Lobby.IsPlayerActive(i)) continue;

            x360_Gamepad pad = GamepadManager.Instance.GetGamepad(i + 1);
            if (!pad.IsConnected) continue;
            bool pressed = pad.GetButton("B");
            if (pressed)
            {
                Lobby.RemovePlayer(i);
            }
        }
    }
}
