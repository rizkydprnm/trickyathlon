using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{Mathf.FloorToInt(Player.distance * 10) / 10f:F1}<size=12px>m";
    }
}
