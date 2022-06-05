using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonClicked : MonoBehaviour
{
	// Start is called before the first frame update

	public void StartGame()
	{
		BallManager ballManager = GameObject.Find("BallManager").GetComponent<BallManager>();
		if (ballManager != null)
			ballManager.StartLevelHandler();
		GameObject UImanagerObj = GameObject.Find("UIManager");
		UIManager uIManager = UImanagerObj.GetComponent<UIManager>();
		uIManager.change_state(UIManager.UIStates.GAMEPLAY);

	}
}
