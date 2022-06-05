using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromArrayAndDestroy : MonoBehaviour
{
    public GameObject[] toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        int RandomPick = Random.Range(0, toSpawn.Length);
        GameObject funny = Instantiate(toSpawn[RandomPick]);
        funny.transform.SetParent(this.transform.parent, false);
        funny.transform.position = this.transform.position;
        Destroy(this.gameObject);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
