using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneLoader
{
    public static TransitionController transitionController;

    private static string EscenaMantener = "PersistanceGameplay";

    public static void LoadScene(string sceneName)
    {
        Debug.Log("loading");

        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {

            if (SceneManager.GetSceneAt(i).name != EscenaMantener) SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }

        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
    }

    public static void LoadSceneWithTransition(string sceneName)
    {
        transitionController.StartLoad(sceneName);
    }
}
