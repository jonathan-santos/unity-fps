using UnityEngine;

public class TakeDamageAction : MonoBehaviour
{
    public int life = 3;

    public void TakeDamage(int damage)
    {
        this.life -= damage;
        if (this.life < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
