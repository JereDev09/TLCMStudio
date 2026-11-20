using UnityEngine;

public class Losa : MonoBehaviour, IImpactable
{
    [SerializeField] private float _resistencia = 25f;
    [SerializeField] private float _conservacion = 0.8f;

    public void OnImpact(PlayerController controller)
    {
        float velocityY = Mathf.Abs(controller.LastVelocityY);

        if (velocityY > _resistencia)
        {
            controller.SetNewVelocity(controller.LastVelocityY * _conservacion);

            GameManager.AddOneCombo();

            Destroy(gameObject);
        }


    }
}
