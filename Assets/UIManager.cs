using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public enum UIStates
	{
		INTRO,
		DEATH,
		LEVELEND
	}
	[SerializeField] Sprite[] possibleSprites;
	public GameObject UI_INTRO;
	public GameObject UI_DEATH;
	public GameObject UI_LEVELEND;


	private UIStates state = UIStates.INTRO;

	public void change_state(UIStates newstate)
	{
		switch(newstate)
		{
			case UIStates.INTRO:
				{
					UI_DEATH.SetActive(false);
					UI_LEVELEND.SetActive(false);
					UI_INTRO.SetActive(true);
					break;
				}
			case UIStates.DEATH:
				{
					UI_DEATH.SetActive(false);
					UI_LEVELEND.SetActive(false);
					UI_INTRO.SetActive(true);
					break;
				}
			case UIStates.LEVELEND:
				{
					UI_DEATH.SetActive(false);
					UI_LEVELEND.SetActive(false);
					UI_INTRO.SetActive(true);
					break;
				}


		}
	}



	// Start is called before the first frame update

}
