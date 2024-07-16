using UnityEngine;

public class Hydrant : MonoBehaviour
{
    public int maxUsers = 4;
    private int currentUsers = 0;

    public bool CanUse()
    {
        return currentUsers < maxUsers;
    }

    public bool ConnectFirefighter()
    {
        if (CanUse())
        {
            currentUsers++;
            return true;
        }
        return false;
    }

    public void DisconnectFirefighter()
    {
        if (currentUsers > 0)
        {
            currentUsers--;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f); // 1f는 연결 가능 거리입니다. 필요에 따라 조정하세요.
    }
}