using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject ball;
    public void weAreClicked()
	{
		//summon ball
		print("gay retard");
	}
	private void Update()
	{
		if(Input.anyKeyDown)
		{
			StartGame();
		}
	}

	public void StartGame()
	{
		GameObject newBall = Instantiate(ball);
		ball.transform.parent = null;
		ball.transform.position = new Vector3(0,50,0);

		Destroy(this.gameObject);
	}
}
