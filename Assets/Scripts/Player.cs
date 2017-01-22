using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
    Playing,
    GameOver
}

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    // Delegate gets called when the barn is destroyed
    public delegate void PlayerDied();
    public PlayerDied playerDied;

    public int playerIndex = -1;

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

    public float force = 15f;

    List<Collider> InRadius = new List<Collider>();

    public PlayerState State = PlayerState.Playing;
	public float CommandCooldown = 3.0f;
	public float BrainwaveCooldown = 6.0f;
	public float BlastCooldown = 12.0f;

	public float CommandTimer;
	public float BrainwaveTimer;
	public float BlastTimer;

	public Color playerColor;

	private GameObject brainwaveclone;
	public GameObject BrainWavePrefab;
	public GameObject SmokeBlastParticlePrefab;

	private Barn barn;
	public Barn Barn
    {
        get
        {
            return barn;
        }
        set
        {
            barn = value;
            barn.barnDestroyed += GameOver;
        }
    }

	private GameObject currentHitChicken = null;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		CommandTimer = 0.0f;
		BrainwaveTimer = 0.0f;
		BlastTimer = 0.0f;
	}

    public void GameOver()
    {
        gameObject.SetActive(false);
        State = PlayerState.GameOver;
        playerDied();
        Debug.Log("Game over for player " + playerIndex);
    }

	// Update is called once per frame
	public void Update()
	{
        if (State == PlayerState.Playing)
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

		// we have no cooldown on the command when 0
		if (CommandTimer <= 0)
		{
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
					CommandTimer = CommandCooldown;
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
					CommandTimer = CommandCooldown;
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
					CommandTimer = CommandCooldown;
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
					CommandTimer = CommandCooldown;
				}
			}
		}
		else
		{
			if (CommandTimer > 0.0f)
				CommandTimer -= Time.deltaTime;
		}

		if (BrainwaveTimer <= 0.0f)
		{
			if (gamepad.GetTriggerTap_R())
			{
				RaycastHit hit;
				Vector3 fwd = transform.TransformDirection(Vector3.forward);
				if (Physics.Raycast(transform.position, fwd, out hit, 5.0f))
				{
					if (hit.collider.tag == "Chicken")
					{
						// get the player to which this chicken belongs
						Player ownedPlayer = hit.collider.gameObject.GetComponent<Chicken>().GetOwner();
						// check if we dont already own this chicken
						if (ownedPlayer != this)
						{
							currentHitChicken = hit.collider.gameObject;
							//Send out beam
							brainwaveclone = Instantiate(BrainWavePrefab, transform.position, transform.rotation);


							// then set the new owner for this player (brainwash this chicken)
							hit.collider.gameObject.GetComponent<Chicken>().SetOwner(this);

							for (int i = 0; i < hit.collider.gameObject.GetComponentInChildren<Renderer>().materials.Length; i++)
							{
								if (hit.collider.gameObject.GetComponentInChildren<Renderer>().materials[i].name == "Mat_PlayerOwner (Instance)")
								{
									Material mat = hit.collider.gameObject.GetComponentInChildren<Renderer>().materials[i];
									mat.SetColor("_Color", playerColor);
									hit.collider.gameObject.GetComponentInChildren<Renderer>().materials[i] = mat;
								}

                                if (hit.collider.gameObject.GetComponentInChildren<Renderer>().materials[i].name == "ChickenTexture (Instance)")
                                {
                                    Material mat = hit.collider.gameObject.GetComponentInChildren<Renderer>().materials[i];
                                    mat.SetColor("_Color", playerColor);
                                    hit.collider.gameObject.GetComponentInChildren<Renderer>().materials[i] = mat;
                                }
							}
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
				BrainwaveTimer = BrainwaveCooldown;
			}
		}
		else
		{
			if (BrainwaveTimer > 0.0f)
				BrainwaveTimer -= Time.deltaTime;
		}

		if (brainwaveclone != null && currentHitChicken != null)
		{
			brainwaveclone.GetComponent<LineRenderer>().SetPosition(0, transform.position);
			brainwaveclone.GetComponent<LineRenderer>().SetPosition(1, currentHitChicken.transform.position);
			brainwaveclone.GetComponent<LineRenderer>().material.SetColor("_Color", playerColor);
		}

		if (BlastTimer <= 0.0f)
		{
			if (gamepad.GetTriggerTap_L())
			{
				// play sound
				if (!audioSource.isPlaying)
				{
					audioSource.clip = clipTriggerLeft;
					audioSource.Play();
				}

				Instantiate(SmokeBlastParticlePrefab, transform.position, transform.rotation);
				// do explosion burst in radius
				for (int i = 0; i < InRadius.Count; i++)
				{
					Vector3 direction = transform.position - InRadius[i].gameObject.transform.position;
					direction.Normalize();
					direction.y = 0;
					InRadius[i].attachedRigidbody.AddForce((-direction * force), ForceMode.Impulse);
				}
				currentButtonIndex = GetButtonIndex("LT");
				BlastTimer = BlastCooldown;
			}
		}
		else
		{
			if (BlastTimer > 0.0f)
				BlastTimer -= Time.deltaTime;
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

	public void SendToBarn(Barn playersBarn)
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
