using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script define el comportamiento del enemigo del juego, incluyendo su capacidad de patrullar, detectar y perseguir
/// al jugador, así como realizar ataques cuando está en modo de persecución.
/// </summary>
public class Enemy : Fighter
{
    /// <summary>
    /// Define dos estados posibles para el enemigo: Patrulla y Persecucion.
    /// </summary>
    enum Estados { Patrulla, Persecucion }

    [SerializeField] Estados EstadoEnemigo = Estados.Patrulla;//El estado actual del enemigo, que puede ser Patrulla o Persecucion.
    [SerializeField] float RangoBusqueda = 1;//La distancia máxima a la que el enemigo puede detectar al jugador.
    [SerializeField] float LimiteDistancia = 0.3f;//La distancia a la que el enemigo detiene su movimiento al llegar al objetivo.
    [SerializeField] int AgregarPuntuacion;//La cantidad de puntos que el jugador obtiene al derrotar a este enemigo.
    [SerializeField]
    FloatingText TextoFlotantePuntos;//Una referencia al prefab de texto flotante que se muestra cuando el jugador obtiene puntos al derrotar a este enemigo.

    Transform JugadorTransform;//Referencia al Transform del jugador.
    Vector3 PosicionEnemigo;//La posición a la que el enemigo se está moviendo actualmente.

    /// <summary>
    /// Busca al jugador utilizando GameObject.FindGameObjectWithTag("Player"). 
    /// Invoca repetidamente los métodos SetTarget() y SendPunch().
    /// </summary>
    void Start()
    {
        JugadorTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SetTarget", 0, 5);
        InvokeRepeating("SendPunch", 0, 5);
    }

    /// <summary>
    /// Instancia un prefab de texto flotante y actualiza la puntuación del jugador cuando el enemigo es derrotado.
    /// </summary>
    public void AddScore()
    {
        var go = Instantiate(TextoFlotantePuntos, transform.position, TextoFlotantePuntos.transform.rotation);
        go.Init(string.Format("<b>{0}</b>", AgregarPuntuacion.ToString()));
        ScoreManager.Singleton.Puntuacion += AgregarPuntuacion;
    }

    /// <summary>
    /// Dibuja gizmos para mostrar el rango de búsqueda del enemigo y el destino actual.
    /// </summary>
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangoBusqueda);
        Gizmos.DrawWireSphere(PosicionEnemigo, 0.2f);

        base.OnDrawGizmosSelected();
    }

    /// <summary>
    /// Inicia un ataque (puñetazo) si el enemigo está en modo de persecución y no está en movimiento.
    /// </summary>
    void SendPunch()
    {
        if (EstadoEnemigo != Estados.Persecucion)
            return;
        if (VelocidadEnemigo.magnitude != 0)
            return;
        StartCoroutine(Punch());
    }

    /// <summary>
    /// Establece un nuevo destino aleatorio dentro del rango de búsqueda si el enemigo está en modo de patrulla.
    /// </summary>
    void SetTarget()
    {
        if (EstadoEnemigo != Estados.Patrulla)
            return;
        PosicionEnemigo = new Vector2(transform.position.x + Random.Range(-RangoBusqueda, RangoBusqueda)
            , Random.Range(CameraFollow.LimitsY.y, CameraFollow.LimitsY.x));
    }

    Vector2 VelocidadEnemigo;

    /// <summary>
    /// * Actualiza el comportamiento del enemigo dependiendo de su estado actual.
    /// * Si está en modo de persecución, sigue al jugador y cambia al modo de patrulla si el jugador está fuera del rango de búsqueda.
    /// * Si está en modo de patrulla, busca al jugador y cambia al modo de persecución si lo detecta.
    /// * Controla el movimiento del enemigo y su orientación según su velocidad y estado de animación.
    /// </summary>
    void Update()
    {
        if (EstadoEnemigo == Estados.Persecucion)
        {
            PosicionEnemigo = JugadorTransform.transform.position;
            if (Vector3.Distance(PosicionEnemigo, transform.position) > RangoBusqueda * 1.2f)
            {
                PosicionEnemigo = transform.position;
                EstadoEnemigo = Estados.Patrulla;
                return;
            }
        }
        else if (EstadoEnemigo == Estados.Patrulla)
        {
            var Objetivo = Physics2D.CircleCast(transform.position, RangoBusqueda, Vector2.up);
            if (Objetivo.collider != null)
            {
                if (Objetivo.collider.CompareTag("Player"))
                {
                    EstadoEnemigo = Estados.Persecucion;
                    return;
                }
            }
        }
        VelocidadEnemigo = PosicionEnemigo - transform.position;
        SpriteRenderer.flipX = VelocidadEnemigo.x < 0;
        if (VelocidadEnemigo.magnitude < LimiteDistancia)
            VelocidadEnemigo = Vector2.zero;
        VelocidadEnemigo.Normalize();
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Punch")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("GetPunch")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            Animator.SetBool("IsWalking", VelocidadEnemigo.magnitude != 0);
        }
        else
        {
            VelocidadEnemigo = Vector2.zero;
        }

        RigidBody.velocity = new Vector2(VelocidadEnemigo.x * VelocidadHorizontal, VelocidadEnemigo.y * VelocidadVertical);
    }
}
