using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class branch : MonoBehaviour
{
    public Animator animator;
    public GameObject ball;
    public GameObject cup;

    ParticleSystem ps;

    void Awake()
    {
        animator = GetComponent<Animator>();
        ps = transform.parent.GetComponent<ParticleSystem>();
    }
    
    public void FreezeAnimation()
    {
        animator.speed = 0;
        ps.Stop();
    }

    public void StartAnimation()
    {
        animator.speed = 1;
        ps.Play();
    }

    public void SpawnCup()
    {
        cup.SetActive(true);
        
        cup.AddComponent<SpriteMask>();
        cup.GetComponent<SpriteMask>().sprite = cup.GetComponent<SpriteRenderer>().sprite;

    }

    public void SpawnBall()
    {
        ball.SetActive(true);
    }
}