//-----------------------------------------------------------------------------------------
// Script processes different effects when bonus is taken
//-----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bonus : MonoBehaviour 
{
	// Types of bonuses/effects
	public enum BonusType {AddLife, UpgradeWeapon, FreezeEveryone}

	public BonusType bonusType;		// Current bonus type (how it will affeect game)
	public int parameter = 1;		// Custom parameter to adjust bonus effects


	//=======================================================================================================
	// Process curent bonus effect. Can return an integer value (related to effect of the bonus)
	public void ActivateBonus (PlayerBehaviour player)
	{
		switch (bonusType)
		{
			case BonusType.AddLife:
				player.ReplenishLife(parameter);
				break;


			case BonusType.UpgradeWeapon:
				player.UpgradeWeapon(parameter);
				break;

		}

		gameObject.SetActive (false);
	}

	//-----------------------------------------------------------------------------------------------------
}