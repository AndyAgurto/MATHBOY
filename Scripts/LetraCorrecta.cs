using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetraCorrecta : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interaccion.RespuestaCorrecta = true;
            Destroy(gameObject);
        }
    }
}
