//-----------------------------------------------------------------------------------------
// Script creates batch of bonuses and processes their appearance according to specified rules
//-----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PoolManager))]
public class BonusGenerator : MonoBehaviour 
{
	public float startDelay;			// Delay  till firs bonus will appear in game
	public float delay;					// Delay between bunuses appearance
	public int generationRange;			// Max distance(from origin) for generation 
	public Transform customParent;		// Custom parent object
	public float speed = 10;

	// Important internal variable - please don't change it blindly
	float nextGenerationTime;
	PoolManager poolManager;
	GameObject newBonus;


	//---------------------------------------------------------------------------------------
	// Initialize internal variables
	void Start () 
	{
		Random.InitState (System.DateTime.Now.Millisecond);

		poolManager = GetComponent<PoolManager>();
	
		nextGenerationTime = Time.time + startDelay;
	}

	//---------------------------------------------------------------------------------------
	// Process randomized bonuses appearance in game
	void Update () 
	{ 
		if (nextGenerationTime < Time.time)
		{
			newBonus = poolManager.GetRandomObject();

			if (newBonus)
			{
				newBonus.tag = "Bonus";
				newBonus.gameObject.transform.position = new Vector3 (
																		transform.position.x + Random.insideUnitCircle.x * generationRange,																		
																		transform.position.y + Random.insideUnitCircle.y * generationRange / 2,
																		transform.position.z
																	); 
				if (customParent)
						newBonus.transform.parent = customParent;

				newBonus.GetComponent<Rigidbody>().velocity = transform.forward * -speed;
				newBonus.GetComponent<Rigidbody>().AddTorque(Vector3.up * speed*Random.Range(-100, 100));
			} 

			nextGenerationTime = Time.time + delay;
		}

	}

	//------------------------------------------------------------------
}