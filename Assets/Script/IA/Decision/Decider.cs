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

    //WAYPOINTS      BASIC MEDIUM  HARD    -- TIPO DE ENEMIGO
    //        1	      0	     0	   0 / 1 
    //        2	      1	     0	   0 / 1
    //        3	      1	     1	   3
    //        4	      1	     1	   2
    //        5	      1	     1	   2
    //        >= 6	  1	     1	   2


    public int[,] primaryDecisionOptions = new int[,]
    { {  1,  0,0,0},
      {  2,  1,0,0},
      {  3,  1,1,0},
      {  4,  1,1,1},
      {  5,  2,2,1},
      {  6,  2,2,1},
      {  7,  2,2,1},
      {  8,  2,2,1},
      {  9,  2,2,2},
      { 10,  3,2,2},
    };

    /// <summary>
    /// De descarga la acciones de firbase
    /// </summary>
    public async void getActionsFromFirebase()
    {
        try
        {
            DeciderInfoDto info = await FirebaseDatabaseManager.Instance.get<DeciderInfoDto>("IA/Decider");
            primaryDecisionOptions = info.toArray();
            print("Acciones de la IA from Firebase");
        }
        catch(Exception e)
        {

            
            primaryDecisionOptions = new int[,]
                    { {  1,  0,0,0},
                      {  2,  1,0,0},
                      {  3,  1,1,0},
                      {  4,  1,1,1},
                      {  5,  2,2,1},
                      {  6,  2,2,1},
                      {  7,  2,2,1},
                      {  8,  2,2,1},
                      {  9,  2,2,2},
                      { 10,  3,2,2},
                    };
        }
    }

    /// <summary>
    /// Sube las acciones a firebase
    /// </summary>
    public void setActionsFromFirebase()
    {
        try
        {
            var obj = new DeciderInfoDto(primaryDecisionOptions);
            FirebaseDatabaseManager.Instance.addOrUpdate<DeciderInfoDto>("IA", "Decider", obj);

        }
        catch (Exception e)
        {
            print(e);
        }
    }

    /// <summary>
    /// Toma una decision en funcion de los parametros del input
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="info"></param>
    public IEnumerator takeDecision(IAAgent agent, IAInputInfo info)
    {
        Enemy enemy = (agent as Enemy);

        int actionIndex = primaryDecisionOptions[info.waypointsToPlayer.Count() - 1 >= primaryDecisionOptions.GetLength(0) ? primaryDecisionOptions.GetLength(0) - 1 : info.waypointsToPlayer.Count() - 1, (int)enemy.type + 1];

        //Utilizamos un árbol de toma de decisiones

        var card = enemy.cardsActives[actionIndex];
        var action = card.gameObject.GetComponent<CardAction>();
        action.setActor(enemy.gameObject);

        int index = info.waypointsToPlayer.Count > card.GetComponent<Card>().info.Power ? card.GetComponent<Card>().info.Power : info.waypointsToPlayer.Count;

        if (action.checkAction())
        {

            var tilee = action.recommendTile(); //info.waypointsToPlayer[index - 1]
            action.clickOnTile(tilee);
            yield return new WaitForSeconds(1F);
        }

        GameManager.Instance.checkEndGame();

        agent.actionDone = true;


    }

}
