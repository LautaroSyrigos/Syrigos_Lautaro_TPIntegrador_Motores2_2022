using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script se encarga de coordinar el control del jugador, la interacción con el entorno 
/// del juego y la gestión de su estado y animaciones.
/// </summary>
public class Player : Fighter
{
    Vector2 cntrl;//Esta variable almacena las entradas del jugador en los ejes horizontal y vertical.

    /// <summary>
    /// * cntrl se actualiza con las entradas del jugador en los ejes horizontal y vertical utilizando Input.GetAxis("Horizontal") y Input.GetAxis("Vertical").
    /// * Si el jugador presiona la tecla Z, se inicia la corrutina Punch().
    /// * La variable EstaDefendiendose se establece según si el jugador mantiene presionada la tecla X.
    /// * Se actualiza el valor del parámetro "IsGuard" en el animador (Animator) para controlar la animación del jugador.
    /// * Si el jugador no está realizando una animación de puñetazo, recibiendo un puñetazo o guardándose, se actualiza la animación de caminar, la velocidad del 
    /// Rigidbody (RigidBody.velocity) y la posición del jugador para que permanezca dentro de ciertos límites verticales de la cámara. Si el jugador está realizando alguna de esas animaciones, la velocidad del Rigidbody se establece en cero para detener cualquier movimiento.
    /// </summary>
    void Update()
    {
        cntrl = new Vector2(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        if (cntrl.x != 0)
            SpriteRenderer.flipX = cntrl.x < 0;

        if (Input.GetKeyDown(KeyCode.Z))
            StartCoroutine(Punch());

        EstaDefendiendose = Input.GetKey(KeyCode.X);

        Animator.SetBool("IsGuard", EstaDefendiendose);

        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Punch")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("GetPunch")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            Animator.SetBool("IsWalking", cntrl.magnitude != 0);
            RigidBody.velocity = new Vector2(cntrl.x * VelocidadHorizontal,
                cntrl.y * VelocidadVertical);
            transform.position = new Vector3(transform.position.x,
                Mathf.Clamp(transform.position.y, CameraFollow.LimitsY.y, CameraFollow.LimitsY.x), // Bloquea la pos Y
                transform.position.z);
        }
        else
        {
            RigidBody.velocity = Vector3.zero;
        }
    }
}
