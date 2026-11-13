using UnityEngine;

public class CañonFlechaAnimController : MonoBehaviour
{
    [SerializeField] private Animator _impulsoUI;
    [SerializeField] private CañonController _cañonController;
    [SerializeField] private Transform _flechaPivot;

    private void OnEnable()
    {
        CañonController.OnCambioEstadoCañon += CambioEstadoCañon;
    }

    private void CambioEstadoCañon(EstadoCañon cañon)
    {
        switch (cañon)
        {
            case EstadoCañon.NONE:
                break;
            case EstadoCañon.APUNTANDO:
                break;
            case EstadoCañon.PREPARANDO_DISPARO:
                _impulsoUI.SetTrigger("AparecerFlecha");
                break;
            case EstadoCañon.DISPARANDO:
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        CañonController.OnCambioEstadoCañon -= CambioEstadoCañon;
    }

    private void Update()
    {
        _flechaPivot.eulerAngles = new Vector3(0f, 0f, _cañonController.PotenciaDisparo * 90f);
    }
}
