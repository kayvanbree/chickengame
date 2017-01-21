using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    public float hp = 100;

	public void Attack(float damage)
    {
        hp -= damage;
    }
}
