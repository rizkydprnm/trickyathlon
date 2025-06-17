using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    enum Mode
    {
        RandomSeed,
        SetSeed,
    }

    [SerializeField] private Mode mode = Mode.RandomSeed;

    [SerializeField] int seed;
    [SerializeField] TextMeshProUGUI seedText;

    public void Execute()
    {
        switch (mode)
        {
            case Mode.RandomSeed:
                Generator.Initialize(UnityEngine.Random.Range(0, int.MaxValue));
                SceneManager.LoadScene("GeneratorTest");
                break;

            case Mode.SetSeed:
                try
                {
                    seed = Convert.ToInt32(seedText.text, 16);
                }
                catch (FormatException)
                {
                    Debug.LogError("Invalid seed format: " + seedText.text);
                    return;
                }

                Generator.Initialize(seed);
                SceneManager.LoadScene("GeneratorTest");
                break;
        }
    }
}
