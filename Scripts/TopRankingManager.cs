using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopRankingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankingDisplayText; // Cambiar a TextMeshProUGUI
    [SerializeField] private TMP_InputField nombreInputField; // Cambiar a TMP_InputField

    private void Start()
    {
        MostrarRanking(); // Mostrar el ranking actual
    }

    public void GuardarPuntaje()
    {
        // Verificar que se ingresó un nombre
        if (string.IsNullOrEmpty(nombreInputField.text))
        {
            Debug.LogWarning("Por favor, ingresa un nombre para guardar el puntaje.");
            return;
        }

        // Obtener el puntaje promedio acumulado del juego
        float puntajePromedio = GameOverS.puntajeTotal / GameOverS.nivelesSuperados;
        int intentos = GameOverS.nivelesSuperados; // Reemplazar con el contador de intentos si es diferente

        // Guardar el puntaje en el ranking
        RankingManager.Instance.GuardarRanking(
            nombreInputField.text,
            puntajePromedio,
            GameOverS.tiemposNiveles,
            GameOverS.puntajesNiveles
        );

        // Refrescar la pantalla con el ranking actualizado
        MostrarRanking();
    }

    private void MostrarRanking()
    {
        rankingDisplayText.text = "TOP 10 RANKING\n";
        rankingDisplayText.text += "NOMBRE ---------- Promedio -------------- Tiempo en Niveles ---------- Puntajes\n"; // Encabezado de la tabla

        // Iteramos sobre las entradas del ranking
        foreach (var entry in RankingManager.Instance.rankingEntries)
        {
            // Redondeamos los tiempos a 2 decimales
            string tiemposRedondeados = string.Join(", ", entry.tiemposNiveles.Select(t => Mathf.Round(t * 100f) / 100f).Select(t => t.ToString("F1")));

            // Formateamos y mostramos los datos
            rankingDisplayText.text += $"{entry.nombre,-20} ---------- {entry.promedio,-10} ---------- {tiemposRedondeados,-20} ---------- {string.Join(", ", entry.puntajesNiveles)}\n";
        }
        }
    public void Principal()
    {
        Time.timeScale = 1;  // Restaurar el tiempo
        Physics2D.IgnoreLayerCollision(6, 10, false);

        // Cargar la escena del menú principal (o ajustar el índice según tu flujo)
        SceneManager.LoadScene("MenuPrincipal");
    }
}
