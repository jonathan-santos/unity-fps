using UnityEngine;

public class SpawnerAction : MonoBehaviour
{
    [Header("Spawn")]
    public float spawnInterval;
    public GameObject[] objects;

    [Header("Quantity")]
    public int quantityMinimum;
    public int quantityMaximum;

    [Header("Distance from Spawn")]
    public float minDistanceFromSpawn = 10;
    public float maxDistanceFromSpawn = 30;

    GameObject randomObject;
    int maxcount;
    int count = 0;

    void Start() {
        StartSpawn();
    }

    void StartSpawn()
    {
        count = 0;
        maxcount = Random.Range(quantityMinimum, quantityMaximum);
        if (spawnInterval == 0)
        {
            for (int i = 0; i < maxcount; i++)
            {
                Spawn();
            }
        }
        else
        {
            InvokeRepeating("Spawn", 0, spawnInterval);
        }
    }

    void Update()
    {
        if (count > maxcount)
        {
            CancelInvoke();
        }
    }

    void Spawn()
    {
        int index = Random.Range(0, objects.Length);
        randomObject = Instantiate(objects[index]);

        randomObject.transform.parent = transform;

        randomObject.transform.localPosition = new Vector3(
            Random.Range((transform.position.x + minDistanceFromSpawn) - maxDistanceFromSpawn, transform.position.x + maxDistanceFromSpawn),
            0,
            Random.Range((transform.position.z + minDistanceFromSpawn) - maxDistanceFromSpawn, transform.position.z + maxDistanceFromSpawn)
        );
        count++;
    }
}
