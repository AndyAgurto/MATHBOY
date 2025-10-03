using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip animacion;

    void Start()
    {
        animator = GetComponent<Animator>();
   }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F12))
        {
            StartCoroutine(CambiarEscena());
        }
    }
    IEnumerator CambiarEscena()
    {
        animator.SetTrigger("Inicio");
        yield return new WaitForSeconds(animacion.length);

        SceneManager.LoadScene(1);
    }
}
