using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLimit : MonoBehaviour
{
    // Start is called before the first frame update
    public float maximumSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        float speed = Vector3.Magnitude(GetComponent<Rigidbody>().velocity);  // test current object speed

        if (speed > maximumSpeed)

        {
            float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = GetComponent<Rigidbody>().velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            GetComponent<Rigidbody>().AddForce(-brakeVelocity);  // apply opposing brake force
        }
    }
}
