using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	[SerializeField] Camera cam;
	[SerializeField] GameObject player;

	[SerializeField] GameObject BulletHole;

	[SerializeField] GameObject BulletHoleWood;
	[SerializeField] GameObject BulletHoleGlass;
	[SerializeField] GameObject BulletHoleMetal;
	[SerializeField] GameObject BulletHoleStone;


	float cooldownRemaining;

	bool canFire = true;



	//information from weapon classes
	public AudioClip fire;
	public AudioClip reload;
	public float fireRate;
	public float damage;
	public int ammoCount;



	void Start()
	{
		cooldownRemaining = 0;

	}

	void Update () 
	{

		cooldownRemaining -= Time.deltaTime;

		if(Input.GetKey(KeyCode.Mouse0)&& !Input.GetKey(KeyCode.LeftShift) && ammoCount > 0 && cooldownRemaining <= 0 && canFire == true)
		{
			Fire ();
			ammoCount --;
			//Debug.Log (ammoCount);
		}

		if(Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.Mouse0)&& !Input.GetKey(KeyCode.LeftShift) && ammoCount < 25)
		{
			StartCoroutine("Reload");
			Debug.Log("Reloading");
		}
	}

	/*public virtual void getWeaponInfo(AudioClip fireGun, AudioClip reloadGun, float gunFireRate, float gunDamage, int gunAmmoCount)
	{
		fire = fireGun;
		reload = reloadGun;
		fireRate = gunFireRate;
		damage = gunDamage;
		ammoCount = gunAmmoCount;
	}
*/
	void Fire()
	{

		if(cooldownRemaining > 0)
		{
			return;
		}

		//plays audio at the players position
		AudioSource.PlayClipAtPoint(fire, new Vector3(transform.position.x, transform.position.y, transform.position.z));

		Ray ray = new Ray(cam.transform.position, cam.transform.forward);
		Transform hitTransform;
		Vector3 hitPoint;

		hitTransform = ClosestHit(ray, out hitPoint);

		if(hitTransform != null)
		{
			//Debug.Log (hitTransform.name);

			Health h = hitTransform.GetComponent<Health>();

			if(h != null)
			{
				h.TakeDamage(damage);
			}


		}

		
		cooldownRemaining = fireRate;
	}

	Transform ClosestHit(Ray ray, out Vector3 hitPoint)
	{
		RaycastHit[] hits = Physics.RaycastAll(ray);

		Transform closestHit = null;
		float distance = 0;
		hitPoint = Vector3.zero;

		foreach(RaycastHit hit in hits)
		{
			if(hit.transform != player.transform && (closestHit == null || hit.distance < distance))
			{
				closestHit = hit.transform;
				distance = hit.distance;
				hitPoint = hit.point;

				if(hit.transform.tag == "Enemy" || hit.transform.tag == "Decal")
				{

				}
				else
				{
					string matTag = hit.transform.tag;
					Debug.Log (matTag);

					switch(matTag)
					{
					case "Wood":
						Instantiate(BulletHoleWood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						break;
					case "Metal":
						Instantiate(BulletHoleMetal, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						break;
					case "Stone":
						Instantiate(BulletHoleStone, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						break;
					case "Glass":
						Instantiate(BulletHoleGlass, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						break;
					default:
						Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						break;
					}

				}

				//Debug.Log ("Closest hit: " + closestHit + " " + "Distance: " + distance + " " + "Hit Point: " + hitPoint);
			}
		}

		return closestHit;

	}


	IEnumerator Reload()
	{
		AudioSource.PlayClipAtPoint(reload, new Vector3(transform.position.x, transform.position.y, transform.position.z));
		canFire = false;
		ammoCount = 25;
		yield return new WaitForSeconds(2f);
		canFire = true;
		Debug.Log("Done Reloading");
	}



}
