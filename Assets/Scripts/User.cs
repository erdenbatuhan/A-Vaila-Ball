//
//  User.cs
//  A Vaila Ball
//
//  Created by Batuhan Erden.
//  Copyright Â© 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class User {

    private int id;
    private string username;
    private int highScore;

	public User(int id, string username, int highScore) {
	    this.id = id;
        this.username = username;
        this.highScore = highScore;
	}

	/* ---------------------------- GETTERS & SETTERS ---------------------------- */

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public string getUsername() {
        return username;
    }

    public void setUsername(string username) {
        this.username = username;
    }

    public int getHighScore() {
        return highScore;
    }

    public void setHighScore(int highScore) {
        this.highScore = highScore;
    }
}
