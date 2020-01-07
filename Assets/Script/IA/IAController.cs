using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
  
    /// <summary>
    /// La IA realiza una accion
    /// </summary>
    public void doAction(List<IAAgent> agents)
    {

        foreach (IAAgent agent in agents)
        {

            Vector2 distance = agent.transform.position - agent.target.transform.position;
            //DETERMINAR DIRECCION
            //MOVER ENEMIGO
        }


    }
}
