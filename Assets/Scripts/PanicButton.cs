using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PanicButton : MonoBehaviour
{
    [SerializeField] private InputAction panicAction;
    [SerializeField] private List<GameObject> meditations;
    
    private const string ModularSceneName = "ModularSettings";
    private const string SafeSpaceSceneName = "SafeSpace";

    private static PanicButton _instance;
    
    private void Awake()
    {
        if (_instance is null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

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
        if (SceneManager.GetActiveScene().name == ModularSceneName) return;
        if (SceneManager.GetActiveScene().name == SafeSpaceSceneName)
        {
            if (meditations.Any(meditation => meditation.activeSelf)) meditations.ForEach(meditation => meditation.SetActive(false));
            else SceneManager.LoadScene(ModularSceneName);
        }
        else SceneManager.LoadScene(SafeSpaceSceneName);
    }
}
