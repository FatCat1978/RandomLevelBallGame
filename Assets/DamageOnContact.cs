using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
	public float DamageDone;

	public float DamageCooldown;

	public float LastDamageTick;


	private void OnTriggerEnter(Collider other)
	{
		GameObject potentialPlayer = other.gameObject;
		PlayerInfo PlayersInfo = potentialPlayer.GetComponent<PlayerInfo>();
		if (PlayersInfo != null && ((long)Time.time > LastDamageTick+DamageCooldown))
		{
			LastDamageTick = Time.time;

			PlayersInfo.takeDamage(DamageDone);
			
		}
	}


}
