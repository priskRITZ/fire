using UnityEngine;
using TMPro; // TextMeshPro 사용

public class BuildingHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public ParticleSystem fireEffect; // 불 효과 파티클 시스템
    public TMP_Text healthText; // HP 텍스트 UI

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
        // 추가적인 불 끄는 효과 구현
    }
}