using System;
using TMPro;
using UnityEngine;

using PrimeTween;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI seedText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] GameObject[] uiElements;

    void Start()
    {
        Player.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    void OnPlayerDeath()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(false);
        }
        Tween.UIAnchoredPositionX(GetComponent<RectTransform>(), startValue: 200, endValue: 0, duration: 0.125f, Ease.InCirc);
        Player.OnPlayerDeath.RemoveListener(OnPlayerDeath);

        GeneratorData data = Generator.GetData();
        scoreText.text = $"{Player.Instance.Distance:F2}<size=8>m</size>";
        seedText.text = $"{data.Seed:X8}"; // Changed to hexadecimal format with 8-digit padding
        speedText.text = $"{Player.Instance.Distance / Player.Instance.Playtime:F2}";

        TimeSpan time = TimeSpan.FromSeconds(Player.Instance.Playtime);
        timeText.text = $"{time:mm\\:ss\\.fff}";
    }

    public void RestartGame(bool randomSeed = false)
    {
        if (randomSeed)
        {
            Generator.Initialize(UnityEngine.Random.Range(0, int.MaxValue));
        }
        else
        {
            Generator.Initialize(Generator.GetData().Seed);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
