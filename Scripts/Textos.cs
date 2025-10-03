using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Textos : MonoBehaviour
{
    public TextMeshProUGUI textD;
    [TextArea(1, 30)]
    public string[] parrafos;
    public int index;
    public float velParrafo;
    public GameObject botonSalir;
    public GameObject Dialogo;
    public GameObject Libreta;

    private void Start()
    {
        Libreta.SetActive(true);
        Dialogo.SetActive(false);
        botonSalir.SetActive(false);
    }
    private void Update()
    {
        if (textD.text == parrafos[index])
        {
            botonSalir.SetActive(true);
        }
    }

    IEnumerator Diag()
    {
        textD.text = "";
        foreach (char letra in parrafos[index].ToCharArray())
        {
            textD.text += letra;
            yield return new WaitForSeconds(velParrafo);
        }
    }
  
    public void activarLibreta()
    {
        
        Dialogo.SetActive(true);
        Libreta.SetActive(false);
        StartCoroutine(Diag());
    }
    
    public void BotonSalir()
    {
        Dialogo.SetActive(false);
        Libreta.SetActive(true);
    }
}
