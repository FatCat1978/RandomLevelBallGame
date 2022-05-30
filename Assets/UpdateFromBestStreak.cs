using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateFromBestStreak : MonoBehaviour //make this a generic class.
{
    [SerializeField] PlayerPrefsManager PlayerPrefsManagerInst;
    void FixedUpdate()
    {
        TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "BEST STREAK:" + PlayerPrefsManagerInst.floorsStreakMax;
    }
}
