using UnityEngine;
using UnityEngine.UI;

public class HoseConnectingSlider : MonoBehaviour
{
    public Slider slider;

    public void SetProgress(float progress)
    {
        slider.value = progress;
    }

    private void Update()
    {
        // 슬라이더가 항상 카메라를 향하도록 합니다
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}