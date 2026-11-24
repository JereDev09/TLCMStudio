using UnityEngine;

public class WorldFeedBack : MonoBehaviour
{
    [SerializeField] private ParticleSystem _lineasVelocidad;
    //numero mas bajo hace mas dificil subir el progreso de la curva
    [SerializeField] private float _factorSpawneo;
    [SerializeField] private float _maxParticles;
    [SerializeField] private AnimationCurve _lineasVelocidadCurva;


    private void Update()
    {
        float progress = GameManager.instance.WorldSpeed * _factorSpawneo;

        ParticleSystem.EmissionModule Module = _lineasVelocidad.emission;

        progress = Mathf.Clamp01(progress);

        Module.rateOverTime = _lineasVelocidadCurva.Evaluate(progress) * _maxParticles;

    }
}
