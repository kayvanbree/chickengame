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

	public ParticleSystem SmokeParticle;
	public ParticleSystem FireParticle1;
	public ParticleSystem FireParticle2;
	public ParticleSystem FireParticle3;
	public ParticleSystem FireParticle4;

	void Start()
    {
        HealthBar.color = Color.Lerp(Color.red, Color.green, hp / maxHp);

		SmokeParticle.Stop();
		FireParticle1.Stop();
		FireParticle2.Stop();
		FireParticle3.Stop();
		FireParticle4.Stop();
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

		if (hp < 50)
		{
			if (!SmokeParticle.isPlaying)
				SmokeParticle.Play();
		}
		if (hp < 40)
		{
			if (!FireParticle1.isPlaying)
				FireParticle1.Play();
		}
		if (hp < 30)
		{
			if (!FireParticle2.isPlaying)
				FireParticle2.Play();
		}
		if (hp < 20)
		{
			if (!FireParticle3.isPlaying)
				FireParticle3.Play();
		}
		if (hp < 10)
		{
			if (!FireParticle4.isPlaying)
				FireParticle4.Play();
		}
	}
}
