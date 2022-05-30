using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[SelectionBase]
public class PlayerInfo : MonoBehaviour
{
	[SerializeField] GameObject SpawnOnDeath;

	public int HitCount;
	public int currentScore;

	private void FixedUpdate()
	{
		if (this.transform.position.y < -30) //we're outta bounds!
			ballDeath();
	}

	private void ballDeath()
	{
		//show the UI and delete self. spawn particle?
		GameObject UIManagerObj = GameObject.Find("UIManager");
		UIManager Manager = UIManagerObj.GetComponent<UIManager>();
		if (Manager != null)
			Manager.change_state(UIManager.UIStates.DEATH);


		if (SpawnOnDeath != null)
		{ 
			GameObject hehe = Instantiate(SpawnOnDeath);
			hehe.transform.position = this.transform.position;
		
		}
		this.transform.GetChild(0).gameObject.SetActive(false);
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

		Destroy(this);
	}

	public void BallLevelTransition()
	{
		this.transform.GetChild(0).gameObject.SetActive(false);
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

		GameObject UIManagerObj = GameObject.Find("UIManager");
		UIManager Manager = UIManagerObj.GetComponent<UIManager>();
		if (Manager != null)
			Manager.change_state(UIManager.UIStates.LEVELEND);

		//Destroy(this);
	}




}
