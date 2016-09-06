//
//  BackgroundMusic.cs
//  A Vaila Ball - Mobile
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	
	private void Start() {
		if (Ball.getUsername() == null)
			GetComponent<AudioSource>().Play();
		else
			Destroy(gameObject);
	}
	
	private void Awake() {
		DontDestroyOnLoad(transform.gameObject);	
	}
}
