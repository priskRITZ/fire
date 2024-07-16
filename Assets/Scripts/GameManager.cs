using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class GameManager : MonoBehaviour
{
    public int startingFunds = 1000;
    public int currentFunds;
    public TMP_Text fundsText; // TMP_Text Ÿ�� ���

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
