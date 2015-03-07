using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float hitPoints = 100f;
	float currentHitPoints;

	// Use this for initialization
	void Start () {
		currentHitPoints = hitPoints;
	}

	public void TakeDamage(float damage)
	{
		currentHitPoints -= damage;
		Debug.Log ("Current health of " + this.name + " = " + currentHitPoints); 


		if (currentHitPoints <= 0)
		{
			Die ();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
