using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estos son tipos de eventos personalizados que heredan de UnityEvent y se utilizan para definir
// eventos personalizados que pueden ser invocados y escuchados en el Editor de Unity.
[System.Serializable]
public class EventosPeleadores : UnityEngine.Events.UnityEvent{}
[System.Serializable]
public class CadenaEventos : UnityEngine.Events.UnityEvent<string>{}

/// <summary>
/// Este Script proporciona una base para personajes en un juego que pueden recibir daño, defenderse y 
/// realizar ataques, con la capacidad de invocar eventos personalizados para notificar cambios importantes, 
/// como la muerte o el cambio de resistencia.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Fighter : MonoBehaviour
{
    [SerializeField] int _Vidas = 3;//Puntos de vida del personaje.
    protected int Vidas
    {
        get { return _Vidas; }
        set
        {
            _Vidas = value;
            CuandoCambiaVida.Invoke(_Vidas.ToString());
        }
    }

    [SerializeField] int _Defensa = 100;//Puntos de defensa del personaje.
    protected int Defensa
    {
        get { return _Defensa; }
        set
        {
            _Defensa = value;
            CuandoCambiaDefensa.Invoke(_Defensa.ToString());
        }
    }
    //Estos son eventos que se invocan cuando el personaje muere, cambia su resistencia y cambia sus puntos de vida, respectivamente.
    [SerializeField] protected EventosPeleadores CuandoMuere;//Evento que se invoca cuando muere el personaje.
    [SerializeField] protected CadenaEventos CuandoCambiaDefensa;//Evento que se invoca cuando cambia la resistencia del personaje.
    [SerializeField] protected CadenaEventos CuandoCambiaVida;////Evento que se invoca cuando cambian los puntos de vida.
    [SerializeField] protected float VelocidadVertical;//Velocidad de movimiento vertical.
    [SerializeField] protected float VelocidadHorizontal;//Velocidad de movimiento horizontal.
    [SerializeField] protected Transform PuñoIzquierdo;//Posicion del puño izquierdo.
    [SerializeField] protected Transform PuñoDerecho;//Posicion del puño derecho.
    [SerializeField] protected float RangoGolpe = 0.1f;//El radio del área de efecto de los puños.
    [SerializeField] protected int DañoGolpe = 1;//El daño infligido por el personaje al atacar.
    [SerializeField] protected int DañoDefensa = 10;//El daño recibido por el personaje al defenderse.

    protected Rigidbody2D RigidBody;
    protected SpriteRenderer SpriteRenderer;
    protected Animator Animator;

    protected bool EstaDefendiendose = false;

    /// <summary>
    /// Dibuja gizmos para mostrar el área de efecto de los puños cuando se selecciona el objeto en el Editor de Unity.
    /// </summary>
    protected virtual void OnDrawGizmosSelected()
    {
        if (PuñoIzquierdo == null || PuñoDerecho == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(PuñoIzquierdo.position, RangoGolpe);
        Gizmos.DrawWireSphere(PuñoDerecho.position, RangoGolpe);
    }

    /// <summary>
    /// Se utiliza para inicializar las referencias a los componentes (Rigidbody2D, SpriteRenderer y Animator) 
    /// y para invocar los eventos CuandoCambiaVida y CuandoCambiaDefensa con los valores iniciales de vida y resistencia.
    /// </summary>
    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        CuandoCambiaVida.Invoke(_Vidas.ToString());
        CuandoCambiaDefensa.Invoke(_Defensa.ToString());
    }

    /// <summary>
    /// Este método se llama cuando el personaje recibe un golpe. Si el personaje está defendiendo, disminuye su 
    /// resistencia; de lo contrario, activa la animación de recibir un golpe y disminuye sus puntos de vida. Si 
    /// los puntos de vida llegan a cero, se invoca el evento CuandoMuere.
    /// </summary>
    void GetPunch()
    {
        print("GetPunch");
        if (EstaDefendiendose)
        {
            Defensa -= DañoDefensa;
            return;
        }
        Animator.SetTrigger("GetPunch");
        Vidas -= DañoGolpe;
        if (Vidas <= 0)
            CuandoMuere.Invoke();
    }

    /// <summary>
    /// Este método destruye el objeto del personaje.
    /// </summary>
    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Este método se utiliza para realizar un ataque. Si el personaje no está realizando una animación de ataque, obtiene 
    /// la posición del puño activo, realiza una comprobación de colisión circular y envía el mensaje GetPunch() a cualquier 
    /// objeto que esté en el área de efecto del puño.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Punch()
    {
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Punch")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("GetPunch")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            Animator.SetTrigger("SendPunch");
            yield return new WaitForSeconds(0.8f);
            Vector2 GolpePosicion = SpriteRenderer.flipX ? PuñoIzquierdo.position : PuñoDerecho.position;
            var Objetivo = Physics2D.CircleCast(GolpePosicion, RangoGolpe, Vector2.up);
            if (Objetivo.collider != null)
            {
                print(Objetivo.collider.gameObject.name);
                if (Objetivo.collider.gameObject != gameObject)
                {
                    Objetivo.collider.SendMessage("GetPunch");
                }
            }
        }
    }
}
