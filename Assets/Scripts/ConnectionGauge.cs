using UnityEngine;
using UnityEngine.UI;

public class ConnectionGauge : MonoBehaviour
{
    public Image fillImage;

    private void Start()
    {
        // 항상 카메라를 향하도록 설정
        transform.forward = Camera.main.transform.forward;
    }

    public void UpdateGauge(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
    }

    private void Update()
    {
        // 지속적으로 카메라를 향하도록 업데이트
        transform.forward = Camera.main.transform.forward;
    }
}