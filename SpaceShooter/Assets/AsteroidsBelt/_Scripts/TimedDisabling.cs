//--------------------------------------------------------------------------------------------------------
// Example script.
// The simple script, that disables/destruct current object after lifeTime time
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimedDisabling : MonoBehaviour 
{
	public float lifeTime = 3;  						// After this time object will be destroyed
	public Vector2 randomizedLifeTime = Vector2.zero; 	// Randomize lifeTime in limits		
	public bool onlyIfNotVisible = false;   			// Disable only when object is no longer visible by any camera
	public bool alsoDestroy = false;					// Script will also destroy the object
	 
	// Important internal variables, please don't change them blindly
	bool ReadyForDisabling;
	float TimeForDisabling;
	Vector3 initialScale;

	//=======================================================================================================
	// Setup Time when object will be disabled
	void Start () 
	{
		initialScale = transform.localScale;
		ReadyForDisabling = true;

		if(randomizedLifeTime != Vector2.zero)
		{
			Random.InitState (System.DateTime.Now.Millisecond);
			TimeForDisabling = Time.time + Random.Range(randomizedLifeTime.x, randomizedLifeTime.y);
		}
		else
			TimeForDisabling = lifeTime + Time.time;
	} 

	//---------------------------------------------------------------------------------------------------------	
	// Reset Time when object will be disabled if object had re-enabled
	void OnEnable () 
	{
		Start();
	}

	//---------------------------------------------------------------------------------------------------------	
	// Reset Time when object will be disabled if object had re-enabled
	void OnDisable () 
	{
		transform.localScale = initialScale;
		if (onlyIfNotVisible)  ReadyForDisabling = false;
	}

	//---------------------------------------------------------------------------------------------------------	
	// Check visibility to allow destroying (only if onlyIfNotVisible=true)
	void OnBecameInvisible () 
	{
		if (onlyIfNotVisible)  ReadyForDisabling = true;
	}

	//----------------
	void OnBecameVisible () 
	{
		if (onlyIfNotVisible)  ReadyForDisabling = false;
	}

	//---------------------------------------------------------------------------------------------------------	
	// Disable/Destroy the object if it lifeTime has expired
	void Update () 
	{
		if (Time.time > TimeForDisabling  &&  ReadyForDisabling) 
		{
			gameObject.SetActive (false); 

			if (alsoDestroy) 
				Destroy (gameObject); 
		}

	}

	//---------------------------------------------------------------------------------------------------------
}