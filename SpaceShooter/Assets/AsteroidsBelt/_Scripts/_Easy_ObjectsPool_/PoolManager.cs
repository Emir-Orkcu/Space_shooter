//--------------------------------------------------------------------------------------------------------
// This is the main script to handle  objects pool.
// Script  allow to create pool of preseted objects in needed quantity and  handle  their  extraction or pooling back
// If needed you can add any object to pool. It's  better to use PooledObject script with objects to add.
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour 
{
	// Class to specify object parameters to preload to pool
	[System.Serializable]
	public class PoolingObjects
	{
		public string customName;				// Specify custom name if  you want all instances to be renamed
		public GameObject objectPrefab;			// Prefab to instanciate and  pool
		public int quantity = 1;				// Quantity of instances
		public bool addAutoPoolScript = true;  	// Add script to pool object back automatically
	}


	public PoolingObjects[] preloadObjects; 				// List of objects that should be preloaded to the pool
	[HideInInspector]
	public List<GameObject> Pool = new List<GameObject> ();	// Array of all stored objects


	//=======================================================================================================
	// Preload all objects in needed quantity (specified in peloadObjects list) to pool 
	public void Awake () 
	{
		GameObject newObj;
		PooledObject gameObjScript;

		Random.InitState (System.DateTime.Now.Millisecond);


		for (int i=0; i < preloadObjects.Length; i++)
		{
			if (preloadObjects[i].customName == "") 
				preloadObjects[i].customName = preloadObjects[i].objectPrefab.name;

			if (preloadObjects[i] != null)
				for (int j=0; j < preloadObjects[i].quantity; j++)
				{
					// Instantiate object and reset it transformation. Assign it as child to Pool
					newObj = Instantiate(preloadObjects[i].objectPrefab) as GameObject;
					newObj.name = preloadObjects[i].customName;

					// Add PooledObject script, if needed		 
					if (preloadObjects[i].addAutoPoolScript)
					{
						newObj.AddComponent<PooledObject>();
						gameObjScript = newObj.GetComponent<PooledObject>();
						gameObjScript.parentPool = this;
					}

					// Add the new object to pool
					PoolObject(newObj, false);
				}

		}


	}

	//------------------------------------------------------------------------
	// Extract gameObject from pool by name (customName if it's  specified)
	public GameObject GetObjectByName(string objectName)
	{
		GameObject gameObj;

		for (int i=0; i < Pool.Count; i++)
			if (Pool[i] != null)
				if (Pool[i].name == objectName)
				{		
				  	Pool[i].transform.parent = null;
					Pool[i].SetActive(true);
					gameObj = Pool [i];

					Pool.RemoveAt(i);

					return gameObj;
				}

		return null;
	}

	//------------------------------------------------------------------------
	// Extract gameObject from pool by ID in the pool
	public GameObject GetObjectByID(int id)
	{
		GameObject gameObj;

		if (Pool.Count > id)
		{
			gameObj = Pool[id];
			Pool.RemoveAt(id);

			if (gameObj)
			{
				gameObj.transform.parent = null;
				gameObj.SetActive(true);

				return gameObj;
			}
		}

		return null;
	}

	//------------------------------------------------------------------------
	// Extract random gameObject from pool
	public GameObject GetRandomObject()
	{
		GameObject gameObj;

		if (Pool.Count > 0)  
		{
			int id = Mathf.FloorToInt(Random.Range(0, Pool.Count));
			gameObj = Pool[id];
			Pool.RemoveAt(id);

			if (gameObj)
			{
				gameObj.transform.parent = null;
				gameObj.SetActive(true);

				return gameObj;
			}
		}

		return null;

	}

	//------------------------------------------------------------------------
	// Pool object back to pool and reset object transformation. 
	// Set PreloadedTypeOnly to true if  you'd like to allow addingonly objects with one of the names from peloadObjects
	public void PoolObject (GameObject _object, bool PreloadedTypeOnly)
	{
		if (_object)
			if (!PreloadedTypeOnly) 
			{
				ResetObjectTransform(_object);
				_object.SetActive(false);			
				_object.transform.parent = transform;
				Pool.Add(_object);
			}
			else  // If only objects with preseted names allowed
				for (int i=0; i<preloadObjects.Length; i++)
				{
					if(preloadObjects[i].customName == _object.name)
					{
						ResetObjectTransform(_object);
						_object.SetActive(false);
						_object.transform.parent = transform;
						Pool.Add(_object);
					}
				}

	}

	//------------------------------------------------------------------------
	// Reset object position and rotation(and make it a child of this game object) and disable it
	public GameObject ResetObjectTransform(GameObject _object)
	{
		if (_object)
		{
			_object.transform.parent = transform;
			_object.transform.localPosition = Vector3.zero;
			_object.transform.localRotation = transform.localRotation;

			if(_object.GetComponent<Rigidbody>())
			{
				_object.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
				_object.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
			}

			return _object;
		}

		return null;
	}

	//------------------------------------------------------------------------
}