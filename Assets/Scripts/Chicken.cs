using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
   // public GameObject Target;
    public Vector3 Velocity { get; set; }

    public float maxSpeed = 15f;
    public float maxForce = 1f;
    public float maxTurn = 1f;

    // Radius for barn finding
    public float radius = 1f;

    // The dps of the chicken to the barn
    public float dps = 1f;

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
	
	void Update ()
    {
       
    }

    public void AttackBarn(GameObject gameObject)
    {
        Barn barn = gameObject.GetComponent<Barn>();
        if(barn != null)
        {
            StartCoroutine(Attack(barn));
            StopCoroutine(Attack(barn));
        } 
    }

    IEnumerator Attack(Barn barn)
    {
        // Check if we are in correct state and radius
        while (chickenState == ChickenState.AttackBase && IsNearBarn(barn))
        {
            barn.Attack(dps);
            yield return new WaitForSeconds(1f);
        }
    }

    private bool IsNearBarn(Barn barn)
    {
        return Vector3.Distance(transform.position, barn.transform.position) <= radius;
    }

	public void GoToBarn(GameObject barn)
	{
		// start coroutine of moving towards target
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
        chickenState = ChickenState.AttackBase;
        AttackBarn(barnTransform.gameObject);
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
