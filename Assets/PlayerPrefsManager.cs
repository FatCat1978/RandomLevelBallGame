using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // Start is called before the first frame update



    //These are meant to be accessed by other shit throughout the game, so hey, public.

    public int floorsStreakMax = 0; //the "Win Streak"
    public int currentFloorStreak = 0;

    public int scoreThisFloor = 0;
    public int totalScore = 0;
    public int highScore = 0;




public void SaveAll()
	{
        PlayerPrefs.SetInt("floorsStreakMax", floorsStreakMax);
        PlayerPrefs.SetInt("currentFloorStreak", currentFloorStreak);

        PlayerPrefs.SetInt("scoreThisFloor", scoreThisFloor);
        PlayerPrefs.SetInt("totalScore", totalScore);
        PlayerPrefs.SetInt("highScore", highScore);
	}

public void LoadAll()
	{
        floorsStreakMax = PlayerPrefs.GetInt("floorsStreakMax");
        currentFloorStreak = PlayerPrefs.GetInt("currentFloorStreak");

        scoreThisFloor = PlayerPrefs.GetInt("scoreThisFloor");
        totalScore = PlayerPrefs.GetInt("totalScore");
        highScore = PlayerPrefs.GetInt("highScore");
	}

    // Update is called once per frame
    void Start()
    {
        LoadAll();
    }

    public void PlayerDeath() //when we die, we need to update the max streak.
	{
        if (currentFloorStreak > floorsStreakMax)
            floorsStreakMax = currentFloorStreak;
        currentFloorStreak = 0;
        if (totalScore > highScore)
            highScore = totalScore;
        totalScore = 0;


    //and then we can reset the level.
    


	}

    public void OnLevelEnd(GameObject player)
	{ //add this level's score to the total
        Debug.Log("Storing level data due to the player being dead.");
        if (player == null)
            player = GameObject.Find("PlayerBall(Clone)"); //horrible.
        PlayerInfo PI = player.GetComponent<PlayerInfo>();  

        currentFloorStreak += 1;
        if(PI != null)
            totalScore += PI.currentScore;

        SaveAll();

        Destroy(player);

	}


}
