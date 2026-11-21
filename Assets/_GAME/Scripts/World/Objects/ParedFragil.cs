using UnityEngine;

public class ParedFragil : MonoBehaviour, IImpactable, IRompible
{
    public void OnImpact(PlayerController controller)
    {
        Romper();
    }

    public void Romper()
    {
        GameManager.AddOneCombo();

        Destroy(gameObject);
    }
}
