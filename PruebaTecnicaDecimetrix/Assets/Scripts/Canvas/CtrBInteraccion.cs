using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrBInteraccion : MonoBehaviour
{
    //AVISOS
    public GameObject pAviso1;
    public GameObject pAviso2;
    public GameObject pAviso3;
    public GameObject pEnhorabuena;

    public Transform transformInventario;

    //BOTONES
    public GameObject bCerrarInventario;
    public GameObject bVerificarAviso2;
    public GameObject bVerificarAviso3;

    //CAMARAS
    public Camera mainCamera;
    public Camera arCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aviso1_continuar()
    {
        pAviso1.SetActive(false);
        pAviso2.SetActive(true);
    }

    public void Aviso2_empezar()
    {
        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = -1;
        arCamera.rect = new Rect(arCamera.rect.x, arCamera.rect.y + 0.5f, arCamera.rect.width, arCamera.rect.height);
        
        //SE DESACTIVA EL ANUNCIO, SE MUEVE EL INVENTARIO HACIA EL CENTRO, SE DESACTIVA EL BOTÓN DE CERRAR INVENTARIO Y SE ACTIVA EL BOTON DE VERIFICAR DEL ANUNCIO 2
        pAviso2.SetActive(false);
        transformInventario.localPosition = new Vector3(0f, transformInventario.localPosition.y, transformInventario.localPosition.z);
        bCerrarInventario.SetActive(false);
        bVerificarAviso2.SetActive(true);
    }

    public void Verificar_Aviso2()
    {

    }
}
