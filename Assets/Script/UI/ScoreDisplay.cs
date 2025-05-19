using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{Mathf.FloorToInt(Player.distance * 10) / 10f:F1}<size=12>M";
    }
}
