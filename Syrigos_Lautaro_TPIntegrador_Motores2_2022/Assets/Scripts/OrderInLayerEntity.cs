using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script ajusta dinámicamente el orden en capas del componente SpriteRenderer del GameObject al que está adjunto, 
/// basado en su posición Y, lo que puede ser útil para garantizar que los objetos se muestren correctamente en relación 
/// con otros objetos en la escena, especialmente cuando están en diferentes alturas.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class OrderInLayerEntity : MonoBehaviour {

    SpriteRenderer sr;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        sr.sortingOrder = -(int)(transform.position.y * 100);
	}
}
