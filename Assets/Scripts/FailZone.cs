using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Fail Trigger: {collision.tag}");
        if (collision.tag == "Enemy")
        {
            EnemyManager.instance.DoPlayerDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Fail Collider: {collision.gameObject.tag}");
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyManager.instance.DoPlayerDeath();
        }
    }
}
