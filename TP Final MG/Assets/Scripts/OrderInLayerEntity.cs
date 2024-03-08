using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script se encarga de ajustar dinámicamente el orden en capas del componente SpriteRenderer del GameObject 
/// al que está adjunto, basado en su posición Y, lo que puede ser útil para garantizar que los objetos se muestren 
/// correctamente en relación con otros objetos en la escena, especialmente cuando están en diferentes alturas.
/// </summary>
//Este atributo indica que el script también se ejecutará en el Editor de Unity, no solo en tiempo de ejecución del juego.
//Esto permite visualizar los cambios en el orden en capas directamente en el Editor de Unity
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
//Este atributo garantiza que el componente SpriteRenderer esté adjunto al mismo GameObject que este script. Si no está presente, Unity lo agregará automáticamente.
public class OrderInLayerEntity : MonoBehaviour 
{
    SpriteRenderer _SpriteRenderer;//Es una referencia al componente SpriteRenderer adjunto al GameObject

	/// <summary>
	/// Obtiene la referencia al componente SpriteRenderer adjunto al GameObject.
	/// </summary>
	void Start () 
	{
		_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Calcula el orden en capas basado en la posición Y del GameObject (transform.position.y), multiplicando por 
	/// un valor grande para asegurar que el orden en capas sea único y efectivo. Establece el orden en capas del 
	/// componente SpriteRenderer (_SpriteRenderer.sortingOrder) como el valor calculado, pero negativo para invertir el orden 
	/// (mayor Y = menor orden en capas).
	/// </summary>
	void Update () 
	{
		_SpriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
	}
}
