using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
   // public GameObject Target;
    public Vector3 Velocity { get; set; }

    public float maxSpeed = 15f;
    public float maxForce = 1f;
    public float maxTurn = 1f;
    public float radius = 1f;

    private ChickenBehaviour ChickenBehaviour { get; set; }

	Player ownedPlayer = null;

	enum ChickenState
	{
		Idle = 0,
		GoToBase,
		AttackBase,
		NumberStates
	}

	ChickenState chickenState = ChickenState.Idle;

	void Start ()
    {
        Velocity = Vector3.zero;
        //Target = null;
	}
	
	/// <summary>
    /// Moves the chicken
    /// </summary>
	void Update ()
    {
       // if(ChickenBehaviour ==  null)
       //     ChickenBehaviour = new Seek(this);

       // Vector3 targetLocation = ChickenBehaviour.GetTargetPosition(Target);
    }

	public void GoToBarn(GameObject barn)
	{
		// start coroutine of moving towards target
		StopCoroutine(MoveTowardsBarn(barn.transform));
		StartCoroutine(MoveTowardsBarn(barn.transform));
		StopCoroutine(MoveTowardsBarn(barn.transform));
	}

	IEnumerator MoveTowardsBarn(Transform barnTransform)
	{
		//Find distance
		Vector3 direction = barnTransform.position - transform.position;
		direction.Normalize();
		//Check if we are within the radius
		while (Vector3.Distance(transform.position, barnTransform.position) > radius)
		{
			//Rotate to the target
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * maxTurn);

			//Move to the target
			transform.position = Vector3.MoveTowards(transform.position, barnTransform.transform.position, maxSpeed * Time.deltaTime);

			yield return null;
		}
	}

	public void SetOwner(Player newPlayer)
	{
		ownedPlayer = newPlayer;
	}

	//can return null
	public Player GetOwner()
	{
		return ownedPlayer;
	}
}
