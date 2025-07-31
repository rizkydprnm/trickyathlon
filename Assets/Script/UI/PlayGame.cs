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

    int seed;
    [SerializeField] TextMeshProUGUI seedText;
    [SerializeField] AudioSource errorSound;

    public void Execute()
    {
        switch (mode)
        {
            case Mode.RandomSeed:
                Generator.Initialize(UnityEngine.Random.Range(0, int.MaxValue));
                SceneManager.LoadScene("GeneratorTest");
                break;

            case Mode.SetSeed:
                string trimmedText = seedText.text.Trim();

                // Filter out non-hex characters
                string hexString = System.Text.RegularExpressions.Regex.Replace(trimmedText, "[^0-9a-fA-F]", "");

                if (string.IsNullOrEmpty(hexString))
                {
                    errorSound.Play();
                    Debug.LogError("No valid hexadecimal characters in input");
                    return;
                }

                if (int.TryParse(hexString, System.Globalization.NumberStyles.HexNumber, null, out seed))
                {
                    Debug.Log($"Seed: {seed}");
                    Generator.Initialize(seed);
                    SceneManager.LoadScene("GeneratorTest");
                }
                else
                {
                    Debug.LogError($"Failed to parse '{hexString}' as hexadecimal number");
                }
                break;
        }
    }
}
