using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script gestiona el puntaje del juego y proporciona una forma de acceder y actualizar el puntaje desde otras clases. También asegura que solo haya una instancia de ScoreManager en la escena y notifica a través de un evento cuando el puntaje cambia.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] int _Puntuacion = 100;//Un entero que representa el puntaje actual del juego.
    public int Puntuacion//Esta propiedad encapsula la variable _Puntuacion, permitiendo su acceso desde otras clases.     Cuando se establece el puntaje (set), también invoca el evento CuandoPuntuacionCambia con el nuevo valor del puntaje convertido a una cadena.
    {
        get { return _Puntuacion; }
        set
        {
            _Puntuacion = value;
            CuandoPuntuacionCambia.Invoke(_Puntuacion.ToString());
        }
    }
    [SerializeField] protected CadenaEventos CuandoPuntuacionCambia;//Un evento que se invoca cuando cambia el puntaje. Es de tipo CadenaEventos, que es un evento personalizado que acepta un parámetro de tipo string.

    public static ScoreManager Singleton;//Una referencia estática a sí mismo. Esto se usa para asegurar que solo haya una instancia de ScoreManager en la escena.

    /// <summary>
    /// Este método se llama al inicio. 
    /// Comprueba si ya existe una instancia de ScoreManager. Si es así, 
    /// destruye el GameObject actual para asegurar que solo haya una instancia en la escena.
    /// Si no hay una instancia previa, establece Singleton como esta instancia y asegura que
    /// el evento CuandoPuntuacionCambia se invoque con el puntaje inicial.
        /// </summary>
    void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;

        CuandoPuntuacionCambia.Invoke(_Puntuacion.ToString());
    }
}