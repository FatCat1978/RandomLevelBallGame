using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
	[Header("Camera Settings")]
	public float TouchRayCastDist = 30;
	public float CamInputScaler = 1;


	[Header("Ball Settings")]
	public GameObject throwTowards; //what are we getting thrown towards?
	public float maxThrowDistance = 5; //how far away can the mystical point be?
	
	public float throwScaler = 1;
	public float exponentialScaler = 1;
	//public bool TurningEnabled = true;


	public GameObject BallHitReference; //don't edit this!!!

	private bool isDragging = false; 

	private CameraFollower CameraCtrl;
	private LineRendererArrow ArrowDraw;

	private Rigidbody ballBody;

	private PlayerInfo playerInfo;

	public AnimationCurve throwingCurve;


	public void Start()
	{
		playerInfo = this.gameObject.GetComponent<PlayerInfo>();
		CameraCtrl = this.gameObject.GetComponent<CameraFollower>();
		ArrowDraw = this.gameObject.GetComponent<LineRendererArrow>();
		ballBody = this.gameObject.GetComponent<Rigidbody>();
	}

	private void Update()
	{//TODO - convert this! It should use the modern input system.
		if(IsClicking())
		{
			if(!isDragging) //se if we're tapping the car or not
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, TouchRayCastDist)) 
				{
					if (hit.transform == this.transform)
					{
						isDragging = true;
					}
				}
				if(!isDragging) //we are still not dragging the ball. thus, camera control.
				{

					float AngleValueChange = Input.GetAxis("Mouse X") * CamInputScaler;
					CameraCtrl.CameraAngle += AngleValueChange;

				}
				//if we're NOT, we are rotating the camera
			}
			else//we're actively dragging
			{
				if(throwTowards != null)
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit raycastHit = new RaycastHit();
					if(Physics.Raycast(ray, out raycastHit, TouchRayCastDist))
					{ 
					Vector3 newPt = raycastHit.point;
					newPt.y = this.transform.position.y;
					throwTowards.transform.position = newPt;
					}
			
					else
					{
					Vector3 newPt = ray.origin + ray.direction * Vector3.Distance(this.transform.position,Camera.main.transform.position);
					newPt.y = this.transform.position.y;
					throwTowards.transform.position = newPt;
					}


					ArrowDraw.ArrowOrigin = this.gameObject.transform.position;
					ArrowDraw.ArrowTarget = throwTowards.transform.position;
					ArrowDraw.UpdateArrow();

					//float Distance = Vector3.Distance(throwTowards.transform.position, this.transform.position);
					//Debug.Log("DISTANCE: " + Distance);


					//throwTowards.transform.position;


				}
				
			}
		}
		else
		{
			//no longer clicking.
			if(isDragging)
			{

				isDragging=false;
				ArrowDraw.Reset();

				Vector3 throwDir = throwTowards.transform.position - this.transform.position;
				float Distance = Vector3.Distance(throwTowards.transform.position, this.transform.position);
				if (Distance > maxThrowDistance)
					Distance = maxThrowDistance;


				throwDir.Normalize();
				float Scaler = (Distance*Distance)*throwScaler;


				throwDir = throwDir*Scaler;


				ballBody.AddForce(throwDir);

				//TODO, change pitch to relate to how "hard" the ball was hit. barely any distance? high pitch. low pitch for high distances etc. make it feel meaty.
				AudioSource AS = GetComponent<AudioSource>();
				AS.pitch = Random.Range(.5f, 1.5f);
				AS.Play();
				playerInfo.HitCount++;


				//throw ball here!
			}
		}

	}


	public bool IsClicking() //counts for tapping and dragging, funilly enough
	{
		bool isTouchOrClick = false;
		foreach (Touch touch in Input.touches)
		{
			isTouchOrClick = true;
		}

		if (Input.GetMouseButton(0))
			isTouchOrClick = true;
		return isTouchOrClick;
	}
}



