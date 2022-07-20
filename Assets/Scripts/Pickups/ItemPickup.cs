using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemPickup : MonoBehaviour
{
	[SerializeField] private int ScoreOnPickup; //how much we add to the score on pickup - this is shown when the object is destroyed.
	[SerializeField] private GameObject spawnOnPickup; //what particle effect, if any, is shown on the "pickup" phase. can techincally be any object!
	[SerializeField] private float spawnOnPickupYOffset = 1f;

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


	//when we collide with a trigger
	private void OnTriggerEnter(Collider other)
	{
		GameObject potentialPlayer = other.gameObject; //get the game object owning the trigger
		PlayerInfo PlayersInfo = potentialPlayer.GetComponent<PlayerInfo>(); //see if it's a player object before continuing
		
		
		if (PlayersInfo != null && !hasBeenPicked) //if there's a player and we haven't already been picked up
		{
			hasBeenPicked = true; //we're now picked up
			followTarget = potentialPlayer; //follow the player
			
			PlayersInfo.currentScore += ScoreOnPickup; //add score
			onItemPickup(); //and send off the rest.
		}


	}

	public void Update() //every frame.
	{
		if(doDestroy) //if we're queued for destroy, we tick off some time
        {
			secondsUntilDestroy -= Time.deltaTime; 
			if (secondsUntilDestroy < 0) //if no more time's left...
				Destroy(this.gameObject);//we're gone.
			
        }

		if(hasBeenPicked) //if we're picked up
		{
			if (!followTarget) //and we don't have a follow target
				preDestroy(); //we queue the pre-destroy stuff - no point trying to follow/shrink if there's nothing to follow. maybe still shrink?
			else
			{
				//move towards the follow target, and shrink down
				this.transform.localScale -= shrinkVector*Time.deltaTime; //static shrink var
				if (this.transform.localScale.x < 0) //can't go under 0 scale
				{
					this.transform.localScale = new Vector3(0, 0, 0);
				}

				float step = followSpeed * Time.deltaTime; //we step towards the target. static speed, so it's possible for the ball to get away, if that's the case we let 'em and despawn after a set time
				this.transform.position = Vector3.MoveTowards(transform.position, followTarget.transform.position, step);


				if(Vector3.Distance(transform.position,followTarget.transform.position) < 0.001f || this.transform.localScale.x < 0.001f)
				{ //if we're close or tiny, we're nuking ourselves.
					preDestroy();
				}

			}
		}
	}

	private void preDestroy()
	{
		
		QueueForDestroy(); //send it off to the grave. we do this to let the sound play.
		//TODO, generic sound player object system?
	}

	private void onItemPickup()
	{
		GetComponent<AudioSource>().Play(); //spawn particles, show score gain, etc.
		preDestroy();
		//spawn the prefab
		if(spawnOnPickup != null)
        {
			GameObject tempPickupStorage = Instantiate(spawnOnPickup);
			tempPickupStorage.transform.SetParent(null);
			tempPickupStorage.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z); //it needs a higher Y value, so it looks like it's above the object. prevents clipping too.
			
			textshrinkout potentialText = tempPickupStorage.GetComponent<textshrinkout>();
			if(potentialText != null) //set the name.
            {
				potentialText.changeText("+" + ScoreOnPickup);
				potentialText.isShrinking = true;
            }
        }
	}

	public void QueueForDestroy()
	{
		doDestroy = true;
	}



}
