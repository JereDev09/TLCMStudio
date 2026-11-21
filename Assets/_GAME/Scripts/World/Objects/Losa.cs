using UnityEngine;

public class Losa : MonoBehaviour, IImpactable , IRompible
{
    [SerializeField] private float _resistencia = 25f;
    [SerializeField] private float _conservacion = 0.8f;

    public void OnImpact(PlayerController controller)
    {
        float velocityY = Mathf.Abs(controller.LastVelocityY);

        if (velocityY > _resistencia)
        {
            controller.SetNewVelocity(controller.LastVelocityY * _conservacion);

            Romper();
        }
    }

    public void Romper()
    {
        GameManager.AddOneCombo();

        Destroy(gameObject);
    }
}
