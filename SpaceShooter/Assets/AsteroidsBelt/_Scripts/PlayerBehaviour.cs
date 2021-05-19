using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour 
{
	public Shooter weapon;
	public int maxLife = 10;
	public int maxWeaponLevel = 5;
	public float speed = 1;
	public Rect movementLimits = new Rect(-7, 0, 14, 10);

	[Header ("Sounds:")]
	public AudioClip fireSnd;
	public AudioClip pickupSnd;


	[HideInInspector]
	public UI_Basic UI;
	int weaponLevel;
	int life;
	Vector3 oldPosition;
	Vector3 initialPosition;
	Quaternion initialRotation;


	//==================================================================================
	void Awake () 
	{
		life = maxLife;
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	//-----------------------------------------------------------------------------------
	void Update () 
	{
		// If not Paused	       
		if (Time.timeScale != 0 )
		{
			oldPosition = transform.position; 

			// Calls the fire method when holding down ctrl or mouse
			if (Input.GetButton ("Fire1"))
				if (weapon.Fire())
					UI.soundManager.PlaySoundOnce(fireSnd);
			

			// Move object according to Mouse offset in Xlimits
			transform.Translate  (Input.GetAxis ("Mouse X") * speed, 0, Input.GetAxis ("Mouse Y") * speed); 			

			if (!movementLimits.Contains (transform.position))
				transform.position = oldPosition; 
			else
				transform.RotateAround (Vector3.zero, Vector3.forward,  Input.GetAxis ("Mouse X") *1.5f );
		}

	}

	//-----------------------------------------------------------------------------------
	void OnCollisionEnter(Collision other) 
	{
		// Decrease life if collided with Enemy
		if (other.collider.tag == "Enemy") 
			ApplyDamage (1);	

		if (other.collider.tag == "Bonus")
			other.gameObject.GetComponent<Bonus>().ActivateBonus (this);	
	}

	//-----------------------------------------------------------------------------------
	public void ApplyDamage (int _value) 
	{
		if (life >= _value) 
			life -= _value;	
		
		if (weaponLevel >= _value) 
			weaponLevel -= _value;	

		weapon.SetUpgradeLevel (weaponLevel);
	}

	//-----------------------------------------------------------------------------------
	public int GetCurrentLifes ()
	{
		return life;
	}

	//-----------------------------------------------------------------------------------
	public void ReplenishLife (int _value) 
	{
		if (life <= (maxLife - _value)) 
		{
			life += _value;	
			UI.soundManager.PlaySoundOnce(pickupSnd);
		}	

	}

	//-----------------------------------------------------------------------------------
	public void UpgradeWeapon(int _value) 
	{
		if (weaponLevel <= (maxWeaponLevel - _value)) 
		{
			weaponLevel += _value;	
			weapon.SetUpgradeLevel (weaponLevel);
			UI.soundManager.PlaySoundOnce(pickupSnd);
		}

	}

	//-----------------------------------------------------------------------------------
	public int GetWeaponLevel()
	{
		return weaponLevel;
	}

	//---------------------------------------------------------------------------------
	public void ResetTransform () 
	{
		transform.position = initialPosition;
		transform.rotation = initialRotation;
	}

	//---------------------------------------------------------------------------------
	public float RemainingLifePercent () 
	{
		return (float)life / (float)maxLife;
	}

	//-----------------------------------------------------------------------------------
}