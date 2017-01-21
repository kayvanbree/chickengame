using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
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

	List<Collider> InRadius = new List<Collider>();

	public Color playerColor;

	public GameObject barnPrefab;

	private GameObject currentHitChicken = null;

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
		{	currentButtonIndex = -1;}

		if (gamepad.GetButtonUp("B"))
			currentButtonIndex = -1;

		if (gamepad.GetButtonUp("X"))
			currentButtonIndex = -1;

		if (gamepad.GetButtonUp("Y"))
			currentButtonIndex = -1;

		if (gamepad.GetButtonDown("A"))
		{
			//if this is our chicken
			if (IsMyChicken())
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
		}
		if (gamepad.GetButtonDown("B"))
		{			
			//if this is our chicken
			if (IsMyChicken())
			{
				// play sound
				if (!audioSource.isPlaying)
				{
					audioSource.clip = clipButtonB;
					audioSource.Play();	
				}
				currentButtonIndex = GetButtonIndex("B");
			}
		}
		if (gamepad.GetButtonDown("X"))
		{
			//if this is our chicken
			if (IsMyChicken())
			{
				// play sound
				if (!audioSource.isPlaying)
				{
					audioSource.clip = clipButtonX;
					audioSource.Play();
				}
				currentButtonIndex = GetButtonIndex("X");
			}
		}
		if (gamepad.GetButtonDown("Y"))
		{
			//if this is our chicken
			if (IsMyChicken())
			{
				// play sound
				if (!audioSource.isPlaying)
				{
					audioSource.clip = clipButtonY;
					audioSource.Play();
				}
				currentButtonIndex = GetButtonIndex("Y");
			}
		}
		
		if (gamepad.GetTriggerTap_R())
		{
			RaycastHit hit;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(transform.position, fwd, out hit, 3.0f))
			{
				if(hit.collider.tag == "Chicken")
				{
					// get the player to which this chicken belongs
					Player ownedPlayer = hit.collider.gameObject.GetComponent<Chicken>().GetOwner();
					// check if we dont already own this chicken
					if(ownedPlayer != this)
					{
						// then set the new owner for this player (brainwash this chicken)
						hit.collider.gameObject.GetComponent<Chicken>().SetOwner(this);

						//marco, hier moet je de material van deze chicken ophalen, en de color setten van deze speler (playerColor)
					}
				}
			}
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipTriggerRight;
				audioSource.Play();
			}
			currentButtonIndex = GetButtonIndex("RT");
		}

		if (gamepad.GetTriggerTap_L())
		{
			// play sound
			if (!audioSource.isPlaying)
			{
				audioSource.clip = clipTriggerLeft;
				audioSource.Play();
				currentButtonIndex = GetButtonIndex("LT");
				// do explosion burst in radius
				for(int i = 0; i < InRadius.Count; i++)
				{
					Vector3 direction = transform.position - InRadius[i].gameObject.transform.position;
					direction.Normalize();
                    direction.y = 0;
                    InRadius[i].attachedRigidbody.AddForce((-direction * 15.0f), ForceMode.Impulse);
				}
			}
		}
	}

	bool IsMyChicken()
	{
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, fwd, out hit, 3.0f))
		{
			if (hit.collider.tag == "Chicken")
			{
				// get the player to which this chicken belongs
				Player ownedPlayer = hit.collider.gameObject.GetComponent<Chicken>().GetOwner();
				// check if we dont already own this chicken
				if (ownedPlayer == this)
				{
					// we need to store the current hit chicken, else we lose the data later from the hit, and we dont want to do a raycast again
					currentHitChicken = hit.collider.gameObject;
					return true;
				}
			}
		}
		return false;
	}

	public void SendToBarn(GameObject playersBarn)
	{
		if (currentHitChicken != null)
		{
			currentHitChicken.GetComponent<Chicken>().GoToBarn(playersBarn);
			currentHitChicken = null;
		}
	}

	void OnTriggerEnter(Collider other)
	{	
		if(other.tag == "Chicken" || other.tag == "Player0" || other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3")
		{
			if (!InRadius.Contains(other))
			{ InRadius.Add(other); }
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Chicken" || other.tag == "Player0" || other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3")
		{
			if (InRadius.Contains(other))
			{ InRadius.Remove(other); }
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
			case "Y":
				{
					index = buttonIndices[2];
				}
				break;
			case "X":
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
