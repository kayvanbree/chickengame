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

    // Wander stuff for idle
    public Vector3 wanderTarget;
    public float maxWander = 10f;
    public float wanderDistance = 1f;

    public float wanderMinX = -17.06f;
    public float wanderMaxX = 28.18f;
    public float wanderMinZ = -0.53f;
    public float wanderMaxZ = 28.88f;

    public enum ChickenState
	{
		Idle = 0,
		GoToBase,
		AttackBase,
		NumberStates
	}

    public ChickenState chickenState = ChickenState.Idle;
    IEnumerator chickenBehaviour;

	void Start ()
    {
        Velocity = Vector3.zero;
        SwitchBehaviour(Idle());
	}

    void SwitchBehaviour(IEnumerator coroutine)
    {
        if (chickenBehaviour != null) 
            StopCoroutine(chickenBehaviour);
        chickenBehaviour = coroutine;
        StartCoroutine(chickenBehaviour);
    }
	
	void Update ()
    {
       
    }

    void GoIdle()
    {
        chickenState = ChickenState.Idle;
        SwitchBehaviour(Idle());
    }

    IEnumerator Idle()
    {
        while(chickenState == ChickenState.Idle)
        {
            // Generate random Vector3
            if(Vector3.Distance(transform.position, wanderTarget) < wanderDistance || wanderTarget == null)
            {
                wanderTarget = GetRandomWanderTarget();
            }

            //Rotate to the target
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GetChickenDirection(wanderTarget)), Time.deltaTime * maxTurn);

            //Move to the target
            transform.position = Vector3.MoveTowards(transform.position, wanderTarget, maxSpeed * Time.deltaTime);

            yield return null;
        }
    }

    Vector3 GetRandomWanderTarget()
    {
        float randx = Random.Range(wanderMinX, wanderMaxX);
        float randz = Random.Range(wanderMinZ, wanderMaxZ);
        Vector3 randomVector = new Vector3(randx, 0f, randz);
        return randomVector;
    }

    public void AttackBarn(GameObject gameObject)
    {
        Barn barn = gameObject.GetComponent<Barn>();
        if(barn != null)
        {
            chickenState = ChickenState.AttackBase;
            SwitchBehaviour(Attack(barn));
        } 
    }

    IEnumerator Attack(Barn barn)
    {
        // Check if we are in correct state and radius
        while (chickenState == ChickenState.AttackBase && IsNearBarn(barn) && barn.State == BarnState.Alive)
        {
            barn.Attack(dps);
            yield return new WaitForSeconds(1f);
        }

        // Return to base if still allive and out of range
        if (barn.State == BarnState.Alive)
        {
            if (!IsNearBarn(barn))
            {
                GoToBarn(barn);
            }
        }
        else
        {
            // Idle when base down
            GoIdle();
        }
        
    }

    private bool IsNearBarn(Barn barn)
    {
        return Vector3.Distance(transform.position, barn.transform.position) <= radius;
    }

	public void GoToBarn(Barn barn)
	{
        if (barn != null && barn.State == BarnState.Alive)
        {
            // start coroutine of moving towards target
            chickenState = ChickenState.GoToBase;
            SwitchBehaviour(MoveTowardsBarn(barn.transform));
        }
    }

	IEnumerator MoveTowardsBarn(Transform barnTransform)
	{
		//Check if we are within the radius
		while (Vector3.Distance(transform.position, barnTransform.position) > radius)
		{
			//Rotate to the target
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GetChickenDirection(barnTransform.position)), Time.deltaTime * maxTurn);

			//Move to the target
			transform.position = Vector3.MoveTowards(transform.position, barnTransform.transform.position, maxSpeed * Time.deltaTime);

			yield return null;
		}
        AttackBarn(barnTransform.gameObject);
    }

    Vector3 GetChickenDirection(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.Normalize();
        return direction;
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
