using UnityEngine;

public class HordesAction : MonoBehaviour
{
    public SpawnerAction enemiesSpawn;

    public int[] ammountOfEnemiesInHorde = { 5, 10, 15 };
    public int timeBetweenHordes = 10;
    public bool isInHorde = false;

    int currentHorde = 0;

    void Start()
    {
        Invoke("StartHorde", timeBetweenHordes / 2);
    }

    void StartHorde()
    {
        isInHorde = true;
        Debug.Log("currentHorde: " + currentHorde);
        enemiesSpawn.quantityMinimum = ammountOfEnemiesInHorde[currentHorde];
        enemiesSpawn.quantityMaximum = ammountOfEnemiesInHorde[currentHorde];
        enemiesSpawn.StartSpawn();
    }

    void Update()
    {
        var enemiesQuantity = GameObject.FindGameObjectsWithTag("enemy").Length;
        if(enemiesQuantity == 0 && this.isInHorde)
        {
            this.currentHorde += 1;
            isInHorde = false;
            Invoke("StartHorde", timeBetweenHordes);
        }

    }
}
