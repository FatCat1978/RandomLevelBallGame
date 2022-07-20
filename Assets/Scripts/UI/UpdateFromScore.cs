using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateFromScore : MonoBehaviour
{
    [SerializeField] PlayerPrefsManager PlayerPrefsManagerInst;
    void FixedUpdate()
    {
        TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "SCORE:" + PlayerPrefsManagerInst.scoreThisFloor;
    }
}
