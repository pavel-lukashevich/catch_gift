using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGiftDie : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "gift") {
			other.tag = "crashedGift";
		}
	}
}
