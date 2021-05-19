using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{
	public int point;
	public string additionalText;
	public GUISkin PointSkin;
	public float AnimationSpeed = 50;
	public float AnimationTime = 3;
	public bool Enabled = false;

	float targY;
	Vector3 PointPosition;


	//========================================================================================================
	void Start() 
	{
		targY = Screen.height/2;
	}

	//---------------------------------------------------------------------------------------------------------		
	void OnGUI() 
	{
		if (Enabled)
		{
			GUI.skin = PointSkin;

			PointPosition = transform.position;
			Vector3 screenPos2 = Camera.main.WorldToScreenPoint (PointPosition);

			AnimationTime -= Time.deltaTime;
			GUI.color = new Color (1.0f,1.0f,1.0f, AnimationTime);
			GUI.Label (new Rect (screenPos2.x+2 , targY+2, 200, 150), additionalText + point.ToString());

			targY -= Time.deltaTime*AnimationSpeed;
		}

	}

	//---------------------------------------------------------------------------------------------------------		
	void Enable (bool isEnabled)
	{
		if(!Enabled)  SetPoints(1); 
		Enabled = isEnabled;	
	}

	//---------------------------------------------------------------------------------------------------------		
	void SetPoints(int _point) 
	{
		point = _point;
	}

	//---------------------------------------------------------------------------------------------------------		
}
