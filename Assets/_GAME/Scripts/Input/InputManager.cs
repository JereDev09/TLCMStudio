using UnityEngine;

public static class InputManager
{
    private static GameInputs _gameInputs;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        _gameInputs = new GameInputs();
        _gameInputs.Enable();
    }

    public static GameInputs GameInputs => _gameInputs;
}