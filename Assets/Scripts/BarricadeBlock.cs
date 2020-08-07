using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeBlock : MonoBehaviour
{
    [SerializeField] Explosion explosion;
    [SerializeField] AudioClip explosionSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile" || collision.tag == "Enemy")
        {
            if (collision.tag == "Projectile")
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
