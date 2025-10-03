using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaTemporal : MonoBehaviour
{
    [SerializeField] private float tiempoDeBala;
    private void Start()
    {
        Destroy(gameObject, tiempoDeBala);
    }
}
