//---------------------------------------------------------------------------
// Simple scripts to manage game sounds and music
//---------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour 
{
	public AudioSource sourceSounds;	// Link to AudioSource used for SFX
	public AudioSource sourceMusic;		// Link to AudioSource used for background music


	//============================================================================
	// Enable or disable  AudioSources acording to their previous(saved) state 
	void Awake() 
	{
		if (sourceMusic == null)
			sourceMusic = gameObject.AddComponent<AudioSource> ();
		
		if (sourceSounds == null)
			sourceSounds = gameObject.AddComponent<AudioSource> ();

			
		if (PlayerPrefs.HasKey("Slicer_MusicEnabled")) 
			sourceMusic.enabled = LoadBool("Slicer_MusicEnabled");	
		
		if (PlayerPrefs.HasKey("Slicer_SoundsEnabled")) 
			sourceSounds.enabled = LoadBool("Slicer_SoundsEnabled");
		
	}

	//---------------------------------------------------------------------------
	// Returns is Music enabled or not
	public bool GetMusicEnabled()
	{ 
		return sourceMusic.enabled;
	}

	//---------------------------------------------------------------------------
	// Returns is SFX enabled or not
	public bool GetSoundsEnabled()
	{ 
		return sourceSounds.enabled;
	}

	//---------------------------------------------------------------------------
	// Enable/disable SFX 
	public void SoundsEnabled (bool _activated) 
	{
		sourceSounds.enabled = _activated;	
		SaveBool ("Slicer_SoundsEnabled", _activated);
	}

	//---------------------------------------------------------------------------
	// Enable/disable SFX - resulted state will be opposite to initial
	public void SoundsSwitch () 
	{
		sourceSounds.enabled = !sourceSounds.enabled;	
		SaveBool ("Slicer_SoundsEnabled", sourceSounds.enabled);
	}

	//---------------------------------------------------------------------------
	// Enable/disable Music
	public void MusicEnabled (bool _activated) 
	{
		sourceMusic.enabled = _activated;	
		SaveBool ("Slicer_MusicEnabled", _activated);
	}

	//---------------------------------------------------------------------------
	// Enable/disable Music - resulted state will be opposite to initial
	public void MusicSwitch () 
	{
		sourceMusic.enabled = !sourceMusic.enabled;	
		SaveBool ("Slicer_MusicEnabled", sourceMusic.enabled);
	}

	//---------------------------------------------------------------------------
	// Play custom AudioClip(instead of current one) as SFX
	public void PlaySound (AudioClip _newClip) 
	{
		if (sourceSounds.enabled  &&  _newClip != null)
		{
			if (_newClip != sourceSounds.clip) 
				sourceSounds.clip = _newClip;
			
			sourceSounds.Play();
		}

	}


	public void PlaySoundOnce (AudioClip _newClip) 
	{
		if (sourceSounds.enabled  &&  _newClip != null)
				sourceSounds.PlayOneShot(_newClip);		
	}

	//---------------------------------------------------------------------------
	// Play custom AudioClip(instead of current one) as background Music
	public void PlayMusic (AudioClip _newClip) 	
	{
		if (sourceMusic.enabled)
			if (_newClip != sourceMusic.clip) 
			{
				sourceMusic.clip = _newClip;				
				sourceMusic.Play ();
			}

	}

	//---------------------------------------------------------------------------
	// Emulates saving boolean value to Player prefs
	public void SaveBool (string _key, bool _value)
	{
		PlayerPrefs.SetInt(_key, _value ? 1 : 0);
	}

	//---------------------------------------------------------------------------
	// Emulates loading boolean value from Player prefs
	public bool LoadBool (string _key)
	{
		return (PlayerPrefs.GetInt(_key) > 0 ? true : false);			
	}

	//---------------------------------------------------------------------------
}