using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public GameObject Libreta;
    public GameObject Dialogo;
    public GameObject botonPausa;
    private interaccion interaccion;
    private void Start()
    {
        interaccion = GameObject.FindGameObjectWithTag("Player").GetComponent<interaccion>();
        interaccion.Muertejugador += AbrirMenu;
    }

    private void AbrirMenu(object sender,EventArgs e)
    {
        menu.SetActive(true);
        Libreta.SetActive(false);
        Dialogo.SetActive(false);
        botonPausa.SetActive(false);
    }
    public void Reiniciar()
    {
        Physics2D.IgnoreLayerCollision(6, 10, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
