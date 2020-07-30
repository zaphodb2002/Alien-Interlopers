using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y > GameManager.instance.ScreenHeightInUnits - 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
