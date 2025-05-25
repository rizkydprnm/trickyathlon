using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{Mathf.FloorToInt(Player.Instance.Distance)}<size=12px>m";
    }
}
