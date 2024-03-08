using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script es un componente que se adjunta a un GameObject para mostrar texto flotante en el juego. Puede ser utilizado 
/// para mostrar información temporal que debe ser visible para el jugador durante un corto período de tiempo.
/// </summary>
[RequireComponent(typeof(TextMesh))]
public class FloatingText : MonoBehaviour
{
    [SerializeField] float Velocidad;//La velocidad a la que el texto flotante se desplaza hacia arriba.

    [SerializeField] float TiempoVida;//El tiempo de vida del texto flotante antes de que se destruya.
    
    TextMesh Texto;//Es una referencia al componente TextMesh adjunto al GameObject.

    /// <summary>
    /// Obtiene la referencia al componente TextMesh adjunto al GameObject. Programa la destrucción del GameObject después de TiempoVida en segundos.
    /// </summary>
    void Awake()
    {
        Texto = GetComponent<TextMesh>();
        Destroy(gameObject, TiempoVida);
    }

    /// <summary>
    /// Este método se llama para inicializar el texto flotante con el texto especificado. Establece el texto del 
    /// componente TextMesh (Texto) con el texto proporcionado.
    /// </summary>
    /// <param name="text"></param>
    public void Init(string text)
    {
        Texto.text = text;
    }

    /// <summary>
    /// Mueve el GameObject hacia arriba en el eje Y multiplicando la Velocidad por el tiempo entre frames (Time.deltaTime).
    /// </summary>
    void Update()
    {
        transform.position += Vector3.up * Velocidad * Time.deltaTime;
    }
}
