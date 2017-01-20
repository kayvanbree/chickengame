using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the chicken towards the target
/// </summary>
public class Seek : ChickenBehaviour
{
    public Seek(Chicken chicken) : base(chicken)
    {
        
    }

    /// <summary>
    /// Return the force we need to move to the target
    /// </summary>
    /// <returns></returns>
    public override Vector3 GetForce(GameObject target)
    {
        Vector3 desiredVelocity = target.transform.position - Chicken.transform.position;
        if(desiredVelocity != Vector3.zero)
            desiredVelocity.Normalize();
        desiredVelocity *= Chicken.maxSpeed;

        return desiredVelocity - Chicken.Velocity;
    }
}
