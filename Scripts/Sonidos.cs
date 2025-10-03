using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour {
    public AudioSource sound;
    public AudioClip Click;

    public void SoundButton()//Esta funcion es llamada desde el Boton a traves del componente Event Trigger.
    {
        //Simplemente eliges que sonido sonara (en el caso que tengas mas clip)
        sound.clip = Click;
        //Lo desactivo y activo para que genere el sonido
        sound.enabled = false;
        sound.enabled = true;
    }

}
