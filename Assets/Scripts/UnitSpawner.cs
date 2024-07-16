using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject firefighterPrefab;
    public Transform respawnPoint;
    public GameManager gameManager;
    public Transform targetBuilding;

    public float spawnRadius = 2.0f;
    public int maxAttempts = 10;

    public void SpawnFirefighter()
    {
        if (gameManager.currentFunds >= 100)
        {
            Vector3 spawnPosition;
            if (TryGetValidSpawnPosition(out spawnPosition))
            {
                GameObject firefighter = Instantiate(firefighterPrefab, spawnPosition, Quaternion.identity);
                FirefighterMovement movement = firefighter.GetComponent<FirefighterMovement>();
                movement.targetBuilding = targetBuilding; // targetBuilding วาด็
                gameManager.SpendFunds(100);
            }
            else
            {
                Debug.LogError("Could not find a valid spawn position for firefighter.");
            }
        }
    }

    private bool TryGetValidSpawnPosition(out Vector3 spawnPosition)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = respawnPoint.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = respawnPoint.position.y;

            Collider[] colliders = Physics.OverlapSphere(randomPosition, 0.5f);
            if (colliders.Length == 0)
            {
                spawnPosition = randomPosition;
                return true;
            }
        }

        spawnPosition = Vector3.zero;
        return false;
    }
}