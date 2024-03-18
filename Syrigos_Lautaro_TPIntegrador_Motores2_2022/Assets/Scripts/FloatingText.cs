using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script es un componente que se adjunta a un GameObject para mostrar texto flotante en el juego. Puede ser utilizado 
/// para mostrar información temporal que debe ser visible para el jugador durante un corto período de tiempo.
/// </summary>
[RequireComponent(typeof(TextMesh))]
public class FloatingText : MonoBehaviour
{

    [SerializeField]
    float speed;
    
    [SerializeField]
    float lifeTime;

    TextMesh tm;

    void Awake()
    {
        tm = GetComponent<TextMesh>();
        Destroy(gameObject, lifeTime);
    }

    public void Init(string text)
    {
        tm.text = text;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
