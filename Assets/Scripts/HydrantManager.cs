using System.Collections.Generic;
using UnityEngine;

public class HydrantManager : MonoBehaviour
{
    private List<Hydrant> hydrants = new List<Hydrant>();

    private void Start()
    {
        // 씬의 모든 Hydrant를 찾아 리스트에 추가
        hydrants.AddRange(FindObjectsOfType<Hydrant>());
        Debug.Log($"Found {hydrants.Count} hydrants in the scene");
    }

    public Hydrant FindNearestAvailableHydrant(Vector3 position)
    {
        Hydrant nearestHydrant = null;
        float nearestDistance = float.MaxValue;

        foreach (Hydrant hydrant in hydrants)
        {
            if (hydrant.CanUse())
            {
                float distance = Vector3.Distance(position, hydrant.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestHydrant = hydrant;
                }
            }
        }

        return nearestHydrant;
    }
}