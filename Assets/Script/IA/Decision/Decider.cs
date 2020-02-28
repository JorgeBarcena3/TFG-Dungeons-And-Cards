using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Decide si hacer o no una accion
/// </summary>
public class Decider : MonoBehaviour
{


    /// <summary>
    /// Toma una decision en funcion de los parametros del input
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="info"></param>
    public IEnumerator takeDecision(IAAgent agent, IAInputInfo info)
    {
        //REALIZAMOS LA ACCION
        if (info.waypointsToPlayer.Count > 1)
        {
            Enemy enemy = (agent as Enemy);
            int index = enemy ? (agent as Enemy).getAvance() : 0;

            if (info.waypointsToPlayer[index].contain != CELLCONTAINER.ENEMY)
            {

                for (int i = 0; i <= index; i++)
                {
                    Vector3 newPosition = info.waypointsToPlayer[i].transform.position;
                    enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x, enemy.transform.localPosition.y, -15);

                    StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(agent.transform, newPosition, 1f));

                    yield return new WaitUntil(() =>
                        agent.transform.position.x == newPosition.x &&
                        agent.transform.position.y == newPosition.y
                        );
                }

                enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x, enemy.transform.localPosition.y, info.waypointsToPlayer[index].transform.localPosition.z) + enemy.zOffset;

                if (info.waypointsToPlayer[index].contain == CELLCONTAINER.PLAYER)
                {
                    Debug.Log("Te han matado los enemigos");
                    GameManager.Instance.resetScene();
                }

                agent.currentCell.contain = CELLCONTAINER.EMPTY;
                agent.currentCell = info.waypointsToPlayer[index];
                agent.currentCell.contain = CELLCONTAINER.ENEMY;
            }

        }
        else if (info.waypointsToPlayer.Count == 1)
        {
            Debug.Log("Te han matado los enemigos");
            GameManager.Instance.resetScene();
        }

        agent.actionDone = true;


    }

}
