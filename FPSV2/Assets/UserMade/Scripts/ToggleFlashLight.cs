using UnityEngine;
using System.Collections;

public class ToggleFlashLight : MonoBehaviour {

	public GameObject FlashLight;
	public bool Lightenabled;

	void Start()
	{
		FlashLight.SetActive(false);
		Lightenabled = false;
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.F) && Lightenabled == false)
		{
			Lightenabled = true;
			FlashLight.SetActive(true);
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.F) && Lightenabled == true)
			{
				Lightenabled = false;
				FlashLight.SetActive(false);
			}
		}

	}
}
