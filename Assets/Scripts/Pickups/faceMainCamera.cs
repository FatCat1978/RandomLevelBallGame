using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceMainCamera : MonoBehaviour
{

    private GameObject cameraToFace;
    // Start is called before the first frame update
    void Start()
    {
        cameraToFace = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraToFace != null)
        {
            this.transform.forward = cameraToFace.transform.forward;   //name of the script's a little off - this "faces" the camera in the sense that it should always be readable due to the forward vectors matching.
        } //this accounts for transformation and everything.
    }
}
