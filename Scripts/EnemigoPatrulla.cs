using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimientos;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimientos.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
        disparar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimientos[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMovimientos[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimientos.Length);
            Girar();
            disparar();
        }
    }
    private void Girar()
    {
        if (transform.position.x < puntosMovimientos[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)//daño para el jugador.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<interaccion>().TomarDaño(3,other.GetContact(0).normal);
        }
    }

    private void disparar()
    {
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
    }
}