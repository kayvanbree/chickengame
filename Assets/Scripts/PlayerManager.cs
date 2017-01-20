using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public GameObject PlayerPrefab;

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

			// set the indices for the buttons, this way we can later on check if the button index was the same as some other action
			for (int j = 0; j < 6; j++)
				player.GetComponent<Player>().buttonIndices[j] = buttonIndices[i * 6 + j];

			// set random player properties
			player.GetComponent<Player>().Name = "Player" + i;

			// add to player list
			Players.Add(player);
		}
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < numberPlayers; i++)
		{
			Players[i].GetComponent<Player>().Update();

			// check if there are any items left in the sequence
			// if the button pressed for this specific player is equal to the sound in the list
			/*if (Players[i].GetComponent<Player>().currentButtonIndex == some base of a player)
                {
                    Players[i].GetComponent<Player>().currentButtonIndex = -1;

                    Debug.Log("player idx: " + Players[i].ToString() + "sound nr: " );
                   
                    // we know that that player pressed the correct button for that image
                    ScreenImages.RemoveAt(0);
                }
                else if (Players[i].GetComponent<Player>().currentButtonIndex > -1)
                {
                    Players[i].GetComponent<Player>().currentButtonIndex = -1;
                }
            }*/
		}
	}


	void OnGUI()
	{

	}
}
