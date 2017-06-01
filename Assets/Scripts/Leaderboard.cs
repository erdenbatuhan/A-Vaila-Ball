//
//  Leaderboard.cs
//  A Vaila Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {

	private const string URL_GET_LEADERBOARD = "http://138.68.143.170/VailaBall_DATA/Mobile_DATA/lb721gf126f1267e2neh21w21gd13f31f13.php";
	private const string URL_LEADERBOARD_PAGE = "http://vailaball.me/Leaderboard";
	private const string CONSOLE_INITIAL = "  ~ Console: ";
	[SerializeField] private Text console;
	private string[] leaderboard = null;
    private bool isUserInLeaderboards = false;
    public GameObject canvasMainMenu;
    public GameObject usernameBox;
	public GameObject userInfoBox;
	public GameObject hr;

    private void Update() {
        if (usernameBox.transform.GetChild(0).gameObject.GetComponent<Text>().text.Length == 0)
            usernameBox.transform.GetChild(0).gameObject.GetComponent<Text>().text = UserAuthentication.user.getUsername();
    }

    public void seeMore() {
		Application.OpenURL(URL_LEADERBOARD_PAGE + "/?ID=" + UserAuthentication.user.getId());
    }

	public void handleLeaderboard() {
		canvasMainMenu.SetActive(false);
		StartCoroutine(getLeaderboard());
	}

	private IEnumerator getLeaderboard() {
		WWWForm form = new WWWForm();
        form.AddField("form_username", UserAuthentication.user.getUsername());
		form.AddField("form_hash", UserAuthentication.getHashCode());

		WWW www = new WWW(URL_GET_LEADERBOARD, form);
		console.text = CONSOLE_INITIAL + "Getting leaderboard data....";
		yield return www; // Wait www.

		if (www.error != null) {
			if (www.error.Contains("Network"))
				console.text = CONSOLE_INITIAL + "Please check your internet connection....";
			else
				console.text = CONSOLE_INITIAL + www.error;
		} else {
			console.text = CONSOLE_INITIAL + "Leaderboard is loaded.";
			leaderboard = www.text.Split('|');

			showLeaderboard();
		}
	}

	private void showLeaderboard() {
        for (int rank = 1, o = 0, y = -85; rank <= 10; rank++, o += 2, y -= 30)
	        cloneUserInfoBox(rank, o, y);

        if (!isUserInLeaderboards)
            printPersonalRank();
	}

    private void cloneUserInfoBox(int rank, int o, int y) {
		GameObject tmp = Instantiate(userInfoBox);
		tmp.transform.GetChild(0).gameObject.GetComponent<Text>().text = rank + ". " + leaderboard[o] + " has accumulated " + leaderboard[o + 1] + " points.";

        if (UserAuthentication.user.getUsername() == leaderboard[o]) {
			tmp.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red;
            isUserInLeaderboards = true;
        }
        
        customizeTransformComponentOf(tmp, y);
    }

    private void printPersonalRank () {
        string info = leaderboard[leaderboard.Length - 2] + ". " + UserAuthentication.user.getUsername() + " has accumulated " + leaderboard[leaderboard.Length - 1] + " points.";

        GameObject tmp = Instantiate(userInfoBox);
		tmp.transform.GetChild(0).gameObject.GetComponent<Text>().text = info;
        tmp.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red;
        
        customizeTransformComponentOf(tmp, -405);
        hr.SetActive(true);
    }

    private void customizeTransformComponentOf(GameObject obj, int y) {
        obj.transform.SetParent(transform.parent);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
		obj.transform.localScale = new Vector3(1, 1, 1);
    }

	public void back() {
		canvasMainMenu.SetActive(true);
		gameObject.transform.parent.gameObject.SetActive(false);
		
		console.text = CONSOLE_INITIAL + "Please do not forget to save your game!";
	}
}
