using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Este script implementa la lógica de los botones del menú principal.
/// </summary>
public class CtrMenu : MonoBehaviour
{
    public GameObject pMenu;
    public GameObject bAbrirMenu;
    public GameObject pMapaNavegacion;
    public GameObject pDistancias;
    //public GameObject joystick;

    //SONIDOS
    public AudioSource sourceBotones;
    public AudioClip soundBotones;

    public void abrirMenu()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(true);
        bAbrirMenu.SetActive(false);
        pDistancias.SetActive(false);
        //joystick.SetActive(false);
    }

    public void cerrarMenu()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(false);
        bAbrirMenu.SetActive(true);
        pDistancias.SetActive(true);
        //joystick.SetActive(true);
    }

    public void abrirPInventario()
    {
        sourceBotones.PlayOneShot(soundBotones);
    }

    public void abrirPNavegacion()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(false);
        pMapaNavegacion.SetActive(true);
    }

    public void abrirPInteraccion()
    {
        sourceBotones.PlayOneShot(soundBotones);
    }

    public void reiniciarApp()
    {
        sourceBotones.PlayOneShot(soundBotones);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void cerrarApp()
    {
        sourceBotones.PlayOneShot(soundBotones);
        Application.Quit();
    }

    public void volverAlMenu_Navegacion()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(true);
        pMapaNavegacion.SetActive(false);
    }

    public void activarCamara()
    {

    }

}
