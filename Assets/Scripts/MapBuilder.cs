//
//  MapBuilder.cs
//  Flappy Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections.Generic;

public class MapBuilder : MonoBehaviour {

	/* ------------------------------------ QUEUE SYSTEM IN THE GAME ------------------------------------ *\
	 * There will always be 9 objects in total in the game.
	 * These objects are enqueued to the queue once the game is started.
	 * As soon as the player passes the 6th object,
	 * the code automatically dequeues the first 3 objects from the queue (6 left),
	 * and enqueues 3 new objects to the queue (9 objects again).
	 * This provides a more efficient game because the game will always consist of 9 objects in total.
	\* -------------------------------------------------------------------------------------------------- */
	
	private const float DIFF_X = 10; // 'x' difference between cubes
	private const float DIFF_Y = 8;  // 'y' difference between cubes
	private static float x, y;
	private static float minY, maxY;
	private static Queue<GameObject> queue;
	private static long gateCounter = -3;
	public GameObject cube, normalCoin, redCoin, greenCoin, purpleCoin, spellCoin, gate;

	private void Start() {
		x = transform.position.x;
		y = transform.position.y;
		minY = y;
		maxY = y;

		queue = new Queue<GameObject>();
		create(9); // Create 9 Objects and Enqueue them
	}

	private void Update() {
		if (gateCounter == 3) {
			for (int i = 0; i < 12; i++) { // Dequeue 3 Objects [An object has 4 components, 3 * 4 = 12] and delete them
				Destroy(queue.Dequeue());
			}

			create(3); // Create 3 Objects and Enqueue them
			gateCounter = 0;
		}
	}

	private void create(int size) {
		for (int j = 0; j < size; j++) {
			x += DIFF_X; // Move to the next location
			
			int random = Random.Range(1, 100);
			buildObject(random);
		}
	}

	private void buildObject(int random) {
		ObjectBuilder obj = new ObjectBuilder(cube, normalCoin, gate); // -----> Normal Coin, 79%.. {1, 2, ..... 78, 79}

		if (random > 98) // ---------------------------------------------------> Purple Coin, 2%.. {99, 100}
			obj = new ObjectBuilder(cube, purpleCoin, gate);
		else if (random > 93) // ----------------------------------------------> Green Coin, 5%.. {94, 95, 96, 97, 98}
			obj = new ObjectBuilder(cube, greenCoin, gate);
		else if (random > 80) // ----------------------------------------------> Red Coin, 10%.. {81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93}
			obj = new ObjectBuilder(cube, redCoin, gate);
		else if (random == 80) // ---------------------------------------------> Immune Spell, 1%.. {80}
			obj = new ObjectBuilder(cube, spellCoin, gate);

		obj.insert();
	}

	private class ObjectBuilder {
	
		private GameObject cube, coin, gate;
		
		public ObjectBuilder(GameObject cube, GameObject coin, GameObject gate) {
			this.cube = cube;
			this.coin = coin;
			this.gate = gate;
		}
		
		public void insert() {
			y = Random.Range(y - (DIFF_Y / 2), y + (DIFF_Y / 2));

			if (y < minY)
				minY = y;
			else if (y > maxY)
				maxY = y;

			instantiateGameObjects();
		}

		private void instantiateGameObjects() {
			/* -------------- Instantiate the Lower Cube --------------------- */
			instantiateGameObject(cube);
			y += DIFF_Y;
			/* -------------- Instantiate the Coin and the Gate -------------- */
			instantiateGameObject(coin);
			instantiateGameObject(gate);
			y += DIFF_Y;
			/* -------------- Instantiate the Upper Cube --------------------- */
			instantiateGameObject(cube);
			y -= (DIFF_Y * 2);
		}

		private void instantiateGameObject(GameObject obj) {
			Vector3 position = new Vector3(x, y, 0);
			GameObject clone = Instantiate(obj);
			clone.transform.position = position;

			queue.Enqueue(clone);
		}
	}

	/* ---------------------------- GETTERS & SETTERS ---------------------------- */

	public static float getMinY() {
		return minY;
	}

	public static float getMaxY() {
		return maxY;
	}

	public static Queue<GameObject> getQueue() {
		return queue;
	}

	public static long getGateCounter() {
		return gateCounter;
	}

	public static void setGateCounter(long gateCounter) {
		MapBuilder.gateCounter = gateCounter;
	}
}