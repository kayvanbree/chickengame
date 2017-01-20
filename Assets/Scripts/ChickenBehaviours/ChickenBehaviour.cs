using UnityEngine;
/// <summary>
/// Defines a SteeringBehaviour for an AI
/// </summary>
public abstract class ChickenBehaviour
{
    public Chicken Chicken { get; set; }

    protected ChickenBehaviour(Chicken chicken)
    {
        Chicken = chicken;
    }

    /// <summary>
    /// Updates the movement of the chicken
    /// </summary>
    public abstract Vector3 GetForce(GameObject target);
}
