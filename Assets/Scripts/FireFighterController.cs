using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Linq;

public class FirefighterController : MonoBehaviour
{
    public GameObject[] hydrants;
    public GameObject fireTarget;
    public float proximityDistance = 2f;
    public float connectingTime = 3f;
    public HoseConnectingSlider connectingSlider;

    private NavMeshAgent agent;
    private State currentState = State.Idle;
    private GameObject nearestHydrant;

    public enum State
    {
        Idle,
        MovingToHydrant,
        ConnectingHose,
        MovingToFire,
        Extinguishing
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FirefightingSequence());
    }

    IEnumerator FirefightingSequence()
    {
        Debug.Log("Starting FirefightingSequence");
        
        nearestHydrant = FindNearestHydrant();
        if (nearestHydrant == null)
        {
            Debug.LogError("No available hydrant found!");
            yield break;
        }

        yield return StartCoroutine(MoveNearObject(nearestHydrant));
        ChangeState(State.ConnectingHose);

        yield return StartCoroutine(ConnectHose());

        // �浹 ���� ��� ��ġ ã��
        Vector3 safeStartPosition = FindSafeStartPosition();
        agent.Warp(safeStartPosition);

        Debug.Log("Moving to fire target");
        yield return StartCoroutine(MoveNearObject(fireTarget));

        Debug.Log("Reached fire target. Starting to extinguish.");
        ChangeState(State.Extinguishing);
        // ���⼭ ��ȭ �۾��� ������ �� �ֽ��ϴ�.

        Debug.Log("FirefightingSequence completed");
    }

    GameObject FindNearestHydrant()
    {
        return hydrants
            .OrderBy(h => Vector3.Distance(transform.position, h.transform.position))
            .FirstOrDefault();
    }

    IEnumerator MoveNearObject(GameObject targetObject)
    {
        if (targetObject == null)
        {
            Debug.LogError($"Target object is null. Cannot move.");
            yield break;
        }

        Vector3 targetPosition = GetPositionNearObject(targetObject);
        agent.SetDestination(targetPosition);
        
        if (hydrants.Contains(targetObject))
            ChangeState(State.MovingToHydrant);
        else if (targetObject == fireTarget)
            ChangeState(State.MovingToFire);

        Debug.Log($"Moving towards {targetObject.name} at position {targetPosition}");

        while (true)
        {
            if (agent.pathPending)
            {
                yield return null;
                continue;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log($"Reached near {targetObject.name}");
                    break;
                }
            }

            Debug.Log($"Distance to target: {agent.remainingDistance}");
            yield return null;
        }
    }

    Vector3 GetPositionNearObject(GameObject targetObject)
    {
        Vector3 directionToFirefighter = transform.position - targetObject.transform.position;
        directionToFirefighter.Normalize();
        Vector3 targetPosition = targetObject.transform.position + directionToFirefighter * proximityDistance;
        Debug.Log($"Calculated target position: {targetPosition}");
        return targetPosition;
    }

    IEnumerator ConnectHose()
    {
        Debug.Log("Starting to connect hose. Countdown begins.");
        if (connectingSlider != null)
        {
            connectingSlider.gameObject.SetActive(true);
        }

        float elapsedTime = 0f;
        while (elapsedTime < connectingTime)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log($"Connecting hose: {Mathf.Floor(elapsedTime)} seconds elapsed");
            if (connectingSlider != null)
            {
                connectingSlider.SetProgress(elapsedTime / connectingTime);
            }
            yield return null;
        }

        Debug.Log("Hose connection completed.");
        if (connectingSlider != null)
        {
            connectingSlider.gameObject.SetActive(false);
        }

        // �ҹ���� hydrant���� �ణ ����߸��ϴ�.
        Vector3 awayFromHydrant = transform.position - nearestHydrant.transform.position;
        awayFromHydrant.Normalize();
        transform.position += awayFromHydrant * (agent.radius + 0.1f);
    }

    void ChangeState(State newState)
    {
        currentState = newState;
        Debug.Log($"Changed state to {newState}");
    }

    Vector3 FindSafeStartPosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3 directionToFire = (fireTarget.transform.position - currentPosition).normalized;
        
        for (float distance = 0.5f; distance <= 2f; distance += 0.5f)
        {
            Vector3 testPosition = currentPosition + directionToFire * distance;
            if (!Physics.CheckSphere(testPosition, agent.radius))
            {
                return testPosition;
            }
        }
        
        return currentPosition; // ������ ��ġ�� ã�� ���ϸ� ���� ��ġ ��ȯ
    }
}