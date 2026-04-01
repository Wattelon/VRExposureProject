using UnityEngine;

public class GodrayFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        if (player == null) return;
        transform.position = player.position + offset;
    }
}
