using UnityEngine;
using TMPro; // TextMeshPro ���

public class BuildingHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public ParticleSystem fireEffect; // �� ȿ�� ��ƼŬ �ý���
    public TMP_Text healthText; // HP �ؽ�Ʈ UI

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            ExtinguishFire();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth.ToString();
        }
    }

    void ExtinguishFire()
    {
        if (fireEffect != null)
        {
            fireEffect.Stop();
        }
        // �߰����� �� ���� ȿ�� ����
    }
}