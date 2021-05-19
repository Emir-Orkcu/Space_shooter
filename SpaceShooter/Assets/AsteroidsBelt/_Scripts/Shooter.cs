using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PoolManager))]
public class Shooter : MonoBehaviour 
{
	public float delay = 0.5f;			// Delay between bunuses appearance
	public float speed = 10;			// Bullet speed
	public Transform customParent;		// Custom parent object

	// Important internal variable - please don't change it blindly
	float nextGenerationTime;
	PoolManager poolManager;
	GameObject newBullet;
	float initialDelay;


	//=================================================================================================
	void Start ()
	{
		Random.InitState (System.DateTime.Now.Millisecond);

		poolManager = GetComponent<PoolManager>();
		nextGenerationTime = Time.time + delay;
		initialDelay = delay;
	}

	//--------------------------------------------------------------------------------------------------
	public bool Fire ()
	{
		if (nextGenerationTime < Time.time)
		{
			newBullet = poolManager.GetRandomObject();
			if (newBullet)
			{
				newBullet.tag = "Bullet";
				Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), transform.parent.gameObject.GetComponent<Collider>());
				newBullet.gameObject.transform.position = transform.position;
				newBullet.gameObject.transform.rotation = transform.rotation;

				if (customParent)
					newBullet.transform.parent = customParent;

				newBullet.GetComponent<Rigidbody>().velocity = transform.forward * speed;
				newBullet.SetActive (true);
			} 

			nextGenerationTime = Time.time + delay;
			return true;
		}

		return false;
	}

	//--------------------------------------------------------------------------------------------------
	public void SetUpgradeLevel (int _value) 
	{		
		delay = initialDelay/(_value+1);
		nextGenerationTime = 0;
	} 

	//--------------------------------------------------------------------------------------------------
}