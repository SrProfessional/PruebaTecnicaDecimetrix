using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CtrBInteraccion : MonoBehaviour
{
    //AVISOS
    public GameObject pAviso1;
    public GameObject pAviso2;
    public GameObject pAviso3;
    public GameObject pEnhorabuena;
    public GameObject pVolverAIntentar;
    public GameObject bVolverAIntentar2;
    public GameObject bVolverAIntentar3;

    public GameObject pInteraccion;
    public GameObject pInventario;
    public Button bSlot1;
    public Button bSlot2;
    public Button bSlot3;
    public GameObject lineaDivisionScreen;

    //BOTONES
    public GameObject bInventario;
    public GameObject bCerrarInventario;
    public GameObject bVerificarAviso2;
    public GameObject bVerificarAviso3;

    //CAMARAS
    public Camera mainCamera;
    public Camera arCamera;

    public List<GameObject> listaElementos;
    public List<Transform> listaParentsElementos;

    //SPRITES DE SLOTS
    public List<Image> listaImgsSlots;

    //WORLD SPACE TO CANVAS COORDINATES
    //public RectTransform rectCanvas;

    public Transform transformCuboA;
    public Transform transformPrismaB;
    public Transform transformCilindroC;

    //SONIDOS
    public AudioSource sourceBotones;
    public AudioClip soundBotones;
    public AudioClip soundAcierto;
    public AudioClip soundError;

    public bool activarMovimiento;

    private List<Vector3> posicionesIniciales;
    public ParticleSystem particlesExplosion;

    void Start()
    {
        activarMovimiento = false;
        posicionesIniciales = new List<Vector3>();
    }

    public void Aviso1_continuar()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pAviso1.SetActive(false);
        pAviso2.SetActive(true);

        //GUARDA LAS POSICIONES INICIALES DE LOS ELEMENTOS PARA PODERLOS MOSTRAR EN LA MISMA POSICION EN EL AVISO 3
        for (int i = 0; i < listaElementos.Count; i++)
        {
            posicionesIniciales.Add(listaElementos[i].transform.localPosition);
        }
    }

    public void Aviso2_empezar()
    {
        sourceBotones.PlayOneShot(soundBotones);

        //SE DESACTIVA EL ANUNCIO, SE ACTIVA LA LINEA DE DIVISION, SE AÑADEN LOS ELEMENTOS DEL INVENTARIO EN SU ORDEN EN X CENTRADOS
        //Y SE ACTIVA EL BOTÓN DE VERIFICAR ANUNCIO 2
        pAviso2.SetActive(false);
        bInventario.SetActive(false);
        lineaDivisionScreen.SetActive(true);
        bCerrarInventario.SetActive(false);
        bVerificarAviso2.SetActive(true);

        pInventario.SetActive(true);
        pInventario.transform.GetChild(0).localPosition = new Vector3(0f, pInventario.transform.GetChild(0).localPosition.y, pInventario.transform.GetChild(0).localPosition.z);
        pInventario.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        bSlot1.enabled = true;
        bSlot2.enabled = true;
        bSlot3.enabled = true;
        pInteraccion.SetActive(false);

        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = -1;

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamera.tag = "MainCamera";
        mainCamera.tag = "Untagged";

        //DESACTIVAR TODOS LOS ELEMENTOS Y REGRESARLOS A SUS POSICIONES INICIALES
        for (int i = 0; i < listaElementos.Count; i++)
        {
            listaElementos[i].SetActive(false);
            listaElementos[i].transform.localPosition = posicionesIniciales[i];
        }
    }

    public void Verificar_Aviso2()
    {
        if((transformCilindroC.transform.position.x < transformCuboA.transform.position.x
            && transformCuboA.transform.position.x < transformPrismaB.transform.position.x) && 
            (transformCilindroC.transform.position.y > arCamera.ScreenToWorldPoint(lineaDivisionScreen.transform.position).y
            && transformCuboA.transform.position.y > arCamera.ScreenToWorldPoint(lineaDivisionScreen.transform.position).y
            && transformPrismaB.transform.position.y > arCamera.ScreenToWorldPoint(lineaDivisionScreen.transform.position).y))
        {
            //CORRECTO
            sourceBotones.PlayOneShot(soundAcierto);
            StartCoroutine(Aviso2Correcto());
        }
        else
        {
            StartCoroutine(PerderAviso2());
        }
    }

    public void ContinuarEnhorabuena()
    {
        sourceBotones.PlayOneShot(soundBotones);
        pEnhorabuena.SetActive(false);
        pAviso3.SetActive(true);
    }

    public void Aviso3_empezar()
    {
        sourceBotones.PlayOneShot(soundBotones);

        //SE DESACTIVA EL ANUNCIO, SE ACTIVA LA LINEA DE DIVISION, SE AÑADEN LOS ELEMENTOS DEL INVENTARIO EN SU ORDEN EN X CENTRADOS
        //Y SE ACTIVA EL BOTÓN DE VERIFICAR ANUNCIO 2
        pAviso3.SetActive(false);
        bInventario.SetActive(false);
        lineaDivisionScreen.SetActive(true);
        bCerrarInventario.SetActive(false);
        bVerificarAviso3.SetActive(true);

        pInventario.SetActive(true);
        pInventario.transform.GetChild(0).localPosition = new Vector3(0f, pInventario.transform.GetChild(0).localPosition.y, pInventario.transform.GetChild(0).localPosition.z);
        pInventario.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        bSlot1.enabled = true;
        bSlot2.enabled = true;
        bSlot3.enabled = true;
        pInteraccion.SetActive(false);

        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = -1;

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamera.tag = "MainCamera";
        mainCamera.tag = "Untagged";

        //DESACTIVAR TODOS LOS ELEMENTOS Y REGRESARLOS A SUS POSICIONES INICIALES
        for (int i = 0; i < listaElementos.Count; i++)
        {
            listaElementos[i].SetActive(false);
            listaElementos[i].transform.localPosition = posicionesIniciales[i];
        }
    }

    public void Verificar_Aviso3()
    {
        if ((transformPrismaB.transform.position.x < transformCuboA.transform.position.x
            && transformCuboA.transform.position.x < transformCilindroC.transform.position.x) &&
            (transformCilindroC.transform.position.y > arCamera.ScreenToWorldPoint(lineaDivisionScreen.transform.position).y
            && transformCuboA.transform.position.y > arCamera.ScreenToWorldPoint(lineaDivisionScreen.transform.position).y
            && transformPrismaB.transform.position.y > arCamera.ScreenToWorldPoint(lineaDivisionScreen.transform.position).y))
        {
            //CORRECTO
            sourceBotones.PlayOneShot(soundAcierto);
            StartCoroutine(Aviso3Correcto());
        }
        else
        {
            StartCoroutine(PerderAviso3());
        }
    }

    IEnumerator PerderAviso2()
    {
        //ERROR
        sourceBotones.PlayOneShot(soundError);
        bSlot1.enabled = false;
        bSlot2.enabled = false;
        bSlot3.enabled = false;

        //DESACTIVAR TODOS LOS ELEMENTOS Y REGRESARLOS A SUS POSICIONES INICIALES
        for (int i = 0; i < listaElementos.Count; i++)
        {
            listaElementos[i].SetActive(false);
            listaElementos[i].transform.position = posicionesIniciales[i];
        }

        pInventario.SetActive(false);
        lineaDivisionScreen.SetActive(false);

        //HACER EXPLOSION
        particlesExplosion.gameObject.SetActive(true);
        particlesExplosion.Play();

        yield return new WaitForSeconds(2f);

        bVerificarAviso2.SetActive(false);
        bVolverAIntentar2.SetActive(true);

        //ACTIVAR PANEL DE VOLVER A INTENTAR
        pInteraccion.SetActive(true);
        pVolverAIntentar.SetActive(true);

        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = 1;

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamera.tag = "Untagged";
        mainCamera.tag = "MainCamera";
    }

    IEnumerator PerderAviso3()
    {
        //ERROR
        sourceBotones.PlayOneShot(soundError);
        bSlot1.enabled = false;
        bSlot2.enabled = false;
        bSlot3.enabled = false;

        //DESACTIVAR TODOS LOS ELEMENTOS Y REGRESARLOS A SUS POSICIONES INICIALES
        for (int i = 0; i < listaElementos.Count; i++)
        {
            listaElementos[i].SetActive(false);
            listaElementos[i].transform.position = posicionesIniciales[i];
        }

        pInventario.SetActive(false);
        lineaDivisionScreen.SetActive(false);

        //HACER EXPLOSION
        particlesExplosion.gameObject.SetActive(true);
        particlesExplosion.Play();

        yield return new WaitForSeconds(2f);

        bVerificarAviso3.SetActive(false);
        bVolverAIntentar3.SetActive(true);

        //ACTIVAR PANEL DE VOLVER A INTENTAR
        pInteraccion.SetActive(true);
        pVolverAIntentar.SetActive(true);

        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = 1;

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamera.tag = "Untagged";
        mainCamera.tag = "MainCamera";
    }

    public void BSlot1()
    {
        //SE ACTIVA EL ELEMENTO Y SE ENVÍA A LA POSICIÓN DEL TOUCH
        for (int i = 0; i < listaElementos.Count; i++)
        {
            if (listaElementos[i].GetComponent<ClaseElemento>().imgElemento == listaImgsSlots[0].sprite)
            {
                if (Touch.activeFingers.Count == 1)
                {
                    Vector3 screenPos = arCamera.ScreenToWorldPoint(Touch.activeFingers[0].currentTouch.screenPosition);
                    listaElementos[i].SetActive(true);
                    listaElementos[i].GetComponent<DragElement>().enabled = true;
                    listaParentsElementos[i].position = new Vector3(screenPos.x - 0.8f, screenPos.y - 0.69f, screenPos.z + 1.78f);
                    Debug.Log("entra");
                }
            }
        }
    }

    public void BSlot2()
    {
        //SE ACTIVA EL ELEMENTO Y SE ENVÍA A LA POSICIÓN DEL TOUCH
        for (int i = 0; i < listaElementos.Count; i++)
        {
            if (listaElementos[i].GetComponent<ClaseElemento>().imgElemento == listaImgsSlots[1].sprite)
            {
                if (Touch.activeFingers.Count == 1)
                {
                    Vector3 screenPos = arCamera.ScreenToWorldPoint(Touch.activeFingers[0].currentTouch.screenPosition);
                    listaElementos[i].SetActive(true);
                    listaElementos[i].GetComponent<DragElement>().enabled = true;
                    listaParentsElementos[i].position = new Vector3(screenPos.x - 0.8f, screenPos.y - 0.69f, screenPos.z + 1.78f);
                }
            }
        }
    }

    public void BSlot3()
    {
        //SE ACTIVA EL ELEMENTO Y SE ENVÍA A LA POSICIÓN DEL TOUCH
        for (int i = 0; i < listaElementos.Count; i++)
        {
            if (listaElementos[i].GetComponent<ClaseElemento>().imgElemento == listaImgsSlots[2].sprite)
            {
                if (Touch.activeFingers.Count == 1)
                {
                    Vector3 screenPos = arCamera.ScreenToWorldPoint(Touch.activeFingers[0].currentTouch.screenPosition);
                    listaElementos[i].SetActive(true);
                    listaElementos[i].GetComponent<DragElement>().enabled = true;
                    listaParentsElementos[i].position = new Vector3(screenPos.x - 0.8f, screenPos.y - 0.69f, screenPos.z + 1.78f);
                }
            }
        }
    }

    IEnumerator Aviso2Correcto()
    {
        //HACER UN POCO DE ZOOM CON AR CAMERA
        for (int i = 0; i < 100; i++)
        {
            float valorZoom = 0.005f;
            arCamera.transform.position = new Vector3(arCamera.transform.position.x, arCamera.transform.position.y, arCamera.transform.position.z + valorZoom);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        bSlot1.enabled = false;
        bSlot2.enabled = false;
        bSlot3.enabled = false;

        bVerificarAviso2.SetActive(false);
        pInventario.SetActive(false);
        lineaDivisionScreen.SetActive(false);

        //ACTIVAR PANEL DE ENHORABUENA
        pInteraccion.SetActive(true);
        pEnhorabuena.SetActive(true);

        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = 1;

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamera.tag = "Untagged";
        mainCamera.tag = "MainCamera";
    }

    IEnumerator Aviso3Correcto()
    {
        //HACER UN POCO DE ZOOM CON AR CAMERA
        for (int i = 0; i < 100; i++)
        {
            float valorZoom = 0.005f;
            arCamera.transform.position = new Vector3(arCamera.transform.position.x, arCamera.transform.position.y, arCamera.transform.position.z + valorZoom);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        //DESACTIVAR TODOS LOS ELEMENTOS Y REGRESARLOS A SUS POSICIONES INICIALES
        for (int i = 0; i < listaElementos.Count; i++)
        {
            listaElementos[i].SetActive(false);
            listaElementos[i].transform.position = posicionesIniciales[i];
        }

        bVerificarAviso3.SetActive(false);
        pInventario.SetActive(false);
        lineaDivisionScreen.SetActive(false);

        bSlot1.enabled = false;
        bSlot2.enabled = false;
        bSlot3.enabled = false;

        //SE ACOMODAN LAS CAMARAS
        mainCamera.depth = 1;

        //PONER TAG DE MAIN CAMERA A LA AR CAMERA
        arCamera.tag = "Untagged";
        mainCamera.tag = "MainCamera";
    }

    public void VolverAIntentarAviso2()
    {
        Aviso1_continuar();
        bVolverAIntentar2.SetActive(false);
        pVolverAIntentar.SetActive(false);
    }

    public void VolverAIntentarAviso3()
    {
        ContinuarEnhorabuena();
        bVolverAIntentar3.SetActive(false);
        pVolverAIntentar.SetActive(false);
    }
}
