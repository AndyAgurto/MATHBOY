using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverS : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public GameObject Libreta;
    public GameObject Dialogo;
    public GameObject botonPausa;
    private interaccion interaccion;
    private float tempo =65f;  // Referencia al script Tiempo

    private int puntajeNivel;
    public static int puntajeTotal;
    public static int nivelesSuperados;
    public static List<float> tiemposNiveles = new List<float>(); // Almacena los tiempos de cada nivel
    public static List<int> puntajesNiveles = new List<int>();    // Almacena las calificaciones de cada nivel

    private void Start()
    {
        interaccion = GameObject.FindGameObjectWithTag("Player").GetComponent<interaccion>();
        interaccion.NivelSuperado += AbrirMenu;
    }

    private void Update()
    {
        tempo -= Time.deltaTime;
    }

    private void AbrirMenu(object sender, EventArgs e)
    {
        Time.timeScale = 0;  // Pausar el tiempo
        menu.SetActive(true);
        Libreta.SetActive(false);
        Dialogo.SetActive(false);
        botonPausa.SetActive(false);

        float tiempoNivelActual = tempo; // Obtener el tiempo del nivel actual

        // Calcular el puntaje con el tiempo obtenido
        CalcularPuntaje(tiempoNivelActual);

            // Actualizar los puntajes y tiempos
            puntajeTotal += puntajeNivel;
            nivelesSuperados++;
            tiemposNiveles.Add(tiempoNivelActual);
            puntajesNiveles.Add(puntajeNivel);

            // Llamar a GuardarRanking después de calcular los puntajes
            GuardarRanking();

    }

    // Ahora la función CalcularPuntaje toma un parámetro: tiempoNivelActual
    private void CalcularPuntaje(float tiempoNivelActual)
    {
        // Calcular el puntaje con base en el tiempo
        if (tiempoNivelActual <= 60 && tiempoNivelActual > 45)
            puntajeNivel = 20;
        else if (tiempoNivelActual <= 45 && tiempoNivelActual > 30)
            puntajeNivel = 16;
        else if (tiempoNivelActual <= 30 && tiempoNivelActual > 15)
            puntajeNivel = 13;
        else if (tiempoNivelActual <= 15 && tiempoNivelActual > 5)
            puntajeNivel = 10;
        else if (tiempoNivelActual <= 5)
            puntajeNivel = 5;
    }

    // Método para restablecer puntajes y tiempos cuando el jugador regresa al menú principal
    public static void ReiniciarDatos()
    {
        puntajeTotal = 0;
        nivelesSuperados = 0;
        tiemposNiveles.Clear();
        puntajesNiveles.Clear();
    }

    public void SiguienteLvl()
    {
        Time.timeScale = 1;  // Restaurar el tiempo
        Physics2D.IgnoreLayerCollision(6, 10, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Principal()
    {
        Time.timeScale = 1;  // Restaurar el tiempo
        Physics2D.IgnoreLayerCollision(6, 10, false);

        // Restablecer datos antes de regresar al menú principal
        ReiniciarDatos();

        // Cargar la escena del menú principal (o ajustar el índice según tu flujo)
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Método para guardar el ranking
    private void GuardarRanking()
    {
        string nombreJugador = "Anónimo"; // Obtén el nombre del jugador desde el InputField

        if (string.IsNullOrEmpty(nombreJugador))  // Asegúrate de que el nombre no esté vacío
        {
            nombreJugador = "Anónimo";
        }

        // Verificar si nivelesSuperados es mayor que 0 antes de hacer la división
        float promedio = (nivelesSuperados > 0) ? puntajeTotal / (float)nivelesSuperados : 0;
        List<float> tiempos = new List<float>(tiemposNiveles);
        List<int> puntajes = new List<int>(puntajesNiveles);

        // Verifica si la instancia de RankingManager está inicializada correctamente
        if (RankingManager.Instance != null)
        {
            RankingManager.Instance.GuardarRanking(nombreJugador, promedio, tiempos, puntajes);
        }
        else
        {
            Debug.LogError("RankingManager.Instance no está inicializado correctamente.");
        }
    }
}
