using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public float tempo = 0;
    public TextMeshProUGUI textoTempo;
    public static bool ready = false;

    private void Update()
    {
        if (ready)
        {
            tempo -= Time.deltaTime;
            if (tempo < 0.5)
            {
                ready = false;
                interaccion.muerteTiempo = true;
            }
            textoTempo.text = "" + tempo.ToString("f0");
        }
    }


}
