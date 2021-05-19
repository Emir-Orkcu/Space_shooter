//---------------------------------------------------------------------------
// Custom script to process basic functions for UI
//---------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_Basic : MonoBehaviour 
{
	[Header ("Common:")]
	public SoundManager soundManager; 	// Link to SoundManager object
	public Toggle musicButton;			// Button to turn on/Off Music
	public Toggle soundButton;			// Button to  on/Off Sounds
	public GameObject[] disableOnStart;	// List of objects to disable on start


	[Header ("For Ingame Menu:")]
	public Text message;				// Label for different messages text
	public Text scoreUI;				// Label for "Score" text
	public Slider timeUI;				// Remained Time
	public Image lifesUI;				// Link to object lifes UI
	public SpriteRenderer weaponUI;		// Link to weapons upgrade UI
	public GameObject pausePanel;

	[Header ("For EndGame Menu:")]
	public Button GO_NextLevel;			// Button to go to next level
	public Text GO_Caption;		 		// Text for GameOver caption
	public Image[] GO_Stars;			// Stars reward


	[Header ("Sounds:")]
	public AudioClip ingameMusic;
	public AudioClip sound_Lose;		// Sound for GameOver event
	public AudioClip sound_Win;			// Sound for Win event


	// Important internal variables - please don't change them blindly
	Vector3 blinkScale = new Vector3(1.3f, 1.3f, 1.3f);
	float initialWeaponUISize;


	//---------------------------------------------------------------------------
	//Initialization
	void Start () 
	{
		if (soundManager) 
		{
			if (musicButton)  musicButton.isOn = soundManager.GetMusicEnabled ();	
			if (soundButton)  soundButton.isOn = soundManager.GetSoundsEnabled ();
		}

		foreach (GameObject obj in disableOnStart)
			obj.SetActive (false);

		if (weaponUI) initialWeaponUISize = weaponUI.size.x;
	}

	//---------------------------------------------------------------------------
	// Load particular scene
	public void LoadScene (int _sceneID)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (_sceneID);
	}


	public void LoadScene (string _sceneName)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (_sceneName);
	}


	//---------------------------------------------------------------------------
	// Reload current scene
	public void ReloadScene ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	//---------------------------------------------------------------------------
	// Quit application
	public void Quit ()
	{
		Application.Quit();
	}

	//---------------------------------------------------------------------------
	// Pause/Unpause time
	public void Pause ()
	{
		Time.timeScale = (Time.timeScale == 0) ? 1 : 0;

		if (pausePanel)
			pausePanel.SetActive (Time.timeScale == 0);
			
		LockHideCursor (Time.timeScale != 0);
	}


	public void Pause (bool _enable)
	{
		Time.timeScale = _enable ? 0 : 1;

		if (pausePanel)
			pausePanel.SetActive (Time.timeScale == 0);
		
		LockHideCursor (!_enable);
	}

	//---------------------------------------------------------------------------
	// Pause/Unpause time
	public void LockHideCursor (bool _locked)
	{
		Cursor.lockState = _locked ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !_locked;
	}

	//---------------------------------------------------------------------------
	// Show message text
	public void ShowMessage (string _text) 
	{
		ShowMessage (message, _text);

	}

	public void ShowMessage (Text _object, string _text) 
	{
		if (_object) 
		{
			_object.text = _text;
			_object.gameObject.SetActive (true);
		}

	}

	//------------------------------------------------------------------
	// Increase object size for a moment
	public void Blink  (Transform _object, float _delay = 0.1f) 
	{
		StartCoroutine (BlinkCoroutine (scoreUI.transform, blinkScale, _delay));
	}


	IEnumerator BlinkCoroutine (Transform _object, Vector3 _newScale, float _delay = 0.1f)
	{
		Vector3 initialScale = _object.localScale;

		_object.localScale = _newScale;	
		 yield return new WaitForSeconds (_delay);
		_object.localScale = initialScale;
	}

	//------------------------------------------------------------------
	// Set object activeSelf with a delay
	public void DelayedActivation  (GameObject _object, bool _activate = true, float _delay = 0.1f) 
	{
		StartCoroutine (DelayedObjectActivation (_object, _activate, _delay));
	}


	IEnumerator DelayedObjectActivation (GameObject _object, bool _activate = true, float _delay = 0.1f)
	{
		yield return new WaitForSeconds (_delay);
		_object.SetActive (_activate);

	}


	//---------------------------------------------------------------------------
	// Set ingame stats (like score,lifes etc) to UI elements
	public void SetStats (int newScore, float remainingTime, float remainingLifesPercent, int newWeaponLevel)
	{
		scoreUI.text = newScore.ToString();	
		timeUI.value = remainingTime;
		lifesUI.rectTransform.localScale = new Vector2 (remainingLifesPercent, lifesUI.rectTransform.localScale.y); 
		weaponUI.size = new Vector2 (initialWeaponUISize * newWeaponLevel, weaponUI.size.y); 
	}

	//------------------------------------------------------------------
	// Show/Hide GameOver Menu 
	public void ShowEndWindow  (bool _win, int _score = 0, int _starCost = 0) 
	{
		LockHideCursor (false);
		GO_Caption.transform.parent.gameObject.SetActive (true);
		GO_NextLevel.gameObject.SetActive (_win);

		foreach (Image star in GO_Stars)
			star.gameObject.SetActive (false);		
		

		  if (!_win)
		   {
			 soundManager.PlayMusic(sound_Lose);
			 GO_Caption.text = "YOU LOSE \nTotal score:" + _score.ToString();
		   }
		  else 
			  {
				soundManager.PlayMusic(sound_Win);
				GO_Caption.text = "SUCCESS!";

				int starsNum = Mathf.Clamp (_score/_starCost, 0, GO_Stars.Length);
				for (int i = 0; i < starsNum; i++) 
					DelayedActivation(GO_Stars [i].gameObject, true, i*1.1f);
			  }

	}

	//---------------------------------------------------------------------------
	// Pause/Unpause time
	public void ShowIngameMenu (bool _enable)
	{
		lifesUI.transform.parent.gameObject.SetActive (_enable);
	}

	//---------------------------------------------------------------------------
}