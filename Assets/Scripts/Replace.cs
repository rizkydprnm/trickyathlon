using TMPro;
using UnityEngine;

public class Replace : MonoBehaviour
{
    #if POLINES
    void Awake()
    {
        var textMesh = GetComponent<TextMeshProUGUI>();    
        textMesh.text = textMesh.text.Replace("SekaliPakaiBuang", "Rizky Dwi Purnama");
    }
    #endif
}
