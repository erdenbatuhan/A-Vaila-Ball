//
//  Ball.cs
//  A Vaila Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	
	private const int SPEED_FORCE = 8;
	private const int JUMP_HEIGHT = 8;
	private int score = 0;
	private bool spellActivated = false;
	private Material material;
	[SerializeField] private Text usernameText;
	[SerializeField] private Text scoreText;
	public GameObject jumpEffect;
	public GameObject camera, upperCatcher, lowerCatcher;
	public Material immunityMaterial;

	private void Start() {
        material = GetComponent<Renderer>().material;
	}                    

	private void Update() {
		if (Input.GetMouseButtonDown(0))
			jump();

		move();
		getTracked();

		if (score >= UserAuthentication.user.getHighScore())
			UserAuthentication.user.setHighScore(score);
		
		usernameText.text = UserAuthentication.user.getUsername();
		scoreText.text = "Score: " + score + "/" + UserAuthentication.user.getHighScore();
	}

	private void move() {
		/* -------------- Rigidbody is Moving  ---------------- */
		GetComponent<Rigidbody>().velocity = new Vector3(SPEED_FORCE, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
		/* -------------- Rigidbody is Rotating  -------------- */
		float rotation = 9999;
		rotation *= Time.deltaTime;
		GetComponent<Rigidbody>().AddRelativeTorque(Vector3.back * rotation);
	}

	private void jump() {
		/* -------------- Rigidbody is Jumping ---------------------- */
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, JUMP_HEIGHT, GetComponent<Rigidbody>().velocity.z);
		/* -------------- AudioSource is being Played --------------------- */
		GetComponent<AudioSource>().Play();
		/* -------------- Effect is being Instantiated -------------- */
		GameObject effect = Instantiate(jumpEffect);
		effect.transform.position = this.transform.position;
		Destroy(effect, 3);
	}

	private void getTracked() {
		camera.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10);
		upperCatcher.transform.position = new Vector3(transform.position.x, MapBuilder.getMaxY() + 22, 0);
		lowerCatcher.transform.position = new Vector3(transform.position.x, MapBuilder.getMinY() - 6, 0);
	}

	public void resetScale() {
		this.transform.localScale = new Vector3(1, 1, 1);
	}

	public void enlargeScale() {
		float scale = this.transform.localScale.x;
		this.transform.localScale = new Vector3(scale + 0.1f, scale + 0.1f, scale + 0.1f);
	}

	public void changeShader() {
		if (spellActivated)
			GetComponent<Renderer>().material = immunityMaterial;
		else
			GetComponent<Renderer>().material = material;
	}

	public void returnToMenu() {
		MapBuilder.setGateCounter(-3);
		MapBuilder.getQueue().Clear();

		Application.LoadLevel(0);
	} 

	/* ---------------------------- GETTERS & SETTERS ---------------------------- */
	
	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public bool isSpellActivated() {
		return spellActivated;
	}

	public void setSpellActivated(bool spellActivated) {
		this.spellActivated = spellActivated;
	}
}
