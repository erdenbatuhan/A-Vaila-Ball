//
//  BackgroundMusic.cs
//  A Vaila Ball - Mobile
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	
	public static float volume = 0.25f;
	
	private void Start() {
		if (Ball.getUsername() == null)
			GetComponent<AudioSource>().Play();
		else
			Destroy(gameObject);
	}
	
	private void Update() {
		GetComponent<AudioSource>().volume = volume;
	}
	
	private void Awake() {
		DontDestroyOnLoad(transform.gameObject);	
	}
}