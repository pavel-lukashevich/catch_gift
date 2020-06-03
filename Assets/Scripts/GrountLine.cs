using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrountLine : MonoBehaviour {

	public Game game;
	public GameObject boom;
	
	void Start () {
		boom = Instantiate(GameObject.Find("boom"));
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "gift" || other.tag == "crashedGift") {
			GameObject enteredObject = other.gameObject;
			Vector3 boomPosition = enteredObject.transform.position;
			
			boom.transform.position = boomPosition;
			if (game == null) {
				game = GameObject.Find("Game").GetComponent<Game>();
			}
			game.Crash();
			Destroy(enteredObject);
			StartCoroutine(Boom());
		}
	}
	
	IEnumerator Boom() {
		yield return new WaitForSeconds(1.0f);
		Destroy(boom);
		boom = Instantiate(GameObject.Find("boom"));
	}
}
