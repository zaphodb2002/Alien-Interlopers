using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile" || collision.tag == "Enemy")
        {
            if (collision.tag == "Projectile")
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
