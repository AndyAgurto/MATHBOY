using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JugadorController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public bool sePuedeMover = true;
    public bool golpe=false;
    [SerializeField] private Vector2 velocidadRebote;

    private Vector2 input;

 
    [Header("Movimiento")]// se declara las variables a utilizar en movimiento horizontal 
    private float MovH=0f;
    [SerializeField] private float VelocidadMov;//velocidad de recorrido 
    [Range(0, 0.3f)][SerializeField] private float SuavizadoMov;//se le da un suavizado de movimiento en un rango de 0-0.3
    private Vector3 velocidad = Vector3.zero;// vector 3D de velocidad en 0
    public AudioSource sonidoMovimiento;
    private bool mirandoDerecha = true;

    [Header("Salto")]// se declara las variables a utilizar en el salto
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;// se declara un tipo etiqueta para detectar que es el suelo.
    [SerializeField] private Transform controladorSuelo;//se declara de tipo transform lo que permite usar sus propiedades como puntos de posicion,rotacion,escala.
    [SerializeField] private Vector3 dimensionesCaja;// se declara un objeto imaginario de la cajaque se encuentra debajo del personaje. 
    [SerializeField] private bool enSuelo;
    [SerializeField] private AudioClip saltoSonido;
    private bool salto = false;

    [Header("Salto Regulable")]
    [Range(0, 1)] [SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSaltoArriba = true;

    [Header("Escalar")]
    [SerializeField] private float velocidadEscalar;
    private BoxCollider2D boxCollider2D;
    private float gravedadInicial;
    private bool escalando;
    public AudioSource escalarSonido;

    [Header("Animacion")]
    private Animator animator;// se declara las animaciones a utilizar 


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        escalaGravedad = rb2D.gravityScale;
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravedadInicial = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        MovH = input.x * VelocidadMov;

        animator.SetFloat("Horizontal", Mathf.Abs(MovH));
        animator.SetFloat("VelocidadY",rb2D.velocity.y);

        if (Input.GetButtonDown("Horizontal"))
        {
            sonidoMovimiento.Play();
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            sonidoMovimiento.Pause();
        }
        if (Input.GetButtonDown("Vertical"))
        {
            sonidoMovimiento.Pause();
            escalarSonido.Play();
        }
        if (Input.GetButtonUp("Vertical"))
        {
            escalarSonido.Pause();
        }

        if (rb2D.velocity.y < 0 && !enSuelo)
        {
            rb2D.gravityScale = escalaGravedad * multiplicadorGravedad;
        }
        else
        {
            rb2D.gravityScale = escalaGravedad;
        }

        if (Mathf.Abs(rb2D.velocity.y) > Mathf.Epsilon) //cambia en valores pequeños que no es cero 
        {
            animator.SetFloat("VelocidadY", Mathf.Sign(rb2D.velocity.y));
        }
        else
        {
            animator.SetFloat("VelocidadY", 0);
        }

        if (Input.GetButton("Salto"))
        {
            sonidoMovimiento.Pause();
            escalarSonido.Pause();
            if (input.y >= 0)
            {
                salto = true;
            }
            else
            {
                descativarPlataformas();
            }
        }
        if (Input.GetButtonUp("Salto"))
        {
            BotonSaltoArriba();
        }
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);//utiliza parametros de un vector2(en este caso controlador)
                                                                                                   //como posicion,el tamaño del vector2 como la dimensiones de la caja del
                                                                                                   //controladorSuelo, un angulo que seria en este caso 0 y finalmente 
                                                                                                   //una etiqueta queEsSuelo que vendria ser la losa como referencia.
      
        animator.SetBool("EnSuelo", enSuelo);
    }
 
    private void FixedUpdate()
    {
     if (sePuedeMover)
        {
            //Movimiento
            golpe = false;
            animator.SetBool("Golpe", golpe);
            movimiento(MovH * Time.fixedDeltaTime);
            movimientoSalto(salto);
        }

        Escalar();
        salto = false;

    }
    private void movimiento(float mov)
    {
        
        Vector3 velocidadObjetivo = new Vector2(mov, rb2D.velocity.y);//se crea un vector 2D que indique la posicion que tendra que alcanzar tendra sumado al movimiento.
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, SuavizadoMov);//en el suavizado sera la velocidad del cuerpo rigido 2D 
                                                                                                          //igual a una funcion de amortiguacion de resorte lo cual
                                                                                                          //no se excede
                                                                                                          //sus parametros son la velocidad del cuerpo rigido 2d,
                                                                                                          //la velocidad de objetivo, como referencia la velocidad actual,
                                                                                                          //la velocidad de aproximacion que tendra de suavizado.
        if (mov > 0 && !mirandoDerecha)
        {
            //girar
            girar();
        }
        else if (mov < 0 && mirandoDerecha)
        {
            //girar
            girar();
        }
    }
    private void movimientoSalto(bool saltar)
    {
        if (enSuelo && saltar && botonSaltoArriba)
        {
            ControladorSonido.Instance.EjecutarSonido(saltoSonido);
            rb2D.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);//esta funcion se usa para acelerar un cuerpo
                                                                           //rigido segun el parametro dirigido en este caso 0 en X, fuerzadel salto en Y
            enSuelo = false;
            //rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
            saltar = false;
            botonSaltoArriba = false;
        }
    }
  
    private void BotonSaltoArriba()
    {
        if (rb2D.velocity.y > 0)
        {
            rb2D.AddForce(Vector2.down * rb2D.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
        }
        botonSaltoArriba = true;
        salto = false;
    }
    private void Escalar()
    {
        if ((input.y != 0 || escalando) && (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Escalera"))))
        {
            Vector2 velocidadSubida = new Vector2(rb2D.velocity.x, input.y * velocidadEscalar);
            rb2D.velocity = velocidadSubida;
            rb2D.gravityScale = 0;
            escalando = true;
            enSuelo = false;
        }
        else
        {
            rb2D.gravityScale = gravedadInicial;
            escalando = false;
        }
        if (enSuelo)
        {
            escalando = false;
        }
        animator.SetBool("estaEscalando", escalando);
        
    }
    private void girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;// se multiplica por -1 desde el punto x que esta 
        transform.localScale = escala;
    }

    public void Rebote(Vector2 puntoGolpe)
    {
        golpe = true;
        animator.SetBool("Golpe", golpe);
        rb2D.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);

    }
    private void descativarPlataformas()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);//devuelve un arreglo donde detecta que suelo es del
                                                                                                                   //mismo componente a la etiqueta dada
        foreach (Collider2D item in objetos)//se recorre todos los objetos que toca 
        {
            PlatformEffector2D platformEffector2D = item.GetComponent<PlatformEffector2D>();//buscamos el componente dado en la plataforma 
            if (platformEffector2D != null)//para saber si tiene este componente se pregunta si es diferente de nulo
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);//se usa para ignorar colisiones, primero del jugador y
                                                                                                             //el segundo del onjeto que toca
                                                                                                             //true para desactivar la colision de los objetos.
            }
        }
    }
   
    private void OnDrawGizmosSelected()//funciones de vizualizacion de la caja o gizmos 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
