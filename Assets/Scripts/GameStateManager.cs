using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public Player Winner { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
