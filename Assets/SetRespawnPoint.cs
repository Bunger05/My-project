using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawnPoint : MonoBehaviour
{
    public Vector2 location;
    public bool thisRespawn;
    private void Awake()
    {
        location = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        thisRespawn = true;
    }
}
