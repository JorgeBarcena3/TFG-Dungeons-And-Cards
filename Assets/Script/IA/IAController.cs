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
    /// Si la accion se ha acabado o no
    /// </summary>
    public bool actionDone = false;

    /// <summary>
    /// La IA realiza una accion
    /// </summary>
    public IEnumerator doAction(List<IAAgent> agents)
    {
        actionDone = false;

        GameManager GM = GameManager.GetInstance();

        foreach (IAAgent agent in agents)
        {
            List<Tile> waypoints = PathFindingHexagonal.calcularRuta(agent.currentCell, agent.target.currentCell);

            Debug.Log(agent.gameObject.name + " " + waypoints.Count);
            //MOVEMOS LA CAMARA HACIA EL ENEMIGO

            GM.cameraFunctions.moveCameraTo(agent.transform.position);
 
            yield return new WaitUntil(() => 
                GM.cameraFunctions.transform.position.x == agent.transform.position.x &&
                GM.cameraFunctions.transform.position.y == agent.transform.position.y 
                );

            //ELEGIMOS LA ACCION A HACER

            //REALIZAMOS LA ACCION

            Vector3 newPosition = waypoints.FirstOrDefault().GetPosition();

            newPosition.z = agent.transform.position.z;

            StartCoroutine(AuxiliarFuncions.MoveObjectTo(agent.transform, newPosition, 10f));

            yield return new WaitUntil(() =>
                agent.transform.position.x == newPosition.x &&
                agent.transform.position.y == newPosition.y
                );


            agent.currentCell.contain = CELLCONTAINER.EMPTY;
            agent.currentCell = waypoints.First();
            agent.currentCell.contain = CELLCONTAINER.ENEMY;


        }

        actionDone = true;

    }
}
