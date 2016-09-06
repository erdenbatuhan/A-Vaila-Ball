//
//  MainMenu.cs
//  A Vaila Ball - Mobile
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private const string URL_SAVE = "http://138.68.143.170/VailaBall_DATA/Mobile_DATA/s82g932buig23bi3n2832ud3b23bf2382db33872.php";
	private const string CONSOLE_INITIAL = "  ~ Console: ";
	[SerializeField] private Text console;

	public void exit() {
		Application.Quit();
	}

	public void play() {
		Application.LoadLevel(1);
	}

	public void save() {
		StartCoroutine(saveGameToDatabase());
	}

	public void openShop() {
		Debug.Log("Shop!");
	}

	private IEnumerator saveGameToDatabase() {
		WWWForm form = new WWWForm();
		form.AddField("form_username", Ball.getUsername());
		form.AddField("form_score", Ball.getHighScore());
		form.AddField("form_hash", UserAuthentication.getHashCode());
		
		WWW www = new WWW(URL_SAVE, form);
		console.text = CONSOLE_INITIAL + "Your progress is being saved to the database....";
		yield return www; // Wait www.
		
		if (www.error != null)
			if (www.error.Contains("Network"))
				console.text = CONSOLE_INITIAL + "Please check your internet connection....";
			else
				console.text = CONSOLE_INITIAL + www.error;
		else
			console.text = CONSOLE_INITIAL + www.text;
	}
}
