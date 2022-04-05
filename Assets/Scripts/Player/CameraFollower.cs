using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	// Start is called before the first frame update
	private float CameraXOffset = 0; //these are set @ runtime via horizontal

	private float CameraZOffset = 0;

	public float CameraAngleChangePerSecond = 0;

	public float CameraDistance;
	public float CameraYOffset;

	public float CameraAngle = 0; //+-360

	public float MaxCameraHeight = 10f;
	public float MinCameraHeight = .5f;
	
	
	public GameObject cameraObject;

	public Transform CameraTarget;




   void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		CameraAngle += CameraAngleChangePerSecond*Time.deltaTime;
		CameraAngle = CameraAngle % 360;
		//trig - we don't need to calculate the Y offset! So we pretend it's top down.
		//we have the current angle of the camera, and the horizontal distance the camera is away! all we need for now
		float tempAngle = CameraAngle * Mathf.PI / 180;
		CameraXOffset = CameraDistance * Mathf.Cos(tempAngle);// + CameraTarget.transform.position.x;
		CameraZOffset = CameraDistance * Mathf.Sin(tempAngle);// + CameraTarget.transform.position.z;

		//finally, make the camera take up the desired position, and point it @ our target
		cameraObject.transform.position = CameraTarget.transform.position + new Vector3 (CameraXOffset, CameraYOffset, CameraZOffset);
		cameraObject.transform.LookAt(CameraTarget);
	}
}
