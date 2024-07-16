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
        // �����̴��� �׻� ī�޶� ���ϵ��� �մϴ�
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}