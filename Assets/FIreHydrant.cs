using UnityEngine;

public class FireHydrant : MonoBehaviour
{
    public int maxFirefighters = 4;
    private int currentFirefighters = 0;

    public bool IsAvailable()
    {
        return currentFirefighters < maxFirefighters;
    }

    public void AddFirefighter()
    {
        if (IsAvailable())
        {
            currentFirefighters++;
        }
    }

    public void RemoveFirefighter()
    {
        if (currentFirefighters > 0)
        {
            currentFirefighters--;
        }
    }
}