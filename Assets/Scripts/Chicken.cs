using UnityEngine;

public class Chicken : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Velocity { get; set; }

    public float maxSpeed = 15f;
    public float maxForce = 1f;
    public float maxTurn = 1f;
    public float radius = 1f;

    private ChickenBehaviour ChickenBehaviour { get; set; }

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
        if(ChickenBehaviour ==  null)
            ChickenBehaviour = new Seek(this);

        Vector3 targetLocation = ChickenBehaviour.GetTargetPosition(Target);

        //Find distance
        Vector3 direction = targetLocation - transform.position;
        //Check if we are within the radius
        if (direction.magnitude < radius)
            return;
        //Move to the target
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, maxSpeed * Time.deltaTime);
        //Rotate to the target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), maxTurn * Time.deltaTime);
    }
}
