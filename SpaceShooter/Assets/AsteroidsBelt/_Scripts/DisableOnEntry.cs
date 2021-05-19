//-----------------------------------------------------------------------------------------		
//Disable/destroy any object that entries the trigger
//-----------------------------------------------------------------------------------------		
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableOnEntry : MonoBehaviour 
{
	public bool alsoDestroy = false;


	//-----------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other) 
	{
		if (alsoDestroy)
			Destroy (other.gameObject);
		else
			other.gameObject.SetActive (false);
		
	}

	//-----------------------------------------------------------------------------------------
}