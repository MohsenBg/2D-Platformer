using UnityEngine;

public class Menu : MonoBehaviour
{

    void Start()
    {

    }
    public void OnClickPlayButton()
    {
        SceneController.LoadNextScene();
    }

    public void OnClickQuiteButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Exit Game");
#else
        Application.Quit();
#endif
    }
}
