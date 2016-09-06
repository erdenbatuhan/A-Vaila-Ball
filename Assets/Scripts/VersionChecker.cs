//
//  VersionChecker.cs
//  A Vaila Ball - Mobile
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VersionChecker : MonoBehaviour {

	private const string URL_VERSION = "http://138.68.143.170/VailaBall_DATA/Mobile_DATA/v2148921ub21f21nf12m1920j129.txt";
	private const string URL_UPDATE_PAGE = "http://vailaball.me/Download";
	private const string VERSION = "1.0.3";
	private const string CONSOLE_INITIAL = "  ~ Console: ";
	[SerializeField] private Text console;
	private string[] websitesToTest = { "http://www.google.com", "http://www.ebay.com" };
	public GameObject userAuthentication;

	private void Start() {
		startChecking();
	}

	private void startChecking() {
		StartCoroutine(checkInternetConnection());
	}

	private IEnumerator checkInternetConnection() {
		WWW www = new WWW("http://www.google.com");
		yield return www;

		for (int i = 0; www.error != null; i++) {
			console.text = CONSOLE_INITIAL + "Please check your internet connection....";

			www = new WWW(websitesToTest[i % 2]);
			yield return www;
		}

		StartCoroutine(checkVersion());
	}
	
	private IEnumerator checkVersion() {
		WWW www = new WWW(URL_VERSION);
		console.text = CONSOLE_INITIAL + "Checking version....";
		yield return www;

		if (VERSION != getVersion(www.text)) {
			console.text = CONSOLE_INITIAL + "Please update your game to continue....";

			foreach (Transform child in transform)
				child.gameObject.SetActive(true);
		} else {
			if (Ball.getUsername() == null)
				console.text = CONSOLE_INITIAL + "Version check succeeded!";
			else
				console.text = CONSOLE_INITIAL + "Please do not forget to save your game!";

			userAuthentication.SetActive(true);
			Destroy(gameObject);
		}
	}

	private string getVersion(string text) {
		string version = text.Substring(text.IndexOf('>') + 1, 5);

		return version;
	}

	public void showUpdatePage() {
		Application.OpenURL(URL_UPDATE_PAGE);
		Application.Quit();
	}
}
