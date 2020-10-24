using System;
using System.Collections.Generic;
using Mumi;
using UnityEngine;

public class TimeOutSaver : SingletonT<TimeOutSaver> {

	public int lastTime = 0;
	public int diffTime = 0;

	/////////////////////////////////////

	private void Awake ()
	{
		Debug.Log (DateTime.Now.Hour);
		lastTime = PlayerPrefs.GetInt ("lastTime");
		diffTime = (int)DateTime.Now.Hour - lastTime;
	}

	public void Save () 
	{

		lastTime = (int)DateTime.Now.Hour;
		PlayerPrefs.SetInt ("lastTime", lastTime);

	}

	public void Load () 
	{
		lastTime = PlayerPrefs.GetInt ("lastTime");
	}

	

}