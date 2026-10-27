using UnityEngine;

public class CameraFeedBackController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private float shakeAmount;
    private float shakeSpeed;
    private float shakeFrequency;
    public float shakeMultiplier;
    public float maxShakeAmount;
    public float maxShakeSpeed;
    public float minShakeFrenquency;
    public float ShakeAmountMultiplier;
    public float ShakeSpeedMultiplier;
    public float ShakeFrequencyFactor;
    private float _shakeTime;
    public Vector2 shakePos;
    public Vector2 targetShake;
    public Vector3 cameraPos = new Vector3(0,0,-10f);

    private void Update()
    {
        ShakeController();

        transform.position = cameraPos + (Vector3)shakePos;
    }

    private void ShakeController()
    {
        shakeAmount = Mathf.Min(gameManager.WorldSpeed * ShakeAmountMultiplier,maxShakeAmount);
        shakeSpeed = Mathf.Min(gameManager.WorldSpeed * ShakeSpeedMultiplier, maxShakeSpeed);
        shakeFrequency = Mathf.Max(ShakeFrequencyFactor / gameManager.WorldSpeed,minShakeFrenquency);

        _shakeTime += Time.deltaTime;

        if (_shakeTime > shakeFrequency)
        {
            _shakeTime = 0;
            targetShake = new Vector2(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount));
        }

        shakePos = Vector2.Lerp(shakePos, targetShake, shakeSpeed * Time.deltaTime) * shakeMultiplier;
    }
}
