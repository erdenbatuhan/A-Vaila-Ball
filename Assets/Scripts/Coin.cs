//
//  Coin.cs
//  A Vaila Ball - Mobile
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

	/* ------ DROP CHANCES ------ *\
	 * Normal Coin: 79%
	 *    Red Coin: 13%
	 *  Green Coin: 5%
	 * Purple Coin: 2%
	 *  Spell Coin: 1%
	\* -------------------------- */

	private const string CONSOLE_INITIAL = "  ~ Console: You've collected ";
	[SerializeField] private Text console;
	public Ball ball;
	public GameObject coinEffect, point;
	public bool spell, purple, green, red;

	private void OnTriggerEnter() {
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;

		collect();
		animate();
	}

	private void collect() {
		if (spell) { // --------------------------> Spell Coin (Immunity)
			console.text = CONSOLE_INITIAL + "immunity coin (Value: +5). Now you are immune!";
			instantiatePointText("+5", true);
			ball.setScore(ball.getScore() + 5);
			ball.setSpellActivated(true);
			ball.changeShader();
		} else if (purple) { // ------------------> Purple Coin
			console.text = CONSOLE_INITIAL + "purple coin (Value: +10). Your scale is back to normal.";
			instantiatePointText("+10", true);
			ball.setScore(ball.getScore() + 10);
			ball.resetScale();
		} else if (green) { // -------------------> Green Coin
			console.text = CONSOLE_INITIAL + "green coin (Value: +20).";
			instantiatePointText("+20", true);
			ball.setScore(ball.getScore() + 20);
		} else if (red) { // ---------------------> Red Coin
			console.text = CONSOLE_INITIAL + "red coin (Value: -50%). Your scale got bigger about 10%.";
			instantiatePointText("-" + ball.getScore() / 2, false);
			ball.setScore(ball.getScore() - ball.getScore() / 2);
			ball.enlargeScale();
		} else { // ------------------------------> Yellow Coin
			console.text = CONSOLE_INITIAL + "normal coin (Value: +1).";
			instantiatePointText("+1", true);
			ball.setScore(ball.getScore() + 1);
		}
	}

	private void instantiatePointText(string text, bool friendly) {
		point.GetComponent<TextMesh>().text = text;

		if (!friendly)
			point.GetComponent<TextMesh>().color = Color.red;
		else
			point.GetComponent<TextMesh>().color = Color.white;

		point = Instantiate(point);
		point.transform.position = new Vector2(transform.position.x + 2.5f, transform.position.y + 1);

		Destroy(point, 3);
	}
	
	private void animate() {
		GameObject effect = Instantiate(coinEffect);	
		effect.transform.position = transform.position;
		GetComponent<AudioSource>().Play();

		destroyChildren();
		Destroy(effect, 3);
		Destroy(gameObject, 3);
	}
	
	private void destroyChildren() {
		foreach (Transform child in transform)
			Destroy(child.gameObject);
	}
}