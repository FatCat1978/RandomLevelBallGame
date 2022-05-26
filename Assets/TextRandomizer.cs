using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRandomizer : MonoBehaviour
{
    [SerializeField] string[] PossibleStrings;
    // Start is called before the first frame update
    void Start()
    {
       TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "cool. randomize this.";
    }
}
