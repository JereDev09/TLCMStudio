using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneLoader.LoadSceneWithTransition("MainGame");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
