using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    [SerializeField] float speed = 3f;
    public bool isEnemyShot = false;

    private Vector3 direction = Vector3.zero;

    private void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        
        if (transform.position.y > GameManager.instance.ScreenHeightInUnits - 0.5f || transform.position.y < -0.5f)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 dir, bool isEnemy = false)
    {
        this.direction = dir;
        this.isEnemyShot = isEnemy;
    }
}
