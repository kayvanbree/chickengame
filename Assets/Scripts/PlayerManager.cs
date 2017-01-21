using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// Use this for initialization
	void Start()
	{
		numberPlayers = GamepadManager.Instance.GamepadCount;
		for (int i = 0; i < 24; i++)
		{
			buttonIndices[i] = i;
		}

        Players = new GameObject[numberPlayers];
	}

    /// <summary>
    /// Adds a player to the game on the given index
    /// </summary>
    /// <param name="index"></param>
    void AddPlayer(int index)
    {
        // instantiate player on position (later use to spawn at real pos)
        GameObject player = Instantiate(PlayerPrefab, new Vector3(0.0f, 0.0f, 0.0f), PlayerPrefab.transform.rotation) as GameObject;

        player.GetComponent<Player>().gamepad = GamepadManager.Instance.GetGamepad(index + 1);

        player.GetComponent<Player>().playerIdx = index;

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
                Players[0].GetComponent<Player>().playerColor = new Color(0, 1, 0); //green
                Players[0].GetComponent<Player>().barnPrefab = GreenBarn;
                break;
            }
            case 1:
            {
                Players[1].GetComponent<Player>().playerColor = new Color(1, 0, 0); //red
                Players[1].GetComponent<Player>().barnPrefab = RedBarn;
                break;
            }
            case 2:
            {
                Players[2].GetComponent<Player>().playerColor = new Color(1, 1, 0); //yellow
                Players[2].GetComponent<Player>().barnPrefab = YellowBarn;
                break;
            }
            case 3:
            {
                Players[3].GetComponent<Player>().playerColor = new Color(0, 0, 1); //blue
                Players[3].GetComponent<Player>().barnPrefab = BlueBarn;
                break;
            }
        }
    }

    

	void SendToBarn()
	{
		
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
				if (currentPlayer.buttonIndices[0] == currentPlayer.currentButtonIndex) //button A
				{ currentPlayer.SendToBarn(GreenBarn); }
				else if (currentPlayer.buttonIndices[1] == currentPlayer.currentButtonIndex) // button B
				{ currentPlayer.SendToBarn(RedBarn); }
				else if (currentPlayer.buttonIndices[2] == currentPlayer.currentButtonIndex) // button Y
				{ currentPlayer.SendToBarn(YellowBarn); }
				else if (currentPlayer.buttonIndices[3] == currentPlayer.currentButtonIndex) // button X
				{ currentPlayer.SendToBarn(BlueBarn); }
			}
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

	void OnGUI()
	{

	}
}
