using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
	// Start is called before the first frame update
	public float maxHP;
	private float currentHP;
	public float maxEnergy;

	public int HitCount;
	public int currentScore;

	[ReadOnly] public float currentEnergy;

	public float energyRechargeRate; //how much energy is regenerated - per second!

	public float HPRechargeRate; //hp regenerated per second!

	internal void takeDamage(float damageDone)
	{
		currentHP -= damageDone; //todo, flash red or something??
		if (currentHP < 0)
			ballDeath();
	}

	public Slider HealthBar;
	public Slider StaminaBar;

	public Text ScoreText;
	public Text HitCountText;

	void Start()
	{
		currentHP = maxHP;
		currentEnergy = maxEnergy;
		if(HealthBar != null)
			HealthBar.maxValue = maxHP;
		if(StaminaBar != null)
			StaminaBar.maxValue = maxEnergy;

	}

	private void FixedUpdate()
	{
		currentHP += HPRechargeRate * Time.fixedDeltaTime;
		currentEnergy += energyRechargeRate * Time.fixedDeltaTime;

		currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);


		if(StaminaBar != null)
			StaminaBar.value = currentEnergy;
		if(HealthBar != null)
			HealthBar.value = currentHP;
		if(ScoreText != null) //move the update to this to an addscore method?
			ScoreText.text = "SCORE: " + currentScore.ToString();
		if(HitCountText != null)//ditto for hits.
			HitCountText.text = "HIT COUNT: " + HitCount.ToString();

		if (this.transform.position.y < -30) //we're outta bounds!
			ballDeath();
	}

	private void ballDeath()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void drainEnergy(float energy2drain)
	{
		currentEnergy -= energy2drain;
	}

}
