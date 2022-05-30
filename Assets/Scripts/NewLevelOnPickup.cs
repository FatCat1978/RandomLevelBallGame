using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelOnPickup : MonoBehaviour
{
	BallManager ballManager;

	public void OnTriggerEnter(Collider other)
	{
		//find the levelGen in the main level

		if (other.gameObject.name == "PlayerBall" || other.gameObject.name == "PlayerBall(Clone)") 
		{

			GameObject BallManager = GameObject.Find("BallManager");
			ballManager = BallManager.GetComponent<BallManager>();
		if (!ballManager)
			print("WHAT THE FUCK?");
		else
		{
			other.transform.position = new Vector3(0, 30, 0);
			PlayerInfo PL = other.GetComponent<PlayerInfo>();
				if (PL != null)
					PL.BallLevelTransition();


		}

		}

		else { return; }
	}




}
