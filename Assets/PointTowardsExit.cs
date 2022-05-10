using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsExit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Target;
    void Start()
    {
        if (!Target)
        { 
            GameObject MainLevelGenerator = GameObject.Find("LevelGenerator");
            if (MainLevelGenerator != null)
			{
                LevelGenerator frick = MainLevelGenerator.GetComponent<LevelGenerator>();
                Target = frick.generatedEndRoom;
			}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
		{
            float finX = this.transform.position.x - Target.transform.position.x;
            float finY = this.transform.position.y - Target.transform.position.y;

            float rot = Mathf.Atan2(finY, finX);

            transform.LookAt(Target.transform);

        }
    }
}
