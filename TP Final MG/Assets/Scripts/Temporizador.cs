using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public float _Temporizador = 0f;
    public Text TextoTemporizador;

    void Update()
    {
        _Temporizador -= Time.deltaTime;
        TextoTemporizador.text = "" + _Temporizador.ToString("f0");
    }
}
