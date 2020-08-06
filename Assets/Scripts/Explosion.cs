using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Explosion : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Animator animator;

    [SerializeField] float playbackSpeed = 1f;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.speed = playbackSpeed;
        animator.Play("Explosion4");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length / playbackSpeed);
    }
}
