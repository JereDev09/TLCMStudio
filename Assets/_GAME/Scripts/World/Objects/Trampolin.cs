using UnityEngine;

public class Trampolin : MonoBehaviour, IImpactable
{
    public void OnImpact(PlayerController controller)
    {
        controller.AplicarHit(controller.playerHitData.hitOnTrampolin);
    }
}
