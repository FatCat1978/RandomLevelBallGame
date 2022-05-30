using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public enum UIStates
	{
		INTRO,
		DEATH,
		LEVELEND,
		GAMEPLAY
	}

	public GameObject UI_INTRO;
	public GameObject UI_DEATH;
	public GameObject UI_LEVELEND;
	public GameObject UI_GAMEPLAY;


	private UIStates state = UIStates.INTRO;

	public void change_state(UIStates newstate)
	{
		Debug.Log("Setting UI state to:" + newstate);
		switch(newstate)
		{
			case UIStates.INTRO:
				{
					UI_DEATH.SetActive(false);
					UI_GAMEPLAY.SetActive(false);
					UI_LEVELEND.SetActive(false);
					UI_INTRO.SetActive(true);
					break;
				}
			case UIStates.DEATH:
				{
					UI_DEATH.SetActive(true);
					UI_GAMEPLAY.SetActive(false);
					UI_LEVELEND.SetActive(false);
					UI_INTRO.SetActive(false);
					break;
				}
			case UIStates.LEVELEND:
				{
					UI_DEATH.SetActive(false);
					UI_GAMEPLAY.SetActive(false);
					UI_LEVELEND.SetActive(true);
					UI_INTRO.SetActive(false);

					break;
				}
			case UIStates.GAMEPLAY:
				{
					UI_DEATH.SetActive(false) ;
					UI_LEVELEND.SetActive(false) ;
					UI_INTRO.SetActive(false) ;
					UI_GAMEPLAY.SetActive(true) ;
					
					break;
				}


		}
	}



	// Start is called before the first frame update

}
