using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapelBala : MonoBehaviour
{
    [SerializeField] private float velocidad;

    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)//daño para el jugador.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<interaccion>().TomarDaño(1, other.GetContact(0).normal);
            Destroy(gameObject);
        }
    }
}
