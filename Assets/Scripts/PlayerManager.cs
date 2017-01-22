using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	public GameObject PlayerPrefab;
	public Barn GreenBarn;
	public Barn RedBarn;
	public Barn YellowBarn;
	public Barn BlueBarn;

	private GameObject[] Players;
	private int[] buttonIndices = new int[24];
	private int numberPlayers = -1;

    GameStateManager GameStateManager;

	// Use this for initialization
	public void SetButtonIndices()
	{
		numberPlayers = GamepadManager.Instance.GamepadCount;
		for (int i = 0; i < 24; i++)
		{
			buttonIndices[i] = i;
		}

        Players = new GameObject[numberPlayers];

        GreenBarn.gameObject.SetActive(false);
        RedBarn.gameObject.SetActive(false);
        YellowBarn.gameObject.SetActive(false);
        BlueBarn.gameObject.SetActive(false);

        GameStateManager = FindObjectOfType<GameStateManager>();
    }

    /// <summary>
    /// Adds a player to the game on the given index
    /// </summary>
    /// <param name="index"></param>
    public void AddPlayer(int index)
    {
        // instantiate player on position (later use to spawn at real pos)
        GameObject player = Instantiate(PlayerPrefab, new Vector3(0.0f, 2.0f, 0.0f), PlayerPrefab.transform.rotation) as GameObject;

        player.GetComponent<Player>().gamepad = GamepadManager.Instance.GetGamepad(index + 1);

        player.GetComponent<Player>().playerIndex = index;

        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().playerIdx = (index + 1);

        // set the indices for the buttons, this way we can later on check if the button index was the same as some other action
        for (int j = 0; j < 6; j++)
            player.GetComponent<Player>().buttonIndices[j] = buttonIndices[index * 6 + j];

        // set player tag
        player.tag = "Player" + index;

        // add to player list
        Players[index] = player;

        // Get correct player color and barn
        switch(index)
        {
            case 0:
            {
				GreenBarn.gameObject.SetActive(true);
				InitializePlayerComponent(0, new Color(0, 1, 0), GreenBarn);
                break;
            }
            case 1:
            {
				RedBarn.gameObject.SetActive(true);
				InitializePlayerComponent(1, new Color(1, 0, 0), RedBarn);
                break;
            }
            case 2:
            {
				YellowBarn.gameObject.SetActive(true);
				InitializePlayerComponent(2, new Color(1, 1, 0), YellowBarn);
                break;
            }
            case 3:
            {
				BlueBarn.gameObject.SetActive(true);
				InitializePlayerComponent(3, new Color(0, 0, 1), BlueBarn);
                break;
            }
        }
    }

    void InitializePlayerComponent(int index, Color color, Barn barn)
    {
        if (index > numberPlayers) return;

        Player playerComponent = Players[index].GetComponent<Player>();
        playerComponent.playerColor = color;
        playerComponent.Barn = barn;
        playerComponent.playerDied += CheckGameOver;
    }

	// Update is called once per frame
	void Update()
	{
        CheckForNewPlayers();

		for (int i = 0; i < numberPlayers; i++)
		{
            if (Players[i] == null) continue;
			Players[i].GetComponent<Player>().Update();
		}

		for(int i = 0; i < Players.Length; i++)
		{
            if (Players[i] == null) continue;
            Player currentPlayer = Players[i].GetComponent<Player>();
			//if a player pressed a button
			if (currentPlayer.currentButtonIndex > -1)
			{
				if (currentPlayer.buttonIndices[0] == currentPlayer.currentButtonIndex && i != 0 && GreenBarn.IsAlive()) //button A
				{ currentPlayer.SendToBarn(GreenBarn); }
				else if (currentPlayer.buttonIndices[1] == currentPlayer.currentButtonIndex && i != 1 && RedBarn.IsAlive()) // button B
				{ currentPlayer.SendToBarn(RedBarn); }
				else if (currentPlayer.buttonIndices[2] == currentPlayer.currentButtonIndex && i != 2 && YellowBarn.IsAlive()) // button Y
				{ currentPlayer.SendToBarn(YellowBarn); }
				else if (currentPlayer.buttonIndices[3] == currentPlayer.currentButtonIndex && i != 3 && BlueBarn.IsAlive()) // button X
				{ currentPlayer.SendToBarn(BlueBarn); }
			}
		}
	}

    void CheckGameOver()
    {
        int playersAlive = 0;
        Player lastPlayerFound = null;
        for (int i = 0; i < 4; i++)
        {
            if (Players[i] != null && Players[i].GetComponent<Player>().State == PlayerState.Playing)
            {
                playersAlive++;
                lastPlayerFound = Players[i].GetComponent<Player>();
            }
        } 
        if(lastPlayerFound != null && playersAlive == 1)
        {
            GameStateManager.Winner = lastPlayerFound;
            GameStateManager.EndGame();
        }
    }

    void CheckForNewPlayers()
    {
        for (int i = 0; i < numberPlayers; i++)
        {
            if (Players[i] != null) continue;

            x360_Gamepad pad = GamepadManager.Instance.GetGamepad(i + 1);
            if (!pad.IsConnected) continue;
            bool pressed = pad.GetButton("A");
            if (pressed)
            {
                AddPlayer(i);
            }
        }
    }
}
