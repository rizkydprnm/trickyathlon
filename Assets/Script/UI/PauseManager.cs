using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    InputActionMap playerInput;

    void Start()
    {
        InputSystem.actions["UI/Cancel"].performed += _ => PauseGame(!pauseMenu.activeInHierarchy);
        InputSystem.actions["UI/Cancel"].Enable();

        playerInput = InputSystem.actions.FindActionMap("Player", true);
    }

    public void RestartGame()
    {
        Generator.Initialize(Generator.GetData().Seed);
        Time.timeScale = 1f; // Ensure time scale is reset
        playerInput.Enable(); // Re-enable player input
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame(bool state = true)
    {
        if (ResultScreen.shown) return; // Do not pause if result screen is shown
        if (state)
        {
            Time.timeScale = 0f; // Pause the game
            playerInput.Disable(); // Disable player input
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            playerInput.Enable(); // Re-enable player input
            pauseMenu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        playerInput.Enable(); // Re-enable player input
        SceneManager.LoadScene("MainMenu");
    }
}