using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    public int life = 3;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("player");
        Debug.Log(player);
        var agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
        transform.LookAt(player.transform.position, Vector3.up);
    }

    public void TakeDamage(int damage)
    {
        this.life -= damage;
        if (this.life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}