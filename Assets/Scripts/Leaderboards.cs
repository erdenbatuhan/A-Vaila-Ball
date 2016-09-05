//
//  Leaderboards.cs
//  Flappy Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboards : MonoBehaviour {

	private const string URL_GET_LEADERBOARDS = "http://138.68.143.170/FlappyBall/lb721gf126f1267e2neh21w21gd13f31f13.php";
	private const string CONSOLE_INITIAL = "  ~ Console: ";
	[SerializeField] private Text console;
	private string[] leaderboards = null;
	public GameObject userField;
	public GameObject mainMenu;

	public void handleLeaderboards() {
		mainMenu.SetActive(false);

		foreach (Transform child in transform)
			child.gameObject.SetActive(true);

		StartCoroutine(getLeaderboards());
	}

	private IEnumerator getLeaderboards() {
		WWWForm form = new WWWForm();
		form.AddField("form_hash", UserAuthentication.getHashCode());

		WWW www = new WWW(URL_GET_LEADERBOARDS, form);
		console.text = CONSOLE_INITIAL + "Getting leaderboards data....";
		yield return www; // Wait www.

		if (www.error != null) {
			if (www.error.Contains("Network"))
				console.text = CONSOLE_INITIAL + "Please check your internet connection....";
			else
				console.text = CONSOLE_INITIAL + www.error;
		} else {
			console.text = CONSOLE_INITIAL + "Top players are being shown....";
			leaderboards = www.text.Split('|');

			showLeaderboards();
		}
	}

	private void showLeaderboards() {
		for (int p = 1, i = 0, y = 730; i < leaderboards.Length - 1 && y >= 130; p++, i += 2, y -= 60) {
			string userInformation = p + ". " + leaderboards[i] + " has accumulated " + leaderboards[i + 1] + " points.";
			GameObject u = Instantiate(userField);

			if (Ball.getUsername() == leaderboards[i])
				u.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red;

			u.transform.parent = transform;
			u.transform.GetChild(0).gameObject.GetComponent<Text>().text = userInformation;
			u.transform.localPosition = new Vector2(803, y);
			u.transform.localScale = new Vector3(2.0075f, 2.0075f, 1);
		}
	}

	public void back() {
		mainMenu.SetActive(true);

		foreach (Transform child in transform)
			child.gameObject.SetActive(false);
		
		console.text = CONSOLE_INITIAL + "Please do not forget to save your game!";
	}
}