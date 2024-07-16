using UnityEngine;
using UnityEngine.UI;

public class ConnectionGauge : MonoBehaviour
{
    public Image fillImage;

    private void Start()
    {
        // �׻� ī�޶� ���ϵ��� ����
        transform.forward = Camera.main.transform.forward;
    }

    public void UpdateGauge(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
    }

    private void Update()
    {
        // ���������� ī�޶� ���ϵ��� ������Ʈ
        transform.forward = Camera.main.transform.forward;
    }
}