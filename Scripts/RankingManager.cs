using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    private const int maxRanking = 10;
    private string rankingFilePath;

    public List<RankingEntry> rankingEntries;

    public static RankingManager Instance { get; private set; }

    private void Awake()
    {
        // Implementación de Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Si ya existe una instancia, destruye la nueva.
        }
        else
        {
            Instance = this; // Asigna la instancia
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }

        rankingFilePath = Path.Combine(Application.persistentDataPath, "ranking.json");
        CargarRanking();
    }

    public void GuardarRanking(string nombre, float promedio, List<float> tiempos, List<int> puntajes)
    {
        // Crear un nuevo registro de ranking
        RankingEntry nuevaEntrada = new RankingEntry(nombre, promedio, tiempos, puntajes);

        // Añadir y ordenar el ranking
        rankingEntries.Add(nuevaEntrada);
        rankingEntries = rankingEntries.OrderByDescending(entry => entry.promedio).Take(maxRanking).ToList();

        // Guardar el ranking en JSON
        GuardarEnArchivo();
    }

    private void GuardarEnArchivo()
    {
        // Convertir la lista a JSON y guardarla
        string jsonData = JsonUtility.ToJson(new RankingData(rankingEntries), true);
        File.WriteAllText(rankingFilePath, jsonData);
    }

    private void CargarRanking()
    {
        if (File.Exists(rankingFilePath))
        {
            string jsonData = File.ReadAllText(rankingFilePath);
            RankingData datosCargados = JsonUtility.FromJson<RankingData>(jsonData);
            rankingEntries = datosCargados.entries;
        }
        else
        {
            rankingEntries = new List<RankingEntry>();
        }
    }

    // Clase para el JSON
    [System.Serializable]
    public class RankingData
    {
        public List<RankingEntry> entries;
        public RankingData(List<RankingEntry> entries)
        {
            this.entries = entries;
        }
    }

    [System.Serializable]
    public class RankingEntry
    {
        public string nombre;
        public float promedio;
        public List<float> tiemposNiveles;
        public List<int> puntajesNiveles;

        public RankingEntry(string nombre, float promedio, List<float> tiempos, List<int> puntajes)
        {
            this.nombre = nombre;
            this.promedio = promedio;
            this.tiemposNiveles = tiempos;
            this.puntajesNiveles = puntajes;
        }
    }
}
