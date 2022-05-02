using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Este script implementa la lógica de los botones del menú principal.
/// </summary>
public class CtrMenu : MonoBehaviour
{
    public GameObject pMenu;
    public GameObject bAbrirMenu;
    public GameObject bActivarCamara;
    public GameObject bInventarioNavegacion;
    public GameObject pMapaNavegacion;
    public GameObject pDistancias;
    public GameObject pInventario;
    public GameObject arCamera;
    //public GameObject joystick;

    //SONIDOS
    public AudioSource sourceBotones;
    public AudioClip soundBotones;

    //BOTONES
    public Button bInventarioMenu;
    public Button bNavegacion;
    public Button bInteraccion;
    public Button bMapaNavegacion;
    public Button bReiniciar;
    public Button bCerrarApp;
    public Button bCerrarNavegacion;

    public void AbrirMenu()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(true);
        bAbrirMenu.SetActive(false);
        pDistancias.SetActive(false);
        bInventarioNavegacion.SetActive(false);
        bActivarCamara.SetActive(false);
        //joystick.SetActive(false);
    }

    public void CerrarMenu()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(false);
        bAbrirMenu.SetActive(true);
        pDistancias.SetActive(true);
        bInventarioNavegacion.SetActive(true);
        bActivarCamara.SetActive(true);
        //joystick.SetActive(true);
    }

    public void AbrirPInventario()
    {
        sourceBotones.PlayOneShot(soundBotones);

        pInventario.SetActive(true);
        bInventarioNavegacion.SetActive(false);

        if (pMenu.activeSelf == true)
        {
            DesactivarBotonesMenu();
        }
    }

    public void CerrarInventario()
    {
        sourceBotones.PlayOneShot(soundBotones);

        pInventario.SetActive(false);
        bInventarioNavegacion.SetActive(true);

        if (pMenu.activeSelf == true)
        {
            ActivarBotonesMenu();
        }
    }

    public void AbrirPNavegacion()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(false);
        pMapaNavegacion.SetActive(true);
    }

    public void AbrirPInteraccion()
    {
        sourceBotones.PlayOneShot(soundBotones);
    }

    public void ReiniciarApp()
    {
        sourceBotones.PlayOneShot(soundBotones);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CerrarApp()
    {
        sourceBotones.PlayOneShot(soundBotones);
        Application.Quit();
    }

    public void VolverAlMenu_Navegacion()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(true);
        pMapaNavegacion.SetActive(false);
    }

    public void ActivarCamara()
    {
        arCamera.SetActive(true);
    }

    public void CerrarCamara()
    {
        arCamera.SetActive(false);
    }

    public void DesactivarBotonesMenu()
    {
        bInventarioMenu.interactable = false;
        bNavegacion.interactable = false;
        bInteraccion.interactable = false;
        bMapaNavegacion.interactable = false;
        bReiniciar.interactable = false;
        bCerrarApp.interactable = false;
    }

    public void ActivarBotonesMenu()
    {
        bInventarioMenu.interactable = true;
        bNavegacion.interactable = true;
        bInteraccion.interactable = true;
        bMapaNavegacion.interactable = true;
        bReiniciar.interactable = true;
        bCerrarApp.interactable = true;
    }
}
