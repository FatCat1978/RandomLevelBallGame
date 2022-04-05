using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelOnPickup : MonoBehaviour
{

	public void OnTriggerEnter(Collider other)
	{
		//find the levelGen in the main level

		if (other.gameObject.name == "PlayerBall" || other.gameObject.name == "PlayerBall(Clone)") 
		{ 

		GameObject currentLevelGen = GameObject.Find("LevelGenerator");
		if (!currentLevelGen)
			print("WHAT THE FUCK?");
		else
		{
			currentLevelGen.GetComponent<LevelGenerator>().RegenerateLevel();
			other.transform.position = new Vector3(0, 30, 0);
			

		}

		}

		else { return; }
	}




}
