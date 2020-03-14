using UnityEngine;
using UnityEngine.UI;

public class HealthAction : MonoBehaviour
{
    public float maxHealth = 3;
    public float health;
    public bool destroyObjectOnZeroHealth = true;
    public Slider healthBar;

    void Start()
    {
        health = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void ChangeHealth(float change)
    {
        health += change;
        healthBar.value = health;

        if (health < 1 && destroyObjectOnZeroHealth)
        {
            Destroy(this.gameObject);
        }
    }
}
