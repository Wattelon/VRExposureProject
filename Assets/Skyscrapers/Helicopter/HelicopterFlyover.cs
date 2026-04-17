using DG.Tweening;
using UnityEngine;

public class HelicopterFlyover : MonoBehaviour
{
    [SerializeField] private GameObject helicopter;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;

    private bool hasStarted;

    private void Start()
    {
        helicopter.SetActive(false);
        hasStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        helicopter.SetActive(true);
        StartFlyover();
    }

    private void StartFlyover()
    {
        if (hasStarted) return;
        hasStarted = true;

        Vector3[] path = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            path[i] = waypoints[i].position;
        }

        helicopter.SetActive(true);
        helicopter.GetComponent<AudioSource>().Play();
        helicopter.transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(easing)
            .SetLookAt(0.01f)
            .OnComplete(() => { helicopter.SetActive(false); });
    }
}
