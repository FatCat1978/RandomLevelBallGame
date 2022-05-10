using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{


	public int HitCount;
	public int currentScore;

	[ReadOnly] public float currentEnergy;

	public Slider HealthBar;
	public Slider StaminaBar;

	public Text ScoreText;
	public Text HitCountText;

	void Start()
	{

	}

	private void FixedUpdate()
	{
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
