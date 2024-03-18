using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script gestiona el puntaje del juego y proporciona una forma de acceder y actualizar el puntaje desde otras clases. 
/// También asegura que solo haya una instancia de ScoreManager en la escena y notifica a través de un evento cuando el puntaje 
/// cambia.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    int _score = 100;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            whenScoreChange.Invoke(_score.ToString());
        }
    }
    [SerializeField]
    protected MyStringEvent whenScoreChange;

    public static ScoreManager singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;

        whenScoreChange.Invoke(_score.ToString());
    }

}
