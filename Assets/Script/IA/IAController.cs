using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Controlador de la IA
/// </summary>
public class IAController : MonoBehaviour
{
    /// <summary>
    /// Toma las decisiones en función de los parámetros que le pasamos
    /// </summary>
    [HideInInspector]
    public Decider decider;

    /// <summary>
    /// Si la accion se ha acabado o no
    /// </summary>
    [HideInInspector]
    public bool actionDone = false;

    /// <summary>
    /// Inicializacion del control de la IA
    /// </summary>
    public void init()
    {
        decider = this.gameObject.AddComponent<Decider>();
    }

    /// <summary>
    /// La IA realiza una accion
    /// </summary>
    public IEnumerator doAction(List<IAAgent> agents)
    {
        actionDone = false;

        GameManager GM = GameManager.GetInstance();

        agents = agents.OrderBy(m => Vector2.Distance(m.transform.position, GM.player.transform.position)).ToList();

        foreach (IAAgent agent in agents)
        {
            agent.actionDone = false;

            List<Tile> waypoints = PathFindingHexagonal.calcularRuta(agent.currentCell, agent.target.currentCell);

            //MOVEMOS LA CAMARA HACIA EL ENEMIGO

            GM.cameraFunctions.moveCameraTo(agent.transform.position, 5f);

            yield return new WaitUntil(() =>
                GM.cameraFunctions.transform.position.x == agent.transform.position.x &&
                GM.cameraFunctions.transform.position.y == agent.transform.position.y
                );

            //ELEGIMOS LA ACCION A HACER

            StartCoroutine(decider.takeDecision(agent, new IAInputInfo(waypoints, 1, 8)));

            yield return new WaitUntil(() => agent.actionDone);

        }

        actionDone = true;

    }


}
