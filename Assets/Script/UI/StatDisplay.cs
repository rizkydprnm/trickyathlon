using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public TextMeshProUGUI energyText;

    // Update is called once per frame
    void Update()
    {
        energyText.text = $"<color=#FF9800>SPEED</color>\n<size=12px>{(int)Player.Instance.Speed}</size>m/s";
    }
}
