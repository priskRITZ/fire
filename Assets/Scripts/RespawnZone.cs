using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public Transform respawnPoint;

    void Start()
    {
        respawnPoint = this.transform;
    }
}