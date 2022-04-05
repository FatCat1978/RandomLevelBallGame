using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float CameraSensitivity = 45; //angle change per second
	public float BallMaxMagnitude = 10; //under this we can give it a big ol' smackerooni. speed limit for non big hits!!
	public float BallHitManaDrain = 10;
	public float BallHitSpeed;
	public float BallHitCooldown = 1; //how many seconds between hits???/

	public float CameraScrollModifier = 1;

	private long NextBallHit;

	//public bool TurningEnabled = true;

	public HitModes HitModeUsed = HitModes.HitModeConstant; 

	public GameObject BallHitReference; //don't edit this!!!

	private bool spaceReleased = true;  //have we let go of space? Only used for "classic" hit mode

	private CameraFollower CameraCtrl;

	public void Start()
	{
		CameraCtrl = this.gameObject.GetComponent<CameraFollower>();
	}

	private void FixedUpdate()
	{//TODO - convert this! It should use the modern input system.

		bool APressed = Input.GetKey(KeyCode.A);
		bool DPressed = Input.GetKey(KeyCode.D);


		if (DPressed)
		{
			//havePills();
			CameraCtrl.CameraAngle += CameraSensitivity*Time.deltaTime;
		}
		if(APressed)
		{
			CameraCtrl.CameraAngle -= CameraSensitivity * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Space))
		{

			AttemptToHitBall(); //wow cool name nerd why don't you attempt to hit some ass for once
		}
		else
		{
			//we need to be past around like 80% of the cooldown!!
			//if(Time.time<NextBallHit)
				spaceReleased = true;
		}

		float ScrlWheel = Input.GetAxis("Mouse ScrollWheel");
		if (ScrlWheel != 0f)
			CameraCtrl.CameraYOffset += (ScrlWheel*-1)*CameraScrollModifier;
		CameraCtrl.CameraYOffset = Mathf.Clamp(CameraCtrl.CameraYOffset, CameraCtrl.MinCameraHeight, CameraCtrl.MaxCameraHeight);

		}

	private void AttemptToHitBall()
	{
		if(canHit())
		{
			spaceReleased = false;
			float dist_away = 1; //shouldn't matter???

			float temp_angle = this.gameObject.GetComponent<CameraFollower>().CameraAngle;

			float tempAngle = temp_angle * Mathf.PI / 180;
			float XOffSet = dist_away * Mathf.Cos(tempAngle);// + CameraTarget.transform.position.x;
			float ZOffset = dist_away * Mathf.Sin(tempAngle);// + CameraTarget.transform.position.z;

			BallHitReference.transform.position = this.gameObject.transform.position + new Vector3(XOffSet, 0, ZOffset);
			BallHitReference.transform.LookAt(this.gameObject.transform);
			//we take a hint from the camera follow thing here.
			//get a vector based on the camera angle - hit it based on that angle / the ballHitSpeed Variable!!
			//add the force here.
			Rigidbody currentRig = this.gameObject.GetComponent<Rigidbody>();
			currentRig.AddForce(BallHitReference.transform.forward * BallHitSpeed);

			//now we handle the base behavior based on what our Enum's set to
			if(HitModeUsed == HitModes.HitModeNormal)
			{ //play a hit sound, add delay, remove preset value!
				NextBallHit = (long)(Time.time + BallHitCooldown);
				PlayerInfo pinfo = this.gameObject.GetComponent<PlayerInfo>();
				if (pinfo != null)
				{
					pinfo.drainEnergy(BallHitManaDrain);
					pinfo.HitCount++;
				}
			}
			if(HitModeUsed == HitModes.HitModeConstant)
			{ //no hitsound, stamina/magic drain is based on deltatime instead!
				PlayerInfo pinfo = this.gameObject.GetComponent<PlayerInfo>();
				if(pinfo != null)
				{
					pinfo.drainEnergy(BallHitManaDrain*Time.fixedDeltaTime);
				}

			}

		}
		else
		{
			return;//play a failure noise here.
		}
	}

	public bool canHit()
	{
		//are we still holding space (and on the classic mode?)
		if(HitModeUsed == HitModes.HitModeNormal)
		{
			if (!spaceReleased)
				return false;
		}


		Rigidbody currentRig = this.gameObject.GetComponent<Rigidbody>();
		if (currentRig != null)
		{
			if ((long)Time.time < NextBallHit) { return false; }
			if (currentRig.IsSleeping())
				return true;
			if (currentRig.velocity.magnitude < BallMaxMagnitude)
				return true;
			return false;
		}
		else return false;
	}


	public enum HitModes //dev
	{ 
	HitModeConstant, //doesn't play a hitsound, doesn't use a cooldown asides from the fixed update. 
	HitModeNormal
	}



}



