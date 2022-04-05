using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int ScoreOnPickup;
    public GameObject spawnOnPickup;
	private bool hasBeenPicked = false;

	private void OnTriggerEnter(Collider other)
	{
		GameObject potentialPlayer = other.gameObject;
		PlayerInfo PlayersInfo = potentialPlayer.GetComponent<PlayerInfo>();
		if (PlayersInfo != null && !hasBeenPicked)
		{
			hasBeenPicked = true;
			PlayersInfo.currentScore += ScoreOnPickup;
			preDestroy();
		}


	}

	public void preDestroy()
	{
		Destroy(this.gameObject);
	}



}
