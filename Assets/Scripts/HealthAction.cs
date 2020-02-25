using UnityEngine;
using UnityEngine.UI;

public class HealthAction : MonoBehaviour
{
    public int maxHealth = 3;
    public int health;
    public bool destroyObjectOnZeroHealth = true;
    public Slider healthBar;

    void Start()
    {
        health = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void ChangeHealth(int change)
    {
        health += change;
        healthBar.value = health;

        if (health < 1 && destroyObjectOnZeroHealth)
        {
            Destroy(this.gameObject);
        }
    }
}
