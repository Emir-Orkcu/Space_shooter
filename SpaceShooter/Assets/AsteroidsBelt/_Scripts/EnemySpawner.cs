//-----------------------------------------------------------------------------------------
// Create enemies and throw it to Player
//-----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PoolManager))]
public class EnemySpawner : MonoBehaviour 
{
	[Header ("Generation settings:")]
	public float startDelay;				// Delay  till firs bonus will appear in game
	public float delay = 2.5f;				// Delay between bunuses appearance
	public int generationRange = 10;		// Max distance(from origin) for generation 
	public Transform customParent;			// Custom parent object

	[Header ("Enemy settings:")]
	public float enemySpeed = 10;
	public int ignoreCollisionsLayer = 8;


	// Important internal variable - please don't change it blindly
	float nextGenerationTime;
	PoolManager poolManager;
	GameObject newEnemy;
	float maxScale;


	//---------------------------------------------------------------------------------------
	// Initialize internal variables
	void Start () 
	{
		Random.InitState (System.DateTime.Now.Millisecond);
		poolManager = GetComponent<PoolManager>();
		nextGenerationTime = Time.time + startDelay;

		Physics.IgnoreLayerCollision(ignoreCollisionsLayer, ignoreCollisionsLayer);
	}

	//---------------------------------------------------------------------------------------
	// Process randomized bonuses appearance in game
	void Update () 
	{ 
		// Generate and setup enemy
		if (nextGenerationTime < Time.time)
		{
			newEnemy = poolManager.GetRandomObject ();
			nextGenerationTime = Time.time + delay;

			if (newEnemy)
			{
				newEnemy.tag = "Enemy";
				newEnemy.gameObject.transform.position = new Vector3 (
																		transform.position.x + Random.insideUnitCircle.x * generationRange,					 
																		transform.position.y + Random.insideUnitCircle.y * generationRange / 2,
																		transform.position.z
																	); 

				if (customParent)
					newEnemy.transform.parent = customParent;


				newEnemy.GetComponent<Rigidbody>().velocity = transform.forward * -enemySpeed;
				newEnemy.GetComponent<Rigidbody>().AddTorque(Vector3.up * enemySpeed*Random.Range(-100, 100));

				float scaleInc = Random.Range (0, enemySpeed / (100 / maxScale) );
				newEnemy.transform.localScale += new Vector3(scaleInc, scaleInc, scaleInc);

				if (newEnemy.GetComponent<Enemy>()) 
					newEnemy.GetComponent<Enemy>().life += (int)(scaleInc * maxScale);		

				newEnemy.gameObject.SetActive(true);
			} 

		}

	}

	//---------------------------------------------------------------------------------------
	// Increase game complexity 
	public void IncreaseComplexity (float _complexityIncrement)
	{
		delay = delay / _complexityIncrement ;
		enemySpeed = enemySpeed * (_complexityIncrement*0.7f);
		maxScale = _complexityIncrement;
	}
		
	//------------------------------------------------------------------
}