﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Juego");
    }
    public void Opciones()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir");
    }

}
