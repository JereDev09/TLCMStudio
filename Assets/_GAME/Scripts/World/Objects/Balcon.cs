using UnityEngine;

public class Balcon : MonoBehaviour, IImpactable
{
    public void OnImpact(PlayerController controller)
    {
        bool aplicoHit = controller.AplicarHit(controller.playerHitData.hitOnBalcon);

        if (aplicoHit) GameManager.AddOneCombo();
    }
}
