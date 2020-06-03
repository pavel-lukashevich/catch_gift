using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour {

	public Game game;
	public GameObject gift;
	public GameObject guest;
	public int startPosition;
	
	private float startTime;
	public Vector3 startMarkerGuest;
    public Vector3 endMarkerGuest;
	
	public Vector3 startMarkerGift;
    public Vector3 endMarkerGift;
	
	private float journeyLength;
	
	// {guest.x, guest.y, gift.x, gift.y, scale.x}
	public float[ , ] guestPosition = {
		{ -10.7f, 3.7f, -10.0f, 3.1f, 1f },
		{ -10.7f, 0.8f, -10.0f, 0.2f, 1f },
		{ 10.7f, 3.7f, 10.0f, 3.1f, -1f },
		{ 10.7f, 0.8f, 10.0f, 0.2f, -1f },
	};
	
	
	void Start () {
		// start guest
		Vector3 guestPosition = guest.transform.position;
		guestPosition.x = this.guestPosition[startPosition, 0];
		guestPosition.y = this.guestPosition[startPosition, 1];
		
		Vector3 guestScale = guest.transform.localScale;
		guestScale.x = this.guestPosition[startPosition, 4];
		
		guest.transform.position = guestPosition;
		guest.transform.localScale = guestScale;
				
		// guest markers
		startMarkerGuest = guest.transform.position;
		endMarkerGuest = guest.transform.position;
		endMarkerGuest.x = endMarkerGuest.x + this.guestPosition[this.startPosition, 4] * 3.0f;
		
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarkerGuest, endMarkerGuest);
		
		// start gift
		Vector3 giftPosition = gift.transform.position;		
		giftPosition.x = this.guestPosition[startPosition, 2];
		giftPosition.y = this.guestPosition[startPosition, 3];
				
		guest.transform.position = giftPosition;
		
		// gift markers
		startMarkerGift = guest.transform.position;
		endMarkerGift = guest.transform.position;
		endMarkerGift.x = endMarkerGift.x + this.guestPosition[this.startPosition, 4] * 3.0f;
		
	}
	
	void Update () {
		if (gift == null) {
			Destroy(this.gameObject);
		} else {
			Rigidbody2D giftRb = gift.GetComponent<Rigidbody2D>();
			
			if (guest != null) {
				if (Math.Abs(guest.transform.position.x) > Math.Abs(endMarkerGuest.x)) {
				float distCovered = (Time.time - startTime) * game.speed;

				float fractionOfJourney = distCovered / journeyLength;

				guest.transform.position = Vector3.Lerp(startMarkerGuest, endMarkerGuest, fractionOfJourney);
				gift.transform.position = Vector3.Lerp(startMarkerGift, endMarkerGift, fractionOfJourney);
				} else {
					Destroy(guest);
					
					if (giftRb.gravityScale == 0) {
						giftRb.gravityScale = game.speed;
					}
				}
			}
		}	
	}
}
