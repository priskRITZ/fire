using UnityEngine;

public class FirefighterMovement : MonoBehaviour
{
    public Transform targetBuilding;
    public float speed = 5.0f;
    private FireHydrant assignedHydrant;
    private bool isExtinguishing = false;

    void Start()
    {
        assignedHydrant = FindNearestAvailableHydrant();
        if (assignedHydrant != null)
        {
            assignedHydrant.AddFirefighter();
        }
    }

    void Update()
    {
        if (!isExtinguishing && assignedHydrant != null)
        {
            MoveTowardsHydrant();
        }
        else if (assignedHydrant == null)
        {
            MoveTowardsBuilding();
        }
    }

    void MoveTowardsHydrant()
    {
        if (Vector3.Distance(transform.position, assignedHydrant.transform.position) > 1f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, assignedHydrant.transform.position, step);
        }
        else
        {
            // 소방관이 hydrant에 도착했을 때
            isExtinguishing = true;
        }
    }

    void MoveTowardsBuilding()
    {
        if (Vector3.Distance(transform.position, targetBuilding.position) > 1f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetBuilding.position, step);
        }
    }

    FireHydrant FindNearestAvailableHydrant()
    {
        FireHydrant[] hydrants = FindObjectsOfType<FireHydrant>();
        FireHydrant nearestHydrant = null;
        float minDistance = Mathf.Infinity;

        foreach (FireHydrant hydrant in hydrants)
        {
            if (hydrant.IsAvailable() && !IsWithinExtinguishZone(hydrant.transform.position))
            {
                float distance = Vector3.Distance(transform.position, hydrant.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestHydrant = hydrant;
                }
            }
        }

        return nearestHydrant;
    }

    bool IsWithinExtinguishZone(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 1f); // 반경을 적절히 조정
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("ExtinguishZone"))
            {
                return true;
            }
        }
        return false;
    }
}
