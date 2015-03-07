using UnityEngine;
using System.Collections;

public class DecalLife : MonoBehaviour {

	public float life = 1f;

	void Update()
	{
		life -= Time.deltaTime;


		if(life <= 0)
		{
			Destroy(gameObject);
		}
	}
}
