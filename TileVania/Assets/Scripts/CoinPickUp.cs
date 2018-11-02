﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour {
    [SerializeField] AudioClip SFX;
    [SerializeField] [Range(0, 1)] float volume = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(SFX, Camera.main.transform.position, volume);
        Destroy(gameObject);
    }
}
