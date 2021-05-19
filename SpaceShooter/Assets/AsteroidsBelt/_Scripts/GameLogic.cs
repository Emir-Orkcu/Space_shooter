using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLogic : MonoBehaviour
{
	public UI_Basic UI;
	public PlayerBehaviour playerBehaviour;
	public EnemySpawner enemyEmiter;

	public float levelTime = 60;
	public int starCost = 100;
	public float complexityIncrement = 2;
	public float IncComplexityTime = 30;

	GameObject player;
	float endLevelTime;
	int currentLevel = 0;
	int score = 0;
	int totalScore = 0; 
	bool endGame;


	//----------------------------------------------------------------------------------
	void Start()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		player = playerBehaviour.gameObject;
		playerBehaviour.UI = UI;

		IncComplexityTime = Time.time + IncComplexityTime;
		InitNextLevel ();
	}

	//-----------------------------------------------------------------------------------
	void Update ()
	{
		// IF GAME LOST
		if (playerBehaviour.GetCurrentLifes() < 1) 
		{
			player.SetActive (false);
			if (!endGame)  UI.ShowEndWindow (false, totalScore);
			endGame = true;
		}
		else
			// IF LEVEL PASSED
			if (Time.time > endLevelTime) 
			{
				enemyEmiter.gameObject.SetActive (false);
				player.transform.Translate(-player.transform.forward * playerBehaviour.speed * 2 * Time.deltaTime); 
				if (!endGame)  UI.ShowEndWindow (true, score, starCost * currentLevel);
				endGame = true;
			}
			else // INGAME
				{				
					// Increase game complexity if IncComplexityTime have elapsed
					if ( Time.time > IncComplexityTime) 
					{
						enemyEmiter.IncreaseComplexity (complexityIncrement);
						IncComplexityTime = Time.time + IncComplexityTime;
					}

					// Update stats UI 
					UI.SetStats (score, 1.0f-(endLevelTime-Time.time)/levelTime, playerBehaviour.RemainingLifePercent(),  playerBehaviour.GetWeaponLevel());

					//Pause/Unpause by ESC  
					if (Input.GetButtonDown ("Cancel"))  UI.Pause (true);
				}				

	}

	//-----------------------------------------------------------------------------------
	public void IncreaseScore (int _value) 
	{
		score += _value;
		totalScore += _value;
	}

	//-----------------------------------------------------------------------------------
	public void InitNextLevel () 
	{   
		endGame = false;
		currentLevel++;
		endLevelTime = Time.time + levelTime;
		score = 0;

		enemyEmiter.gameObject.SetActive (true);
		playerBehaviour.ResetTransform ();

		UI.LockHideCursor (true);

		UI.soundManager.PlayMusic (UI.ingameMusic);
		UI.ShowMessage("Earn " + starCost*3*currentLevel + " points to achieve 3 stars!");
	}

	//----------------------------------------------------------------------------------
}