using TMPro;
using UnityEngine;

public class HordesAction : MonoBehaviour
{
    [Header("Imports")]
    public SpawnerAction enemiesSpawner;
    public TextMeshProUGUI enemiesCounter;
    public CountdownTimerAction countdownTimer;

    [Header("Configuration")]
    public int[] ammountOfEnemiesInHorde = { 5, 10, 15 };
    public int secondsToStart = 5;
    public int timeBetweenHordes = 10;
    public bool isInHorde = false;

    int currentHorde = 0;

    void Start()
    {
        Invoke("StartHorde", this.secondsToStart);
        countdownTimer.StartTimer(this.secondsToStart);
    }

    void StartHorde()
    {
        isInHorde = true;
        enemiesSpawner.quantityMinimum = ammountOfEnemiesInHorde[currentHorde];
        enemiesSpawner.quantityMaximum = ammountOfEnemiesInHorde[currentHorde];
        enemiesSpawner.StartSpawn();
    }

    void Update()
    {
        var enemiesQuantity = GameObject.FindGameObjectsWithTag("enemy").Length;
        enemiesCounter.text = $"{enemiesQuantity}x";

        if(enemiesQuantity == 0 && this.isInHorde && enemiesSpawner.finishedSpawning)
        {
            this.currentHorde += 1;
            this.isInHorde = false;
            Invoke("StartHorde", timeBetweenHordes);
            countdownTimer.StartTimer(timeBetweenHordes);
        }

    }
}
