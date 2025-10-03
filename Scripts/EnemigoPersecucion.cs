using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPersecucion : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distanciaDeteccion;  // Distancia a la que el enemigo detecta al jugador

    private Transform jugador;
    private SpriteRenderer spriteRenderer;
    private bool jugadorEstaMirando;

    private void Start()
    {
        // Encuentra el objeto del jugador
        GameObject jugadorObjeto = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObjeto != null)
        {
            jugador = jugadorObjeto.transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Verifica si el jugador aún existe
        if (jugador == null)
        {
            return; // Sal de Update si el jugador ha sido destruido
        }

        // Calcula la dirección hacia el jugador
        Vector2 direccionHaciaJugador = jugador.position - transform.position;
        float distanciaHaciaJugador = direccionHaciaJugador.magnitude;

        // Determina si el jugador está mirando al enemigo
        jugadorEstaMirando = MirandoAlJugador(direccionHaciaJugador);

        // Si el jugador no está mirando y está dentro del rango, el enemigo se mueve hacia el jugador
        if (!jugadorEstaMirando && distanciaHaciaJugador <= distanciaDeteccion)
        {
            transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidadMovimiento * Time.deltaTime);
            Girar();
        }
    }

    private bool MirandoAlJugador(Vector2 direccionHaciaJugador)
    {
        // Determina la dirección en la que el jugador está mirando
        Vector3 direccionJugador = jugador.localScale.x > 0 ? Vector2.right : Vector2.left;
        return Vector2.Dot(direccionHaciaJugador.normalized, direccionJugador) < 0;
    }

    private void Girar()
    {
        // Si el enemigo está a la izquierda del jugador, mira hacia la derecha; si no, hacia la izquierda
        if (transform.position.x < jugador.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) // Daño para el jugador.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<interaccion>().TomarDaño(1, other.GetContact(0).normal);
        }
    }
}
