//
//  OnTriggerScript.cs
//  A Vaila Ball - Mobile
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;

public class OnTriggerScript : MonoBehaviour {
	
	private const string CONSOLE_INITIAL = "  ~ Console: ";
	[SerializeField] private Text console;
	public Ball ball;
	public bool ground, gate;

	private void OnTriggerEnter() {
		if (ground) {
			if (ball.isSpellActivated()) {
				// Pass through the cube. Then, de-activate the spell.
				console.text = CONSOLE_INITIAL + "You've lost your immunity. You are not immune anymore.";
				ball.setSpellActivated(false);
				ball.changeShader();
			} else {
				MapBuilder.setGateCounter(-3);
				MapBuilder.getQueue().Clear();

				Application.LoadLevel(1);
			}
		} else if (gate) {
			// Gate is being passed.
			MapBuilder.setGateCounter(MapBuilder.getGateCounter() + 1);
		}

		ball.changeShader();
	}
}
