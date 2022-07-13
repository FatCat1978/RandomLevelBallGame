using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemPickup : MonoBehaviour
{
	[SerializeField] private int ScoreOnPickup; //how much we add to the score on pickup - this is shown when the object is destroyed.
	[SerializeField] private GameObject spawnOnPickup; //what particle effect, if any, is shown on the "pickup" phase. can techincally be any object!

	private bool hasBeenPicked = false; //have we been picked up? if we have, we can shrink and go after
	private GameObject followTarget; //our followtarget - this is almost always going to be the player. probably!

	[SerializeField] private float followSpeed = 12; //per second.

	[SerializeField] private float shrinkspeed = 2; //per second.

	public AudioClip pickupSound; //todo, debug this

	private Vector3 shrinkVector; //how much it shrinks per second. 

	//manages the killing of the object
	private bool doDestroy = false;
	private float secondsUntilDestroy = 3; 

	private void Start()
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource != null)
		{
			audioSource.clip = pickupSound;
		}

		shrinkVector = new Vector3(shrinkspeed, shrinkspeed, shrinkspeed);

	}
	private void OnTriggerEnter(Collider other)
	{
		GameObject potentialPlayer = other.gameObject;
		PlayerInfo PlayersInfo = potentialPlayer.GetComponent<PlayerInfo>();
		if (PlayersInfo != null && !hasBeenPicked)
		{
			hasBeenPicked = true;
			followTarget = potentialPlayer;
			
			PlayersInfo.currentScore += ScoreOnPickup;
			onItemPickup();
		}


	}

	public void Update()
	{
		if(doDestroy)
        {
			secondsUntilDestroy -= Time.deltaTime;
			if (secondsUntilDestroy < 0)
				Destroy(this.gameObject);
        }
		if(hasBeenPicked)
		{
			if (!followTarget)
				preDestroy();
			else
			{
				//move towards the follow target, and shrink down
				this.transform.localScale -= shrinkVector*Time.deltaTime;
				if (this.transform.localScale.x < 0)
				{
					this.transform.localScale = new Vector3(0, 0, 0);
				}

				float step = followSpeed * Time.deltaTime;
				this.transform.position = Vector3.MoveTowards(transform.position, followTarget.transform.position, step);


				if(Vector3.Distance(transform.position,followTarget.transform.position) < 0.001f || this.transform.localScale.x < 0.001f)
				{
					preDestroy();
				}

			}
		}
	}

	private void preDestroy()
	{
		
		QueueForDestroy(); //send it off to the grave
	}

	private void onItemPickup()
	{
		GetComponent<AudioSource>().Play(); //spawn particles, show score gain, etc.
		preDestroy();
	}

	public void QueueForDestroy()
	{
		doDestroy = true;
	}



}
