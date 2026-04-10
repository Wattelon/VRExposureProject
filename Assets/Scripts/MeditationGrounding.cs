using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeditationGrounding : MonoBehaviour
{
    [SerializeField] private List<Renderer> groundingObjects;
    [SerializeField][Range(1, 10)] private int groundingObjectAmount;
    [SerializeField] private Material outlineMaterial;

    private List<Renderer> _currentGroundingObjects;

    private void OnEnable()
    {
        _currentGroundingObjects = GetRandomGroundingObjects();
        foreach (var groundingRenderer in _currentGroundingObjects)
        {
            groundingRenderer.materials = groundingRenderer.materials.Append(outlineMaterial).ToArray();
        }
    }

    private void OnDisable()
    {
        foreach (var groundingRenderer in _currentGroundingObjects)
        {
            groundingRenderer.materials = groundingRenderer.materials[..^1];
        }
        _currentGroundingObjects.Clear();
    }

    private List<Renderer> GetRandomGroundingObjects()
    {
        var randomGroundingObjects = new List<Renderer>();
        var activeObjects = groundingObjects.Where(groundingObject => groundingObject.gameObject.activeInHierarchy).ToList();

        var currentGroundingObjectAmount = Mathf.Min(activeObjects.Count, groundingObjectAmount);
        
        for (var i = 0; i < currentGroundingObjectAmount; i++)
        {
            var groundingObject = activeObjects[Random.Range(0, activeObjects.Count)];
            randomGroundingObjects.Add(groundingObject);
            activeObjects.Remove(groundingObject);
        }
        
        return randomGroundingObjects;
    }
}
