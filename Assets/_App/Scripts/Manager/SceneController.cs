using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static int CurrentScene { get { return SceneManager.GetActiveScene().buildIndex; } }

    public static void LoadNextScene()
    {
        if (!IsLastScene())
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

    public static void LoadLastScene()
    {
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastSceneIndex);
    }

    public static bool IsLastScene()
    {
        return CurrentScene == SceneManager.sceneCountInBuildSettings - 1;
    }

    public static void LoadLevelScene(int level)
    {
        level = Mathf.Min(SceneManager.sceneCountInBuildSettings - 1, level);
        SceneManager.LoadScene(level);
    }
}
