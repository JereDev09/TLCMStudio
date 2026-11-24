using System;
using TMPro;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private TMP_Text _metrosRecorridos;
    [SerializeField] private string _metrosName;
    [SerializeField] private TMP_Text _velocidadMovimiento;
    [SerializeField] private string _velocidadaName;

    private void OnEnable()
    {
        CañonController.OnCañonDisparado += MostrarUI;
    }
    private void OnDisable()
    {
        CañonController.OnCañonDisparado -= MostrarUI;
    }

    private void MostrarUI(Vector2 vector, float arg2)
    {
        if (animator == null) return;

        animator.SetTrigger("MostrarHUD");
    }

    private void Update()
    {
        _metrosRecorridos.text = _metrosName + (int)GameManager.instance.MetrosAvanzados;
        _velocidadMovimiento.text = _velocidadaName + (int)GameManager.instance.WorldSpeed;
    }
}
