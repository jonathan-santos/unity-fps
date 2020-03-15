using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("GoToPlayer", 0, 1);
    }

    void GoToPlayer() {
        if(agent.enabled)
            agent.destination = player.transform.position;
    }

    void LateUpdate()
    {
        transform.LookAt(player.transform.position, Vector3.up);
    }
}