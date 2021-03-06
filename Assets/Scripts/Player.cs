﻿using System;
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

    [SerializeField] Shot shotPrefab = null;

    private float horiz = 0f;
    private float timeSinceLastWeaponFire = 0f;
    [SerializeField] float currentSpeed = 0f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip deathSound;

    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserSound;

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
        Shot shot = Instantiate(shotPrefab);
        shot.transform.position = transform.position;
        shot.Initialize(Vector3.up);
        audioSource.PlayOneShot(laserSound);
        timeSinceLastWeaponFire += fireDelay;
    }

    public void LevelUp()
    {
        level++;
        currentSpeed = playerSpeed + (level * speedStep);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile" && collision.gameObject.GetComponent<Shot>().isEnemyShot)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            EnemyManager.instance.ExplodeAt(transform.position);
            EnemyManager.instance.DoPlayerDeath();
            gameObject.SetActive(false);
        }
    }
}
