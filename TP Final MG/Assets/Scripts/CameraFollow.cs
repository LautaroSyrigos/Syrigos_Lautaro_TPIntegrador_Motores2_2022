using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script controla el seguimiento de la cámara en el eje X según la posición horizontal del objetivo 
/// y permite ajustar los límites verticales de la cámara, tanto en tiempo de ejecución del 
/// juego como en el Editor de Unity.
/// </summary>
[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    //Esta variable estática define los límites verticales de la cámara en el juego. Es estática, lo que
    //significa que su valor es compartido entre todas las instancias de la clase CameraFollow.
    public static Vector2 LimitsY = new Vector2(-0.594f, -1.636f);

    //Esta variable serializada permite ajustar los límites verticales de la cámara en el Inspector de Unity.
    //A diferencia de la variable estática LimitsY, esta es específica de cada instancia de la clase CameraFollow.
    [SerializeField]
    public Vector2 limitsY = new Vector2(-0.594f, -1.636f);//

    public float offset;//Este es el desplazamiento horizontal de la cámara con respecto al objetivo.
    public Transform target;//Este es el objeto que la cámara sigue.

    /// <summary>
    /// Establece los límites verticales de la cámara (LimitsY) con los valores definidos en limitsY.
    /// </summary>
    void Start()
    {
        LimitsY = limitsY;
    }

    /// <summary>
    /// Actualiza la posición de la cámara en función de la posición horizontal del objetivo (PosicionEnemigo). 
    /// La posición vertical de la cámara no se actualiza en este método, lo que implica que la cámara 
    /// sigue al objetivo solo en el eje X.
    /// </summary>
    void Update()
    {
        if (!target)
            return;
        transform.position = new Vector3(offset + target.position.x,
            transform.position.y,
            transform.position.z);
    }
}
