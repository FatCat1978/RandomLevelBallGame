using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	[SerializeField] GameObject ballTemplate;
	LevelGenerator LevelGeneratorinst;
	[SerializeField] UIManager UIManagerinst;

	private GameObject currentBall; //there can only be one of these at a time.

	private void Start()
	{
		GameObject LevelGenerator = GameObject.Find("LevelGenerator");
		LevelGeneratorinst = LevelGenerator.GetComponent<LevelGenerator>();
	}
	public void NextLevelHandler()
	{
		GameObject PPMO = GameObject.Find("SaveManager");
		PlayerPrefsManager PPM = PPMO.GetComponent<PlayerPrefsManager>();

		PPM.OnLevelEnd(currentBall);

		UIManagerinst.change_state(UIManager.UIStates.GAMEPLAY);
		LevelGeneratorinst.RegenerateLevel();
		spawnBall();
	}

	public void RepeatCurrentLevelHandler()
	{
		LevelGeneratorinst.RegenerateLevel();
		spawnBall(true);
	}

	public void StartLevelHandler() //handles initially starting the game.
	{
		spawnBall();
	}

	private GameObject spawnBall(bool changeUI = false)
	{
		if(currentBall != null)
			Destroy(currentBall);
		GameObject newBall = Instantiate(ballTemplate) as GameObject;
		newBall.transform.position = new Vector3(0,50,0);
		currentBall = newBall;
		if (changeUI)
			UIManagerinst.change_state(UIManager.UIStates.GAMEPLAY);
		return newBall;
		
	}



}
