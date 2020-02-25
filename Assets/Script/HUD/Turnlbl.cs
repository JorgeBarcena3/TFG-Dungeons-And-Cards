﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turnlbl : MonoBehaviour
{

    /// <summary>
    /// Texto que se debe cambiar
    /// </summary>
    public Text texto;

    /// <summary>
    /// Animator de la UI
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Animator de la UI
    /// </summary>
    public AnimationClip myAnimation;

    /// <summary>
    /// Se cambia el texto
    /// </summary>
    public void showTurn()
    {
        if (GameManager.GetInstance().turn == TURN.IA)
            texto.text = "ENEMIGOS";
        else
            texto.text = "JUGADOR";

        anim.SetTrigger("Show");
    }

    /// <summary>
    /// Devuelve el tiempo de la animacion
    /// </summary>
    /// <returns></returns>
    public float getTimeAnimation()
    {
        return myAnimation.length * 2;
    }
}