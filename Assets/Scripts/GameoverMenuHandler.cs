﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverMenuHandler : MonoBehaviour
{
    GameStateManager GameStateManager;

    public Sprite GreenWon;
    public Sprite RedWon;
    public Sprite YellowWon;
    public Sprite BlueWon;

    public Image PlayerWonImage;

    void Start()
    {
        GameStateManager = FindObjectOfType<GameStateManager>();

        // Swap panel for correct one
        switch (GameStateManager.Winner.playerIndex)
        {
            case 0:
                PlayerWonImage.sprite = GreenWon;
                break;
            case 1:
                PlayerWonImage.sprite = RedWon;
                break;
            case 2:
                PlayerWonImage.sprite = YellowWon;
                break;
            case 3:
                PlayerWonImage.sprite = BlueWon;
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
                Destroy(GameStateManager);
                SceneManager.LoadScene("MainMenu");
                return;
            }
        }
    }
}
