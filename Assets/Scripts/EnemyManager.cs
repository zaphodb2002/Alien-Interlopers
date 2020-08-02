﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    
    public float score = 0;
    [SerializeField] float baseSpeed = 1f;
    [SerializeField] float speedStep = 0.1f;
    [SerializeField] float fireStep = 0.1f;
    [SerializeField] float fireDelay = 3f;
    [SerializeField] float minFireDelay = 0.5f;
    [SerializeField] int minimumBarricadeWidth = 16; //This includes 2 blocks on either side for padding
    
    [SerializeField] Transform[] enemyPrefabs = null;

    [SerializeField] Transform barricadeBlockPrefab;


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
        timeSinceLastShot = fireDelay;
    }
    private void Start()
    {
        CreateBarricades(3);
    }

    private void Update()
    {
        if (runEnemyMove)
        {
            if (enemies.Count == 0)
            {
                speed = baseSpeed + (wavesActivated * speedStep);
                if (fireDelay >= minFireDelay)
                {
                    fireDelay -= fireStep;
                }
                
                Player.instance.LevelUp();
                CreateWave(3);
                wavesActivated++;
                waveUi.text = wavesActivated.ToString();
            }
            else
            {
                scoreUi.text = score.ToString();
                MoveEnemies(speed);
                HandleWeaponsFire();
            }
        }

    }

    Transform waveParent = null;

    private void CreateWave(int depth)
    {
        if (depth <= 0)
        {
            depth = 1;
        }

        if (depth > 3)
        {
            depth = 3;
        }

        int numOfEnemiesPerRow = Mathf.FloorToInt(GameManager.instance.HalfScreenWidthInUnits);
        int initX = -Mathf.FloorToInt(GameManager.instance.HalfScreenWidthInUnits);
        int initY = Mathf.FloorToInt(GameManager.instance.ScreenHeightInUnits) - depth - 1;  // extra 1 is to clear the UI stuff
        Vector2 location = new Vector2(initX, initY);

        waveParent = new GameObject("Wave Parent").transform;
        waveParent.position = new Vector2((initX / 2f) - 0.5f, initY);
        for (int j = 0; j < depth; j++)
        {
            for (int i = 0; i < numOfEnemiesPerRow; i++)
            {
                Transform newEnemy = Instantiate(enemyPrefabs[j]);
                enemies.Add(newEnemy.GetComponent<Enemy>());
                newEnemy.position = location;
                location.x++;
                newEnemy.parent = waveParent;
            }
            location.x = initX;
            location.y++;
        }
        
    }

    bool isGoingRight = true;
    private void MoveEnemies(float speed)
    {
        if (enemies.Count > 0)
        {
            Vector3 move = isGoingRight ? Vector3.right : Vector3.left;
            
            if (Mathf.Abs(waveParent.position.x) > (GameManager.instance.HalfScreenWidthInUnits / 2f) + 0.5f)
            {
                waveParent.position += Vector3.down;
                waveParent.position += isGoingRight ? Vector3.left * 0.5f : Vector3.right * 0.5f;

                isGoingRight = !isGoingRight;
            }
            
            waveParent.position += move * speed * Time.deltaTime;

            if (waveParent.position.y <= 0)
            {
                DoPlayerDeath();
            }

        }
    }

    float timeSinceLastShot = 0f;
    private void HandleWeaponsFire()
    {
        if (timeSinceLastShot >= fireDelay)
        {
            enemies[Random.Range(0, enemies.Count - 1)].Shoot();
            timeSinceLastShot -= fireDelay;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    public void DoPlayerDeath()
    {
        runEnemyMove = false;
        GameManager.instance.LoadGameOver();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    private void CreateBarricades(int thickness)
    {
        int screenWidthInBarricadeBlocks = Mathf.FloorToInt(GameManager.instance.ScreenWidthInUnits) * 4; // 4 because each barricade block is 0.25f;

        int numberOfBarricades = screenWidthInBarricadeBlocks / minimumBarricadeWidth;

        Vector3 location = new Vector3(-Mathf.FloorToInt(GameManager.instance.HalfScreenWidthInUnits), 1.25f);

        for (int i = 0; i < numberOfBarricades; i++)
        {
            for (int block = 0; block < minimumBarricadeWidth; block++)
            {
                if (block > 1 && block < minimumBarricadeWidth - 2)
                {
                    for (int row = 0; row < thickness; row++)
                    {
                        var newBlock = Instantiate(barricadeBlockPrefab);
                        newBlock.position = location;
                        location.y += 0.25f;
                    }
                    location.y -= 0.25f * thickness;
                }
                
                location.x += 0.25f;
            }
        }
    }
}
