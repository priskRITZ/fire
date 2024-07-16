using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int initialResources = 1000;
    private int currentResources;

    private void Start()
    {
        currentResources = initialResources;
    }

    public bool SpendResources(int amount)
    {
        if (currentResources >= amount)
        {
            currentResources -= amount;
            return true;
        }
        return false;
    }

    public int GetCurrentResources()
    {
        return currentResources;
    }
}