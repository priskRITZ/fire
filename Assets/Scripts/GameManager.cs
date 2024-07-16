using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

public class GameManager : MonoBehaviour
{
    public int startingFunds = 1000;
    public int currentFunds;
    public TMP_Text fundsText; // TMP_Text 타입 사용

    void Start()
    {
        currentFunds = startingFunds;
        UpdateFundsUI();
    }

    public void SpendFunds(int amount)
    {
        if (currentFunds >= amount)
        {
            currentFunds -= amount;
            UpdateFundsUI();
        }
    }

    void UpdateFundsUI()
    {
        fundsText.text = "Funds: " + currentFunds.ToString();
    }
}
