using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public float _Temporizador = 60f; //Tiempo inicial del temporizador en segundos
    public Text TextoTemporizador;
    public Image imagenFinJuego; //Referencia a la imagen "Fin del juego"

    private bool finJuegoMostrado = false; //Para controlar si la imagen "Fin del juego" ya ha sido mostrada

    void Update()
    {
        //Reproduce el temporizador cada frame
        _Temporizador -= Time.deltaTime;

        //Actualiza el texto del temporizador
        TextoTemporizador.text = Mathf.RoundToInt(_Temporizador).ToString();

        //Verifica si el temporizador ha llegado a cero y la imagen "Fin del juego" aún no se ha mostrado
        if (_Temporizador <= 0 && !finJuegoMostrado)
        {
            //Muestra la imagen "Fin del juego" y establecer el bool en verdadero para indicar que ya se ha mostrado
            if (imagenFinJuego != null)
            {
                imagenFinJuego.gameObject.SetActive(true);
            }
            finJuegoMostrado = true;
        }
    }
}
