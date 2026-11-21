using UnityEngine;

public class ParedSolida : MonoBehaviour, IImpactable, IRompible
{
    public void OnImpact(PlayerController controller)
    {
        controller.AplicarHit(controller.playerHitData.hitOnParedSolida);

        Romper();
    }

    public void Romper()
    {
        Destroy(gameObject);
    }
}
