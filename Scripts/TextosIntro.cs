using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextosIntro : MonoBehaviour
{
    public TextMeshProUGUI textD;
    [TextArea(1, 30)]
    public string[] parrafos;
    public int index;
    public float velParrafo;
    public GameObject botonContinuar;
    public GameObject Dialogo;
    public static bool ready = false;

    private void Start()
    {
        Dialogo.SetActive(true);
        StartCoroutine(Diag());
        botonContinuar.SetActive(false);
    }
    private void Update()
    {
        if (textD.text == parrafos[index])
        {
            botonContinuar.SetActive(true);
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


    public void BotonContinuar()
    {
        Tiempo.ready = true;
        Dialogo.SetActive(false);
    }
}