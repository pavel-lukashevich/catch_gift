using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour {

	public Game game;
	
	void Start () {
		game = GameObject.Find("Game").GetComponent<Game>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "gift") {
			GameObject enteredObject = other.gameObject;
			Destroy(enteredObject);
			
			game.Counter();
		}
	}
}
