using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeFadeIn : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxVolume;

    public float secsToMaxVol = 1;

    private AudioSource m_Source;
    private float increaseBy;
    private bool maxVolAchieved = false; 
    // Start is called before the first frame update
    void Start()
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.volume = 0;

        increaseBy = maxVolume / secsToMaxVol;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(!maxVolAchieved)
        { 
            m_Source.volume += increaseBy * Time.deltaTime;
            if (m_Source.volume > 1)
            {
                m_Source.volume = 1;
                increaseBy = 0;
                maxVolAchieved = true;
            }
        }
    }
}
