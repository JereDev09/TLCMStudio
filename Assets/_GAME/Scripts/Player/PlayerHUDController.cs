using TMPro;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] TMP_Text _metrosRecorridos;
    [SerializeField] string _metrosName;
    [SerializeField] TMP_Text _velocidadMovimiento;
    [SerializeField] string _velocidadaName;

    private void Update()
    {
        _metrosRecorridos.text = _metrosName + (int)gameManager.MetrosAvanzados;
        _velocidadMovimiento.text = _velocidadaName + (int)gameManager.WorldSpeed;
    }
}
