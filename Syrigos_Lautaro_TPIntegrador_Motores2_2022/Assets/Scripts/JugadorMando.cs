using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script se encarga de coordinar el control del jugador, la interacción con el entorno del juego y 
/// la gestión de su estado y animaciones. Es una parte crucial para permitir que el jugador interactúe con 
/// el juego de manera efectiva.
/// </summary>
public class JugadorMando : Fighter
{
    Vector2 cntrl;
    void Update()
    {
        cntrl = new Vector2(Input.GetAxis("Horizontal Mando"),
            Input.GetAxis("Vertical Mando"));

        if (cntrl.x != 0)
            sr.flipX = cntrl.x < 0;

        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(Punch());

        isGuard = Input.GetButtonDown("Fire2");

        anim.SetBool("IsGuard", isGuard);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetPunch")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            anim.SetBool("IsWalking", cntrl.magnitude != 0);
            rb.velocity = new Vector2(cntrl.x * horizontalSpeed,
                cntrl.y * verticalSpeed);
            transform.position = new Vector3(transform.position.x,
                Mathf.Clamp(transform.position.y, CameraFollow.LimitsY.y, CameraFollow.LimitsY.x), // Bloquea la pos Y
                transform.position.z);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }
}
