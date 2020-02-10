using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float force = 10f;
    public int duration = 3;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
