using System;
using UnityEngine;

[Serializable]
public class ChunkEntry
{
    public Chunk prefab;
    public float pesoAparicion = 1f;
    public float desdeMetros = 0f;
}

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Chunk[] _chunksIniciales; //para tutorial
    [SerializeField] private ChunkEntry[] _chunks;
    [SerializeField] private float _spawnX = 40f;
    [SerializeField] private float _segundosMinimosSpawn;
    [SerializeField] private float _segundosMaximosSpawn;
    [SerializeField] private float _metrosMinimos;
    [SerializeField] private float _metrosMaximos;

    private float _distanciaToSpawn;

    private Chunk _ultimo;

    private int _indiceInicial;

    private bool _canSpawn;

    private float GetNextDistanceToSpawn()
    {
        float time = UnityEngine.Random.Range(_segundosMinimosSpawn, _segundosMaximosSpawn);
        float espacio = GameManager.WorldSpeed * time;
        espacio = Mathf.Clamp(espacio, _metrosMinimos, _metrosMaximos);

        return espacio;
    }

    private void Start()
    {
        _distanciaToSpawn = GetNextDistanceToSpawn();
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += GameStart;
        GameManager.OnGameEnd += GameEnd;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= GameStart;
        GameManager.OnGameEnd -= GameEnd;
    }

    private void GameEnd()
    {
        _canSpawn = false;
    }

    private void GameStart()
    {
        _canSpawn = true;
    }

    private void Update()
    {
        if (!_canSpawn) return;

        while (_ultimo ==  null || _ultimo.BordeDerecho + _distanciaToSpawn < _spawnX)
        {
            Chunk prefab = SiguientePrefab();

            float x = _ultimo == null ? 0f : _ultimo.BordeDerecho + _distanciaToSpawn;
            _ultimo = Instantiate(prefab, new Vector3(x, 0f, 0f), Quaternion.identity);

            _distanciaToSpawn = GetNextDistanceToSpawn();

            if (_ultimo.BordeDerecho <= x)
            {
                Debug.LogError($"{prefab.name} tiene _horizontalSize <= 0");
                enabled = false;
                return;
            }
        }
    }

    private Chunk SiguientePrefab()
    {
        if (_indiceInicial < _chunksIniciales.Length) return _chunksIniciales[_indiceInicial++];

        float metros = GameManager.MetrosAvanzados;
        float total = 0f;

        foreach(var c in _chunks)
        {
            if (metros >= c.desdeMetros)
            {
                total += c.pesoAparicion;
            }
        }

        float r = UnityEngine.Random.Range(0f, total);

        foreach(var c in _chunks)
        {
            if (metros < c.desdeMetros) continue;

            r -= c.pesoAparicion;

            if (r <= 0f) return c.prefab;
        }

        return _chunks[0].prefab;
    }
}
