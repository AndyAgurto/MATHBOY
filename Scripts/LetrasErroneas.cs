using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetrasErroneas : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<interaccion>().TomarDaño(3, other.GetContact(0).normal);
            Destroy(gameObject);
        }
    }
}
