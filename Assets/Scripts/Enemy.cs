using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isGoingRight = false;

    [SerializeField] int scoreValue = 10;
    [SerializeField] Shot shotPrefab = null;

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

    public void Shoot()
    {
        var shot = Instantiate(shotPrefab);
        shot.transform.position = transform.position;
        shot.Initialize(Vector3.down, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Projectile" && !collision.gameObject.GetComponent<Shot>().isEnemyShot)
        {
            EnemyManager.instance.score += scoreValue;
            EnemyManager.instance.RemoveEnemy(this);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
