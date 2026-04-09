using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

public class UILocomotionDeactivator : MonoBehaviour
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction turnAction;
    [SerializeField] private List<RayInteractor> rayInteractors;

    private void OnEnable()
    {
        moveAction.Enable();
        turnAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        turnAction.Disable();
    }

    void Update()
    {
        foreach (var rayInteractor in rayInteractors)
        {
            rayInteractor.enabled = moveAction.ReadValue<Vector2>() == Vector2.zero && turnAction.ReadValue<Vector2>() == Vector2.zero;
        }
    }
}
