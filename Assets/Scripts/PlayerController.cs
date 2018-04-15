﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public GameObject gm;
    GameMaster gmScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gmScript = gm.GetComponent<GameMaster>();
    }

    void FixedUpdate()
    {
        if (!gmScript.IsGameOver())
        {
            rb.velocity = new Vector3(0, rb.velocity.y, speed);

            rb.AddForce(Vector3.down * 100f);

            if (this.transform.position.y < -3)
            {
                Debug.Log("Killed by falling");
                gmScript.SetGameOver(true);
            }

        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Killed by " + hit.gameObject.tag);
            gmScript.SetGameOver(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            gmScript.IncreamentScore(5);
            Destroy(other.gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return this.speed;
    }
}