using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    private void onTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), false);
        }
    }
}
