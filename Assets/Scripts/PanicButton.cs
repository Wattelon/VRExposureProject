using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PanicButton : MonoBehaviour
{
    [SerializeField] private InputAction panicAction;
    
    private const string ModularSceneName = "ModularSettings";

    private void OnEnable()
    {
        panicAction.Enable();
        panicAction.performed += OnPanicButtonClicked;
    }

    private void OnDisable()
    {
        panicAction.performed -= OnPanicButtonClicked;
        panicAction.Disable();
    }
    
    private void OnPanicButtonClicked(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(ModularSceneName);
    }
}
