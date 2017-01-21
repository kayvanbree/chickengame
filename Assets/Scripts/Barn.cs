using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum BarnState
{
    Alive,
    Dead,
    NumberStates
}

public class Barn : MonoBehaviour
{
    public float maxHp = 100;
    public float hp = 100;

    public BarnState State = BarnState.Alive;
    public Image HealthBar;

    void Start()
    {
        HealthBar.color = Color.Lerp(Color.red, Color.green, hp / maxHp);
    }

	public void Attack(float damage)
    {
        if (State == BarnState.Dead) return;

        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            State = BarnState.Dead;
        }
        HealthBar.fillAmount = (float)hp / (float)maxHp;
        HealthBar.color = Color.Lerp(Color.red, Color.green, hp / maxHp);
    }
}
