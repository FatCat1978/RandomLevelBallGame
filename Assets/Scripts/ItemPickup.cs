using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemPickup : MonoBehaviour
{
    public int ScoreOnPickup;
    public GameObject spawnOnPickup;
	private bool hasBeenPicked = false;

	public AudioClip pickupSound;

	private void Start()
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource != null)
		{
			audioSource.clip = pickupSound;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		GameObject potentialPlayer = other.gameObject;
		PlayerInfo PlayersInfo = potentialPlayer.GetComponent<PlayerInfo>();
		if (PlayersInfo != null && !hasBeenPicked)
		{
			hasBeenPicked = true;
			GetComponent<AudioSource>().Play();	
			PlayersInfo.currentScore += ScoreOnPickup;
			preDestroy();
		}


	}

	public void preDestroy()
	{
		Destroy(this.gameObject);
	}



}
