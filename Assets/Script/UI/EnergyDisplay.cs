using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyDisplay : MonoBehaviour
{
    public TextMeshProUGUI energyText;

    // Update is called once per frame
    void Update()
    {
        energyText.text = $"<color=#FF9800>ENERGY</color>\n<size=12px>{Mathf.CeilToInt(Player.Instance.Energy)}%</size>";
    }
}
