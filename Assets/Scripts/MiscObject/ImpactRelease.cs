using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//we open this if the something hits us hard enough!!!

public class ImpactRelease : MonoBehaviour
{
	private Rigidbody ourRigidBody;
	public bool reportImpactIntensity = false;
	public GameObject ImpactParticlePrefab;
	public float ImpactTriggerIntensityThreshold = 10; //how hard do we have to get hit to activate?

	private bool hasTriggered = false;

	// Start is called before the first frame update
	void Start()
	{
		ourRigidBody = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (hasTriggered)
			return;
		//first off, determine
		Vector3 ForceVec = collision.relativeVelocity;

		float ImpactMagnitude = ForceVec.magnitude;
		if (reportImpactIntensity)
			print(this.name + ":" + ImpactMagnitude);

		if (ImpactMagnitude > ImpactTriggerIntensityThreshold)
			open(collision);

	}

	void open(Collision collision)
	{
		hasTriggered = true;
		print("HERE WE GO");
		ourRigidBody.constraints = RigidbodyConstraints.None;
		if(ImpactParticlePrefab != null)
		{
			GameObject newParticle = Instantiate(ImpactParticlePrefab);

			newParticle.transform.position = collision.contacts[0].point;

		}
	    this.gameObject.SetActive(false);
	}




}
