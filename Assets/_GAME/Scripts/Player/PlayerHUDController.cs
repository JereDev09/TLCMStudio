using TMPro;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] TMP_Text _metrosRecorridos;
    [SerializeField] string _metrosName;
    [SerializeField] TMP_Text _velocidadMovimiento;
    [SerializeField] string _velocidadaName;

    private void Update()
    {
        _metrosRecorridos.text = _metrosName + (int)GameManager.MetrosAvanzados;
        _velocidadMovimiento.text = _velocidadaName + (int)GameManager.WorldSpeed;
    }
}
