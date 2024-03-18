using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estos son tipos de eventos personalizados que heredan de UnityEvent y se utilizan para definir eventos personalizados que 
/// pueden ser invocados y escuchados en el Editor de Unity.
/// </summary>
[System.Serializable]
public class MyEvent : UnityEngine.Events.UnityEvent
{
}
[System.Serializable]
public class MyStringEvent : UnityEngine.Events.UnityEvent<string>
{
}

/// <summary>
/// Este script proporciona una base para personajes en un juego que pueden recibir daño, defenderse y realizar ataques, 
/// con la capacidad de invocar eventos personalizados para notificar cambios importantes, como la muerte o 
/// el cambio de resistencia.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Fighter : MonoBehaviour
{
    [SerializeField]
    int _lifes = 3;
    protected int lifes
    {
        get { return _lifes; }
        set
        {
            _lifes = value;
            whenLifesChange.Invoke(_lifes.ToString());
        }
    }

    [SerializeField]
    int _stamina = 100;
    protected int stamina
    {
        get { return _stamina; }
        set
        {
            _stamina = value;
            whenStaminaChange.Invoke(_stamina.ToString());
        }
    }
    [SerializeField]
    protected MyEvent whenDie;
    [SerializeField]
    protected MyStringEvent whenStaminaChange;
    [SerializeField]
    protected MyStringEvent whenLifesChange;
    [SerializeField]
    protected float verticalSpeed;
    [SerializeField]
    protected float horizontalSpeed;
    [SerializeField]
    protected Transform leftPunch;
    [SerializeField]
    protected Transform rightPunch;
    [SerializeField]
    protected float punchRadius = 0.1f;

    [SerializeField]
    protected int punchDamage = 1;
    [SerializeField]
    protected int guardDamage = 10;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;

    protected bool isGuard = false;

    protected virtual void OnDrawGizmosSelected()
    {
        if (leftPunch == null || rightPunch == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(leftPunch.position, punchRadius);
        Gizmos.DrawWireSphere(rightPunch.position, punchRadius);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        whenLifesChange.Invoke(_lifes.ToString());
        whenStaminaChange.Invoke(_stamina.ToString());
    }

    void GetPunch()
    {
        print("GetPunch");
        if (isGuard)
        {
            stamina -= guardDamage;
            return;
        }
        anim.SetTrigger("GetPunch");
        lifes -= punchDamage;
        if (lifes <= 0)
            whenDie.Invoke();
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

    protected IEnumerator Punch()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetPunch")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            anim.SetTrigger("SendPunch");
            yield return new WaitForSeconds(0.8f);
            Vector2 punchPosition = sr.flipX ? leftPunch.position : rightPunch.position;
            var ob = Physics2D.CircleCast(punchPosition, punchRadius, Vector2.up);
            if (ob.collider != null)
            {
                print(ob.collider.gameObject.name);
                if (ob.collider.gameObject != gameObject)
                {
                    ob.collider.SendMessage("GetPunch");
                }
            }

        }
    }

}
