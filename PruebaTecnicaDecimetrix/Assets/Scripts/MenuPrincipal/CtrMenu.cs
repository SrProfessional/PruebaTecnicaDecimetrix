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
    public GameObject bCerrarCamara;
    public GameObject bInventarioNavegacion;
    public GameObject pMapaNavegacion;
    public GameObject pDistancias;
    public GameObject pInventario;
    public GameObject pInteraccion;
    public Camera mainCamera;
    public GameObject camara;
    public GameObject arCamara;

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
    public Button bSlot1;
    public Button bSlot2;
    public Button bSlot3;

    public List<GameObject> listaElementos;
    public VisualizacionCoordenadas visualizacionCoordenadas;
    public CtrSlotsInventario ctrSlotsInventario;
    private bool activarBInteraccion;

    public GameObject animDobleTouch;
    public Transform transformImgDobleTouch;
    public bool activarAnimacionDobleTouch;

    private void Start()
    {
        pMenu.SetActive(true);
        activarBInteraccion = true;
        activarAnimacionDobleTouch = false;
    }

    private void Update()
    {
        if(ctrSlotsInventario.listaElementosRecogidos.Count == 3 && activarBInteraccion)
        {
            activarBInteraccion = false;
            bInteraccion.interactable = true;
        }

        if(activarAnimacionDobleTouch)
        {
            StartCoroutine(EscalarAvisoDobleTouch());
        }
    }

    public void AbrirMenu()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(true);
        bAbrirMenu.SetActive(false);
        pDistancias.SetActive(false);
        bInventarioNavegacion.SetActive(false);
    }

    public void CerrarMenu()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pMenu.SetActive(false);
        bAbrirMenu.SetActive(true);
        pDistancias.SetActive(true);
        bInventarioNavegacion.SetActive(true);
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

        pInteraccion.SetActive(true);
        pMenu.SetActive(false);
        bSlot1.enabled = true;
        bSlot2.enabled = true;
        bSlot3.enabled = true;
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
        sourceBotones.PlayOneShot(soundBotones);

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamara.tag = "MainCamera";
        camara.tag = "Untagged";
        mainCamera.depth = -1;
        bCerrarCamara.SetActive(true);
        bAbrirMenu.SetActive(false);
        bInventarioNavegacion.SetActive(false);
        animDobleTouch.SetActive(true);
        activarAnimacionDobleTouch = true;
    }

    public void CerrarCamara()
    {
        sourceBotones.PlayOneShot(soundBotones);

        //PONER TAG DE MAIN CAMERA A LA CAMARA NORMAL
        arCamara.tag = "Untagged"; 
        camara.tag = "MainCamera";
        visualizacionCoordenadas.activarCamara = true;
        visualizacionCoordenadas.desactivarCamara = true;
        bCerrarCamara.SetActive(false);

        for (int i = 0; i < visualizacionCoordenadas.listaElementos.Count; i++)
        {
            if (PlayerPrefs.GetInt(visualizacionCoordenadas.listaElementos[i].tag + "Recogido") == 0)
            {
                visualizacionCoordenadas.listaElementos[i].SetActive(true);
            }
        }

        mainCamera.depth = 1;
        bAbrirMenu.SetActive(true);
        bInventarioNavegacion.SetActive(true);
        activarAnimacionDobleTouch = false;
        animDobleTouch.SetActive(false);
        transformImgDobleTouch.localScale = new Vector3(0.7678616f, 0.7678616f, 0.7678616f);
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

    IEnumerator EscalarAvisoDobleTouch()
    {
        activarAnimacionDobleTouch = false;

        float valorAgregado = 0.025f;

        for(int i = 0; i < 8; i++)
        {
            transformImgDobleTouch.localScale = new Vector3(transformImgDobleTouch.localScale.x + valorAgregado, transformImgDobleTouch.localScale.y + valorAgregado,
                transformImgDobleTouch.localScale.z + valorAgregado);

            yield return null;
            yield return null;
        }

        yield return null;

        for (int i = 0; i < 8; i++)
        {
            transformImgDobleTouch.localScale = new Vector3(transformImgDobleTouch.localScale.x - valorAgregado, transformImgDobleTouch.localScale.y - valorAgregado,
                transformImgDobleTouch.localScale.z - valorAgregado);

            yield return null;
            yield return null;
        }

        yield return null;

        activarAnimacionDobleTouch = true;
    }
}
