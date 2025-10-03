using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    public GameObject Libreta;
    public GameObject Dialogo;

    private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        Libreta.SetActive(false);
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        Dialogo.SetActive(false);
        juegoPausado = true;
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        Libreta.SetActive(true);
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Principal()
    {
        // Llamar a ReiniciarDatos para resetear los puntajes y tiempos
        GameOverS.ReiniciarDatos(); // Restablece los puntajes y tiempos desde GameOverS

        Physics2D.IgnoreLayerCollision(6, 10, false);

        // Cargar la escena del menú principal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
