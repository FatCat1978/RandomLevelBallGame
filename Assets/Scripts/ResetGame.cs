using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    // Start is called before the first frame update
public void reset_game()
	{
		GameObject SaveManagerObj = GameObject.Find("SaveManager");
		PlayerPrefsManager PPM = SaveManagerObj.GetComponent<PlayerPrefsManager>();

		PPM.PlayerDeath();

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}
}
