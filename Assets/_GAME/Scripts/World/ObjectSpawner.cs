using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [Header("Prefab")]
    [SerializeField] private GameObject _ObjectPrefab;

    [Space(10)]
    [Header("Configuracion inicial")]
    [SerializeField] private SpawnType _TipoSpawneo;
    [SerializeField] private Entornos _DondeSpawneara;
    [Tooltip("Desactivalo para que solo se spawnee en el centro del objeto")]
    [SerializeField] private bool _SpawnInArea = true;
    [SerializeField] private bool _UsarMultiplesReferencias;
    [Tooltip("Si esta activo usara las referencias de esta lista de forma aleatoria sin usar su tamaño solo posicion")]
    [SerializeField] private Transform[] _Referencias;

    [Space(10)]
    [Header("Configuracion de spawneo")]
    [SerializeField] private float _MetrosMinimosParaSpawnear;
    [SerializeField] private float _TiempoParaReespawnear;
    [SerializeField] private float _MetrosParaReespawnear;
    [Tooltip("Activalo para que cuando se llegue a la meta de metros o tiempo y la probabilidad falle se resetee la espera")]
    [SerializeField] private bool _ResetearEspera;
    [Tooltip("Cambialo cuando Resetear espera esta desactivado para que vuelva a intentar spawnear el objeto luego del tiempo que elijas")]
    [SerializeField] private float _EsperaReintentoSpawn;
    [Range(0f,100f)]
    [SerializeField] private float _ProbabilidadParaSpawnear;

    
}
