using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _intialWorldSpeed;
    [SerializeField] private float _intialMetrosAvanzados;

    private static float _worldSpeed;
    private static float _metrosAvanzados;
    private static int _combo;

    public static event Action OnGameStart;
    public static event Action OnGameEnd;
    public static event Action<int> OnComboChanged;
    public static bool GameEnded;

    public static void StartGame()
    {
        OnGameStart?.Invoke();
        GameEnded = false;
    }

    private void Start()
    {
        _worldSpeed = _intialWorldSpeed;
        _metrosAvanzados = _intialMetrosAvanzados;
        _combo = 0;
    }

    private void Update()
    {
        _metrosAvanzados += WorldSpeed * Time.deltaTime;

        CheckGameEnd();
    }

    private void CheckGameEnd()
    {
        if (_worldSpeed <= 0 && !GameEnded)
        {
            GameEnded = true;
            _worldSpeed = 0;

            OnGameEnd?.Invoke();
        }
    }

    public static void AddOneCombo()
    {
        _combo += 1;
        OnComboChanged?.Invoke(_combo);
        Debug.Log("combo " + _combo + "!");
    }

    public static void RestartCombo()
    {
        _combo = 0;
        OnComboChanged?.Invoke(_combo);
        Debug.Log("COMBO PERDIDO :C");
    }

    public static void SetWorldSpeed(float speed)
    {
        _worldSpeed = speed;
    }

    public static void AddWorldSpeed(float value)
    {
        if (GameEnded) return;

        _worldSpeed += value;
    }

    public static float MetrosAvanzados => _metrosAvanzados;
    public static float WorldSpeed => _worldSpeed;
    public static int Combo => _combo;
}
