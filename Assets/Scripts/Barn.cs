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
    // Delegate gets called when the barn is destroyed
    public delegate void BarnDestroyed();
    public BarnDestroyed barnDestroyed;

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
            if (barnDestroyed != null)
                barnDestroyed();
            else
                Debug.Log("A non player barn was destroyed!");
        }
        HealthBar.fillAmount = (float)hp / (float)maxHp;
        HealthBar.color = Color.Lerp(Color.red, Color.green, hp / maxHp);
    }
}
