using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script permite que la cámara siga a uno o más objetos en la escena, manteniendo su posición dentro 
/// de los límites verticales definidos y ajustando su posición horizontalmente con el valor de offset.
/// </summary>
[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public static Vector2 LimitsY = new Vector2(-0.594f, -1.636f);

    //[SerializeField]
    //public Vector2 limitsY = new Vector2(-0.594f, -1.636f);

    //public float offset;
    //public Transform target;

    //void Start()
    //{
    //    LimitsY = limitsY;
    //}

    //void Update()
    //{
    //    if (!target)
    //        return;
    //    transform.position = new Vector3(offset + target.position.x,
    //        transform.position.y,
    //        transform.position.z);
    //}

    public Transform[] targets; // Arreglo de transformaciones de los jugadores a seguir
    public Vector2 limitsY = new Vector2(-0.594f, -1.636f); // Límites verticales para la cámara
    public float offset; // Offset para ajustar la posición de la cámara

    void Start()
    {
        LimitsY = limitsY;
        if (targets.Length == 0)
        {
            Debug.LogWarning("No se han asignado jugadores para seguir en la cámara.");
        }
    }

    void LateUpdate()
    {
        if (targets.Length == 0)
            return;

        Vector3 centerPoint = GetCenterPoint(); // Obtener el punto medio entre los jugadores
        Vector3 newPosition = new Vector3(offset + centerPoint.x, transform.position.y, transform.position.z); // Calcular la nueva posición de la cámara

        // Limitar la posición vertical de la cámara
        newPosition.y = Mathf.Clamp(newPosition.y, limitsY.y, limitsY.x);

        transform.position = newPosition; // Mover la cámara a la nueva posición
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Length == 1) // Si solo hay un jugador, devolver su posición
        {
            return targets[0].position;
        }

        // Calcular el punto medio entre los jugadores
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
