using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;
    
    private MeshRenderer _meshRenderer;

    public static SceneTransitionManager Instance { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Fade(true));
    }

    public void TransitionToScene(string sceneName)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        if (asyncOperation is null) return;
        asyncOperation.allowSceneActivation = false;

        _meshRenderer.gameObject.SetActive(true);
        StartCoroutine(Fade(false));
        StartCoroutine(TransitionRoutine(asyncOperation));
    }

    private IEnumerator TransitionRoutine(AsyncOperation asyncOperation)
    {
        while (_meshRenderer.material.color.a < 1f) yield return null;
        asyncOperation.allowSceneActivation = true;
    }

    private IEnumerator Fade(bool fadeIn)
    {
        var time = 0f;
        var fadeColor = Color.black;
        var start = fadeIn ? 1f : 0f;
        var finish = fadeIn ? 0f : 1f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(start, finish, time / fadeTime);
            _meshRenderer.material.color = fadeColor;
            yield return null;
        }
        
        if (fadeIn) _meshRenderer.gameObject.SetActive(false);
    }
}
