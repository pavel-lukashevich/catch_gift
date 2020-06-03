using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public GameObject zero;
	
	public GameObject startGameGUI;
	public bool isPlaying = false;
	
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	
	public int count;
	public int hp;
	public TextMesh counter;
	
	public GameObject hp1;
	public GameObject hp2;
	public GameObject hp3;
	
	[HideInInspector]
	public Transform trGifts;
	
	[HideInInspector]
	public Transform trGuests;
	
	public float speed = 1.0f;
	
	public AudioSource soundStep;
	public AudioSource soundCrash;
	public AudioSource soundBackground;
	
	
	void Start () {
		
		// trGifts = GameObject.Find("gifts").transform;
		// trGuests = GameObject.Find("guests").transform;
		
		// HidePlayer();
		// player1.SetActive(true);
		
		// hp1.SetActive(false);
		// hp2.SetActive(false);
		// hp3.SetActive(false);
		
		// StartCoroutine(Timer());
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			HidePlayer();
			player1.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.LeftControl)) {
			HidePlayer();
			player2.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.RightShift)) {
			HidePlayer();
			player3.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.RightControl)) {
			HidePlayer();
			player4.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			if (isPlaying) {
				StopGame();
			} else {
				StartGame();
			}
		}
	}
	
	void HidePlayer () {
		player1.SetActive(false);
		player2.SetActive(false);
		player3.SetActive(false);
		player4.SetActive(false);
	}
	
	IEnumerator Timer() {
		GameObject guestAndGift = Instantiate(zero);
		Gift component = guestAndGift.AddComponent<Gift>();
		int giftId = Random.Range(0, trGifts.childCount);
		int guestId = Random.Range(0, trGuests.childCount);
		
		component.gift = Instantiate(trGifts.GetChild(giftId).gameObject);
		component.guest = Instantiate(trGuests.GetChild(guestId).gameObject);
		
		component.startPosition = Random.Range(0, 4);
		component.game = this.GetComponent<Game>();
		
		
		float time = 1 / speed;
		yield return new WaitForSeconds(Random.Range(time * 0.9f, time * 1.1f));
		StartCoroutine(Timer());
	}
	
	void Step () {
		soundStep.Play();
	}
		
	public void Counter () {
		count++;
		if (count % 10 == 0) {
			speed += 0.1f;
		}
		counter.text = count.ToString();
	}
		
	public void Crash () {
		soundCrash.Play();
		hp++;
		if (hp >= 1) hp1.SetActive(true);
		if (hp >= 2) hp2.SetActive(true);
		if (hp >= 3) hp3.SetActive(true);
		if (hp > 3) StopGame();
	}
	
	void StartGame() {
		trGifts = GameObject.Find("gifts").transform;
		trGuests = GameObject.Find("guests").transform;
		
		count = 0;
		hp = 0;
		isPlaying = true;
		counter.text = count.ToString();
		
		HidePlayer();
		player1.SetActive(true);
		
		hp1.SetActive(false);
		hp2.SetActive(false);
		hp3.SetActive(false);
		
		StartCoroutine(Timer());
		
		soundBackground.Play();
		startGameGUI.SetActive(false);
		
	}
	
	void StopGame () {
		isPlaying = false;
		soundBackground.Stop();
		// startGameGUI.SetActive(true);
		// TextMesh score = GameObject.Find("score").GetComponent<TextMesh>();
		// if (count != 0) {
			// score.text = count.ToString();
		// } else {
			// score.text = "";
		// }
		
		StopAllCoroutines();
		
		GameObject[] gifts = GameObject.FindGameObjectsWithTag("gift");
		foreach (GameObject obj in gifts) {
			if (obj.name.EndsWith("(Clone)")) {
				Destroy(obj);
			}
		}
		
		GameObject[] crashedGifts = GameObject.FindGameObjectsWithTag("crashedGift");
		foreach (GameObject obj in crashedGifts) {
			if (obj.name.EndsWith("(Clone)")) {
				Destroy(obj);
			}
		}
		
		GameObject[] guests = GameObject.FindGameObjectsWithTag("guest");
		foreach (GameObject obj in guests) {
			if (obj.name.EndsWith("(Clone)")) {
				Destroy(obj);
			}
		}
		StartCoroutine(startGUI());
	}
	
	IEnumerator startGUI() {
		yield return new WaitForSeconds(1f);
		startGameGUI.SetActive(true);
		
		TextMesh score = GameObject.Find("score").GetComponent<TextMesh>();
		if (count != 0) {
			score.text = count.ToString();
		} else {
			score.text = "";
		}
	}
}
