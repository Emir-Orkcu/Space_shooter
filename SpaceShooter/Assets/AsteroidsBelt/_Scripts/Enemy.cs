using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour 
{
	public GameObject currentDetonator;
	public int life = 1;
	public int cost = 100;
	public float explosionLife = 10;


	//-----------------------------------------------------------------------------------
	void OnCollisionEnter(Collision other) 
	{
		// Destroy this object and create explosion at if life < 1  
		life--;

		if (life < 1  ||  other.collider.tag == "Player")
		{
			SpawnExplosion(other.gameObject);
			gameObject.SetActive(false);
		}
			
	}

	//-----------------------------------------------------------------------------------
	public void SpawnExplosion(GameObject hit)
	{
		// Create explosion using currentDetonator prefarb
		GameObject exp = Instantiate (currentDetonator, transform.position, Quaternion.identity);
		exp.SetActive(true);
		ShowPoints score = exp.GetComponent<ShowPoints> ();

		if (hit.tag == "Player" &&  score)
			score.enabled = false;
		else
		{
			if (score) 
			{
				score.enabled = true;
				score.point = cost;
			}
			hit.SetActive(false);
			GameObject.FindObjectOfType<GameLogic>().IncreaseScore (cost);
		}
		
		Destroy (exp, explosionLife); 
	}

	//-----------------------------------------------------------------------------------
}