using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PointTowardsTarget : MonoBehaviour
{
    [SerializeField ] private LineRenderer lineRenderer;
    [SerializeField] private float lineDist;
    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        if(!lineRenderer)
            lineRenderer = GetComponent<LineRenderer>();
        
        //We assume that the target is the end room!
        if(!target)
		{
            LevelGenerator LG = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
            target = LG.generatedEndRoom;
		}

    }

    // Update is called once per frame
    void Update()
    {
        if (!target || !lineRenderer)
            return;
        float adjY = target.transform.position.z-this.transform.position.z; 
        float adjX = target.transform.position.x-this.transform.position.x;
        float angle = Mathf.Atan2(adjY,adjX);

        float x0 = this.transform.position.x;
        float y0 = this.transform.position.z;

        float x = x0 + lineDist*Mathf.Cos(angle);        
        float y = y0 + lineDist*Mathf.Sin(angle);

        Vector3 lineEndPos = new Vector3(x,this.transform.position.y,y);


        lineRenderer.SetPositions(new Vector3[] { this.transform.position, lineEndPos});

    }
}
