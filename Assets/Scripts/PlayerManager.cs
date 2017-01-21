using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public GameObject PlayerPrefab;
	public GameObject GreenBarn;
	public GameObject RedBarn;
	public GameObject YellowBarn;
	public GameObject BlueBarn;

	private List<GameObject> Players;
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

		Players = new System.Collections.Generic.List<GameObject>(numberPlayers);

		for (int i = 0; i < numberPlayers; i++)
		{
			// instantiate player on position (later use to spawn at real pos)
			GameObject player = Instantiate(PlayerPrefab, new Vector3(0.0f, 0.0f, 0.0f), PlayerPrefab.transform.rotation) as GameObject;

			player.GetComponent<Player>().gamepad = GamepadManager.Instance.GetGamepad(i + 1);

			player.GetComponent<Player>().playerIdx = i;

			player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().playerIdx = (i + 1);

			// set the indices for the buttons, this way we can later on check if the button index was the same as some other action
			for (int j = 0; j < 6; j++)
				player.GetComponent<Player>().buttonIndices[j] = buttonIndices[i * 6 + j];

			// set player tag
			player.tag = "Player" + i;

			// add to player list
			Players.Add(player);
		}

		Players[0].GetComponent<Player>().playerColor = new Color(0, 1, 0); //green
		Players[0].GetComponent<Player>().barnPrefab = GreenBarn;

		Players[1].GetComponent<Player>().playerColor = new Color(1, 0, 0); //red
		Players[1].GetComponent<Player>().barnPrefab = RedBarn;

		Players[2].GetComponent<Player>().playerColor = new Color(1, 1, 0); //yellow
		Players[2].GetComponent<Player>().barnPrefab = YellowBarn;

		Players[3].GetComponent<Player>().playerColor = new Color(0, 0, 1); //blue
		Players[3].GetComponent<Player>().barnPrefab = BlueBarn;
	}

	void SendToBarn()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < numberPlayers; i++)
		{
			Players[i].GetComponent<Player>().Update();
		}

		for(int i = 0; i < Players.Count; i++)
		{
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

	void OnGUI()
	{

	}
}
