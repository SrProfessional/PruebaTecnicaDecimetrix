using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Este scirpt alerta sobre elementos a menos de 4m de distancia por medio de la vibración del dispositivo móvil.
/// </summary>
public class AlertarElementosCercanos : MonoBehaviour
{
    public TextMeshProUGUI txtDistCuboA;
    public TextMeshProUGUI txtDistPrismaB;
    public TextMeshProUGUI txtDistCilindroC;

    public Button bCamara;
    public CtrMenu ctrMenu;

    void Update()
    {
        if (float.Parse(txtDistCuboA.text) <= 4f || float.Parse(txtDistPrismaB.text) <= 4f || float.Parse(txtDistCilindroC.text) <= 4f)
        {
            Handheld.Vibrate();
            bCamara.interactable = true;
        }
    }

    public void tomarCuboA()
    {
        ctrMenu.ActivarCamara();
    }

    public void tomarPrismaB()
    {
        ctrMenu.ActivarCamara();
    }

    public void tomarCilindroC()
    {
        ctrMenu.ActivarCamara();
    }
}
