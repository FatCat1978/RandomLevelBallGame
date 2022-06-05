using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//per second
public class RotateAtSpeed : MonoBehaviour
{
    public float speed; //we only rotate @ y
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
		{
            this.gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		}
        
    }
}
