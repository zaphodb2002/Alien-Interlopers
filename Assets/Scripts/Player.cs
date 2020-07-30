using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    [SerializeField] float playerSpeed = 3f;
    [SerializeField] float fireDelay = 0.5f;

    [SerializeField] int level = 0;
    [SerializeField] float speedStep = 0.5f;
    [SerializeField] float fireDelayStep = 0.05f;

    [SerializeField] Transform shotPrefab = null;

    private float horiz = 0f;
    private float timeSinceLastWeaponFire = 0f;
    [SerializeField] float currentSpeed = 0f;

    private void Awake()
    {
        if(GameObject.FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        currentSpeed = playerSpeed;

    }

    private void Update()
    {
        horiz = Input.GetAxisRaw("Horizontal");
        if (horiz != 0f)
        {
            if (transform.position.x >= GameManager.instance.HalfScreenWidthInUnits - 0.5f && horiz > 0f)
            {
                horiz = 0f;
            }
            if (transform.position.x <= -(GameManager.instance.HalfScreenWidthInUnits - 0.5f) && horiz < 0f)
            {
                horiz = 0f;
            }

            transform.position += (Vector3.right * horiz) * Time.deltaTime * (currentSpeed);
        }

        if (timeSinceLastWeaponFire > 0f)
        {
            timeSinceLastWeaponFire -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                FireWeapon();
            }
        } 
    }

    private void FireWeapon()
    {
        Instantiate(shotPrefab).position = transform.position;
        timeSinceLastWeaponFire += fireDelay;
    }

    public void LevelUp()
    {
        level++;
        currentSpeed = playerSpeed + (level * speedStep);
    }
}
