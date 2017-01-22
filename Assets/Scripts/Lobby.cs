using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public List<PlayerEntry> Players = new List<PlayerEntry>(4);
    private int numberPlayers = 0;

    private PlayerManager PlayerManager;

    void OnLevelWasLoaded()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
        if (PlayerManager != null)
        {
            PlayerManager.SetButtonIndices();
            LoadPlayers();
        }   
    }

    void LoadPlayers()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Players[i].IsActive())
            {
                PlayerManager.AddPlayer(i);
            }
        }
    }

    public bool IsPlayerActive(int index)
    {
        return Players[index].IsActive();
    }

    public int NumberOfPlayers()
    {
        return numberPlayers;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void AddPlayer(int index)
    {
        Players[index].Activate();
        numberPlayers++;
        Debug.Log("Player " + (index + 1) + " entered the game");
    }

    public void RemovePlayer(int index)
    {
        Players[index].Deactivate();
        numberPlayers--;
        Debug.Log("Player " + (index + 1) + " has left the game");
    }
}