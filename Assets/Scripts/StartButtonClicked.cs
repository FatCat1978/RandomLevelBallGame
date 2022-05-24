using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonClicked : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject ball;

	public void StartGame()
	{
		GameObject newBall = Instantiate(ball);
		ball.transform.parent = null;
		ball.transform.position = new Vector3(0,50,0);

		Destroy(this.gameObject);
	}
}
