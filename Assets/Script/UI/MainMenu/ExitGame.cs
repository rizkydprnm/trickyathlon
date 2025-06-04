#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // This line is only necessary if you are running this in the Unity Editor
#endif
    }
}
