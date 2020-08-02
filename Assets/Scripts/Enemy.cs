using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isGoingRight = false;

    [SerializeField] int scoreValue = 10;
    [SerializeField] Shot shotPrefab = null;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip deathSound;

    public Vector3 spawnMoveDirection = Vector3.zero;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (spawnMoveDirection != Vector3.zero)
        {
            MoveAcrossScreen(spawnMoveDirection);
        }
    }

    public void DoMove(float speed)
    {
        speed = speed * Time.deltaTime;
        if (transform.position.y <= 0)
        {
            EnemyManager.instance.DoPlayerDeath();
        }

        if (isGoingRight)
        {
            if (transform.position.x < Mathf.Floor(GameManager.instance.HalfScreenWidthInUnits))
            {
                transform.position += Vector3.right * speed;
            }
            else
            {
                transform.position += Vector3.down;
                transform.position += Vector3.left * speed;
                isGoingRight = false;
            }
        }
        else
        {
            if (transform.position.x > -Mathf.Floor(GameManager.instance.HalfScreenWidthInUnits))
            {
                transform.position += Vector3.left * speed;
            }
            else
            {
                transform.position += Vector3.down;
                transform.position += Vector3.right * speed;
                isGoingRight = true;
            }
        }
    }

    private void MoveAcrossScreen(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime;
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.HalfScreenWidthInUnits + 2)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        var shot = Instantiate(shotPrefab);
        shot.transform.position = transform.position;
        audioSource.PlayOneShot(laserSound);
        shot.Initialize(Vector3.down, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Projectile" && !collision.gameObject.GetComponent<Shot>().isEnemyShot)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 100f);
            EnemyManager.instance.score += scoreValue;
            EnemyManager.instance.RemoveEnemy(this);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
