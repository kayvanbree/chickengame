using UnityEngine;

public class Chicken : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Velocity { get; set; }

    public float maxSpeed = 15f;
    public float maxForce = 1f;
    public float maxTurn = 1f;

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

        Vector3 steeringForce = ChickenBehaviour.GetForce(Target);
        steeringForce.y = 0f;

        Velocity += steeringForce * Time.fixedDeltaTime;

        var q = Quaternion.LookRotation(Velocity - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, maxTurn * Time.deltaTime);

        transform.position += transform.forward * Time.deltaTime * maxSpeed;

    }
}
