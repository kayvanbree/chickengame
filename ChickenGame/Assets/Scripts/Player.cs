using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
	public string Name = "PlayerNone";
	public int playerIdx = -1;

	public x360_Gamepad gamepad;

	public AudioClip clipButtonA;
	public AudioClip clipButtonB;
	public AudioClip clipButtonX;
	public AudioClip clipButtonY;
	public AudioClip clipTriggerRight;
	public AudioClip clipTriggerLeft;

	private AudioSource audioSource;

	public int[] buttonIndices = new int[6];
	public int currentButtonIndex = -1;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	public void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		// reset index to -1, so we know that this player is not pressing any buttons
		if (gamepad.GetButtonUp("A"))
		{
			//Debug.Log("a is released");
			currentButtonIndex = -1;
		}

		if (gamepad.GetButtonUp("B"))
			currentButtonIndex = -1;

		if (gamepad.GetButtonUp("X"))
			currentButtonIndex = -1;

		if (gamepad.GetButtonUp("Y"))
			currentButtonIndex = -1;

		if (gamepad.GetButtonDown("A"))
		{
			// play sound
			if (!audioSource.isPlaying)
			{	
				audioSource.clip = clipButtonA;
				audioSource.Play();
			}
			// get the index for this button for this player and set it to the current button index
			// this way we can check if this is the same as the one on the screen
			currentButtonIndex = GetButtonIndex("A");
		}
		if (gamepad.GetButtonDown("B"))
		{
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipButtonB;
				audioSource.Play();
				currentButtonIndex = GetButtonIndex("B");
			}
		}
		if (gamepad.GetButtonDown("X"))
		{
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipButtonX;
				audioSource.Play();
				currentButtonIndex = GetButtonIndex("X");
			}
		}
		if (gamepad.GetButtonDown("Y"))
		{
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipButtonY;
				audioSource.Play();
				currentButtonIndex = GetButtonIndex("Y");
			}
		}
		if(gamepad.GetTriggerTap_L())
		{
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipTriggerLeft;
				audioSource.Play();
				currentButtonIndex = GetButtonIndex("LT");
			}
		}
		if (gamepad.GetTriggerTap_R())
		{
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipTriggerRight;
				audioSource.Play();
				currentButtonIndex = GetButtonIndex("RT");
			}
		}
	}

	public int GetButtonIndex(string button)
	{
		int index = -1;

		switch (button)
		{
			case "A":
				{
					index = buttonIndices[0];
				}
				break;
			case "B":
				{
					index = buttonIndices[1];
				}
				break;
			case "X":
				{
					index = buttonIndices[2];
				}
				break;
			case "Y":
				{
					index = buttonIndices[3];
				}
				break;
			case "LT":
				{
					index = buttonIndices[4];
				}
				break;
			case "RT":
				{
					index = buttonIndices[5];
				}
				break;
		}

		return index;
	}

	void OnGUI()
	{
		
	}
}
