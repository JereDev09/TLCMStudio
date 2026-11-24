using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float _intialWorldSpeed;
    [SerializeField] private float _intialMetrosAvanzados;

    private float _worldSpeed;
    private float _metrosAvanzados;
    private int _combo;

    public static event Action OnGameStart;
    public static event Action OnGameEnd;
    public static event Action<int> OnComboChanged;
    public bool GameEnded;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
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

    public void AddOneCombo()
    {
        _combo += 1;
        OnComboChanged?.Invoke(_combo);
        Debug.Log("combo " + _combo + "!");
    }

    public void RestartCombo()
    {
        _combo = 0;
        OnComboChanged?.Invoke(_combo);
        Debug.Log("COMBO PERDIDO :C");
    }

    public void SetWorldSpeed(float speed)
    {
        _worldSpeed = speed;
    }

    public void AddWorldSpeed(float value)
    {
        if (GameEnded) return;

        _worldSpeed += value;
    }

    public float MetrosAvanzados => _metrosAvanzados;
    public float WorldSpeed => _worldSpeed;
    public int Combo => _combo;
}
