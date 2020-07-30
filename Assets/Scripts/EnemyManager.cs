using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    
    public float score = 0;
    [SerializeField] float baseSpeed = 1f;
    

    [SerializeField] float step = 0.1f;
    [SerializeField] Transform enemyPrefab = null;
    [SerializeField] TextMeshProUGUI scoreUi;
    [SerializeField] TextMeshProUGUI waveUi;

    private float speed;
    private List<Enemy> enemies;
    private int wavesActivated = 0;
    private bool runEnemyMove = true;

    private void Awake()
    {
        if (GameObject.FindObjectsOfType<EnemyManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        enemies = new List<Enemy>();
        speed = baseSpeed;
    }

    private void Update()
    {
        if (runEnemyMove)
        {
            if (enemies.Count == 0)
            {
                speed = baseSpeed + (wavesActivated * step);
                Player.instance.LevelUp();
                CreateWave(1);
                wavesActivated++;
                waveUi.text = wavesActivated.ToString();
            }
            else
            {
                scoreUi.text = score.ToString();
                MoveEnemies(speed);
            }
        }

    }


    private void CreateWave(int depth)
    {
        if (depth <= 0)
        {
            depth = 1;
        }

        if (depth > GameManager.instance.ScreenHeightInUnits - 3)
        {
            depth = Mathf.FloorToInt(GameManager.instance.ScreenHeightInUnits) - 3;
        }

        int numOfEnemiesToSpawn = Mathf.FloorToInt(GameManager.instance.HalfScreenWidthInUnits);
        Vector2 location = new Vector2(-Mathf.Floor(GameManager.instance.HalfScreenWidthInUnits), Mathf.Floor(GameManager.instance.ScreenHeightInUnits) - 2); //To Clear the score bar
        for (int i = 0; i < numOfEnemiesToSpawn; i++)
        {
            Transform newEnemy = Instantiate(enemyPrefab);
            enemies.Add(newEnemy.GetComponent<Enemy>());
            newEnemy.position = location;
            location.x++;
        }
    }

    private void MoveEnemies(float speed)
    {
        if (enemies.Count > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.DoMove(speed);
            }
        }
    }

    public void DoPlayerDeath()
    {
        runEnemyMove = false;
        GameManager.instance.LoadStartMenu();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
