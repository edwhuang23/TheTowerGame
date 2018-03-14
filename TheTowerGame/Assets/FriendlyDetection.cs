﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyDetection : MonoBehaviour {

	public bool gameWon = false;
	public float movementSpeed = 0.01f;
	GameObject[] opposingObjectPeople;
	GameObject[] opposingFriendlySmallTower;
	GameObject[] opposingFriendlyLargeTower;
	GameObject nearestenemy;
	GameObject nearestTower;
	public float distanceStartTargetingPlayer = 5f;
	float posx; 
	float posy;
	float posz;
	float targetx;
	float targety;
	float targetz;
	float towerx;
	public float meleeDamage = 0.1f;
	float towery;
	float towerz;
	float degrees;
	float shortestDistance;
	float towerShortestDistance;
	int recordSmallTower;
	int record;
	float full = 2;
	public float health = 1f;
	public Image healthbar;
	int target;

	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.tag == "friendlyweapon") {
			TakeDamage (c.gameObject.GetComponent<BulletBehavior>().damage);
		}


	}


	public void TakeDamage(float amount) {
		health -= amount;
		if (health <= 0) {
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}


	public GameObject getTarget() {


		if (target == 0) {
			return nearestenemy;
		} else if (target == 1) {
			return nearestTower;
		} else{
			return opposingFriendlyLargeTower [0];
		}
	}

	void Update () {
		healthbar.fillAmount = health;

		shortestDistance = 10000;
		towerShortestDistance = 10000;

		opposingObjectPeople = GameObject.FindGameObjectsWithTag("friendly");
		opposingFriendlySmallTower = GameObject.FindGameObjectsWithTag("smallFriendlyTower");
		opposingFriendlyLargeTower = GameObject.FindGameObjectsWithTag("largeFriendlyTower");

		posx = transform.position.x; 
		posy = transform.position.y;
		posz = transform.position.z;

		for (int i = 0; i < opposingObjectPeople.Length; i++) {
			targetx = opposingObjectPeople[i].transform.position.x; 
			targety = opposingObjectPeople[i].transform.position.y;
			targetz = opposingObjectPeople[i].transform.position.z;

			var distance = Mathf.Sqrt( Mathf.Pow(targetx - posx, 2) + Mathf.Pow(targety - posy,2) );

			if (distance < shortestDistance) {
				shortestDistance = distance;
				nearestenemy = opposingObjectPeople[i];
				record = i;
			}
		}

		for (int j = 0; j < opposingFriendlySmallTower.Length; j++) {
			towerx = opposingFriendlySmallTower[j].transform.position.x; 
			towery = opposingFriendlySmallTower[j].transform.position.y;
			towerz = opposingFriendlySmallTower[j].transform.position.z;

			var towerDistance = Mathf.Sqrt( Mathf.Pow(towerx - posx, 2) + Mathf.Pow(towery - posy,2) );

			if (towerDistance < towerShortestDistance) {
				towerShortestDistance = towerDistance;
				nearestTower = opposingFriendlySmallTower[j];
				recordSmallTower = j;
			}
		}

		if (shortestDistance < distanceStartTargetingPlayer && towerShortestDistance > shortestDistance ) {

			target = 0;

			if (opposingObjectPeople [record].transform.position.y - transform.position.y > 0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y + movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y < -0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y - movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y < -0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y - movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y > 0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y + movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y == 0 && opposingObjectPeople [record].transform.position.x - transform.position.x > 0) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y);
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y == 0 && opposingObjectPeople [record].transform.position.x - transform.position.x < 0) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y);
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y > 0 && opposingObjectPeople [record].transform.position.x - transform.position.x == 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + movementSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - movementSpeed);
			}

		} else if (opposingFriendlySmallTower != null) {

			target = 1;

			if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y > 0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y + movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y < -0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y - movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y < -0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y - movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y > 0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y + movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y == 0 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x > 0) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y);
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y == 0 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x < 0) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y);
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y > 0 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x == 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + movementSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - movementSpeed);
			}

		} else if(opposingFriendlyLargeTower != null) {

			target = 2;

			if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y > 0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y + movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y < -0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y - movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y < -0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y - movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y > 0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y + movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y == 0 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x > 0) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y);
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y == 0 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x < 0) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y);
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y > 0 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x == 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + movementSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - movementSpeed);
			} 
		} else {
			gameWon = true;
		}
	}
}