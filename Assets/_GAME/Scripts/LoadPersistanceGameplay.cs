using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPersistanceGameplay
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        if (PuedeInstanciarEscena())
        {
            SceneManager.LoadScene("PersistanceGameplay",LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogWarning("La escena PersistanceGameplay ya estaba creada");
        }
    }

    private static bool PuedeInstanciarEscena()
    {
        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            if (SceneManager.GetSceneAt(i).name == "PersistanceGameplay") return false;
        }

        return true;
    }
}
