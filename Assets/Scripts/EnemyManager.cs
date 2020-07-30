using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public float speed;
    [SerializeField] float baseSpeed = 1f;

    [SerializeField] float step = 0.1f;
    [SerializeField] Transform enemyPrefab = null;    

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
                CreateWave();
                wavesActivated++;
            }
            else
            {
                MoveEnemies(speed);
            }
        }

    }


    private void CreateWave()
    {
        int numOfEnemiesToSpawn = Random.Range(3, 10);
        Vector2 location = new Vector2(-Mathf.Floor(GameManager.instance.HalfScreenWidthInUnits), Mathf.Floor(GameManager.instance.ScreenHeightInUnits));
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
