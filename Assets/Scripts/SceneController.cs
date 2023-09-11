using UnityEngine.SceneManagement;

public static class SceneController
{
    public static int CurrentScene { get { return SceneManager.GetActiveScene().buildIndex; } }

    public static void LoadNextScene()
    {
        if (CurrentScene + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(CurrentScene + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public static void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadCurrentScene()
    {
        SceneManager.LoadScene(CurrentScene);
    }
}
