using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarElemento : MonoBehaviour
{
    /*public int tiempoEspera;
    private int tiempoActual;

    void Start()
    {
        tiempoActual = 0;
    }*/

    void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Handheld.Vibrate();
        }
    }

    /*IEnumerator esperar()
    {
        for(int i = 0; i < tiempoEspera; i++)
        {
            if(tiempoActual <= tiempoEspera)
            {
                if (Input.GetTouch(0).tapCount == 2)
                {
                    //Double tap
                }
            }
            else
            {
                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {

                }
            }
        }
        while(tiempoActual <= tiempoEspera)
        {
            yield return new WaitForSeconds(1f);


        }
    }*/
}
