using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public List<PlayerEntry> Players = new List<PlayerEntry>(4);
    private int numberPlayers = 0;

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