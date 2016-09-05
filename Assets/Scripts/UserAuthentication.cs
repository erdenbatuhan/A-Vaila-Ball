//
//  UserAuthentication.cs
//  Flappy Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserAuthentication : MonoBehaviour {
	
	private const string URL_REGISTER = "http://138.68.143.170/FlappyBall/r88732gb23g7g3b3u2g1873rb13ubdn131mc218nec811.php";
	private const string URL_LOGIN = "http://138.68.143.170/FlappyBall/l612r76r211f6r31fy3v186ef11b631t631f.php";
	private const string URL_GET_USER_INFO = "http://138.68.143.170/FlappyBall/g2t7g3129em281f1hf12h12312.php";
	private const string HASH_CODE = "y17rct1y894bt1274128c41n2y7e12he9128bcr7g21wqbuqiubqwpqhex27egc12hr137r";
	private const string CONSOLE_INITIAL = "  ~ Console: ";
	[SerializeField] private Text console;
	[SerializeField] private InputField username;
	[SerializeField] private InputField password;
	[SerializeField] private GameObject usernameBox;
	public GameObject mainMenu;

	private void Update() {
		if (Ball.getUsername() != null) {
			mainMenu.SetActive(true);
			usernameBox.SetActive(true);
			usernameBox.transform.GetChild(0).gameObject.GetComponent<Text>().text = Ball.getUsername();

			Destroy(this.gameObject);
		}
	}

	public void handleLogin() {
		StartCoroutine(login());
	}

	private IEnumerator login() {
		WWWForm form = new WWWForm();
		form.AddField("form_username", username.text);
		form.AddField("form_password", password.text);
		form.AddField("form_hash", HASH_CODE);

		WWW www = new WWW(URL_LOGIN, form);
		console.text = CONSOLE_INITIAL + "Login in progress....";
		yield return www; // Wait www.

		if (www.error != null)
			if (www.error.Contains("Network"))
				console.text = CONSOLE_INITIAL + "Please check your internet connection....";
			else
				console.text = CONSOLE_INITIAL + www.error;
		else
			console.text = CONSOLE_INITIAL + www.text;

		if (www.text.Contains("succeed"))
			StartCoroutine(getUserInfo());
		else
			resetEntries();
	}

	private IEnumerator getUserInfo() {
		WWWForm form = new WWWForm();
		form.AddField("form_username", username.text);
		form.AddField("form_hash", HASH_CODE);

		WWW www = new WWW(URL_GET_USER_INFO, form);
		console.text = CONSOLE_INITIAL + "Getting user information....";
		yield return www; // Wait www.

		if (www.error != null) {
			if (www.error.Contains("Network"))
				console.text = CONSOLE_INITIAL + "Please check your internet connection....";
			else
				console.text = CONSOLE_INITIAL + www.error;
		} else {
			console.text = CONSOLE_INITIAL + "Please do not forget to save your game!";
			string[] output = www.text.Split('|');

			Ball.setHighScore(int.Parse(output[1]));
			Ball.setUsername(output[0]);

			usernameBox.SetActive(true);
			usernameBox.transform.GetChild(0).gameObject.GetComponent<Text>().text = Ball.getUsername();
		}
	}	

	public void handleRegister() {
		StartCoroutine(register());
	}
	
	private IEnumerator register() {
		WWWForm form = new WWWForm();
		form.AddField("form_username", username.text);
		form.AddField("form_password", password.text);
		form.AddField("form_hash", HASH_CODE);
		
		WWW www = new WWW(URL_REGISTER, form);
		console.text = CONSOLE_INITIAL + "Register in progress....";
		yield return www; // Wait www.

		if (www.error != null)
			if (www.error.Contains("Network"))
				console.text = CONSOLE_INITIAL + "Please check your internet connection....";
			else
				console.text = CONSOLE_INITIAL + www.error;
		else
			console.text = CONSOLE_INITIAL + www.text;
		
		resetEntries();
	}
	
	private void resetEntries() {
		username.text = "";
		password.text = "";
	}

	/* ---------------------------- GETTERS & SETTERS ---------------------------- */

	public static string getHashCode() {
		return HASH_CODE;
	}
}