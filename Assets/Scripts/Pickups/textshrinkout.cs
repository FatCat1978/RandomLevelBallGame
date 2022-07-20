using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class textshrinkout : MonoBehaviour
{

	public string displayedValue= ""; //blank by default.

	[SerializeField] private float shrinkRatePerSecond = 0.2f; //per second

	public bool isShrinking = false;

	private TMP_Text textDisplay;
	private Vector3 shrinkVector;
	// Simple enough script
	void Start()
	{
		textDisplay = GetComponent<TMP_Text>();
		textDisplay.text = displayedValue;
		shrinkVector = new Vector3(shrinkRatePerSecond, shrinkRatePerSecond, shrinkRatePerSecond);
	}

	// Update is called once per frame
	void Update()
	{
		if (displayedValue != textDisplay.text)
			textDisplay.text = displayedValue;

		if (!isShrinking)
			return;

		this.transform.localScale -= shrinkVector*Time.deltaTime;
		if(this.transform.localScale.x < 0)
        {
			
			Destroy(this); //simple.
        }
	}

	public void changeText(string text)
    {
		
		displayedValue = text;
    }
}
