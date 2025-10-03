using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class interaccion : MonoBehaviour
{
    private int vida;
    public GameObject[] corazon;
    private JugadorController jugadorController;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;
    public event EventHandler Muertejugador;
    public event EventHandler NivelSuperado;
    private Rigidbody2D rb2D;
    public static bool muerteTiempo=false;
    public static bool RespuestaCorrecta=false;
    [SerializeField] private AudioClip sonidoDaño;
    [SerializeField] private AudioClip muerteSonido;
    [SerializeField] private AudioClip sonidoSuperado;

    private void Start()
    {
        vida = corazon.Length;
        rb2D = GetComponent<Rigidbody2D>();
        jugadorController = GetComponent<JugadorController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (muerteTiempo)
        {
            ControladorSonido.Instance.EjecutarSonido(muerteSonido);
            muerteTiempo = false;
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            MuerteJugadorEvento();
        }
        if (RespuestaCorrecta)
        {
            ControladorSonido.Instance.EjecutarSonido(sonidoSuperado);
            RespuestaCorrecta = false;
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            NivelSuperadoEvento();
        }
    }
    public void TomarDaño(int daño,Vector2 posicion)
    {
        vida -= daño;

        if (daño == 3)
        {
            ControladorSonido.Instance.EjecutarSonido(muerteSonido);
            Destroy(corazon[0].gameObject);
            Destroy(corazon[1].gameObject);
            Destroy(corazon[2].gameObject);
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetTrigger("Muerte");
            Physics2D.IgnoreLayerCollision(6, 10, true);
        }

        if (vida < 1)
        {
            ControladorSonido.Instance.EjecutarSonido(muerteSonido);
            Destroy(corazon[0].gameObject);
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetTrigger("Muerte");
            Physics2D.IgnoreLayerCollision(6, 10, true);
    }
        else if (vida < 2)
        {
            ControladorSonido.Instance.EjecutarSonido(sonidoDaño);
            Destroy(corazon[1].gameObject);
            StartCoroutine(PerderControl());
            jugadorController.Rebote(posicion);
            
        }
        else if (vida < 3)
        {
            ControladorSonido.Instance.EjecutarSonido(sonidoDaño);
            Destroy(corazon[2].gameObject);
            StartCoroutine(PerderControl());
            jugadorController.Rebote(posicion);
        }
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
    public void MuerteJugadorEvento()
    {
        Muertejugador?.Invoke(this, EventArgs.Empty);
    }
    public void NivelSuperadoEvento()
    {
        NivelSuperado?.Invoke(this, EventArgs.Empty);
    }
    private IEnumerator PerderControl()
    {
        jugadorController.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        jugadorController.sePuedeMover = true;
    }
   
}
