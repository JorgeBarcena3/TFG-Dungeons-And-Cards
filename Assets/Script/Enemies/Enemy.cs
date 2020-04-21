using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Tipo de enemigo
/// </summary>
public enum ENEMY_TYPE : int
{
    HARD = 2,
    MEDIUM = 1,
    BASIC = 0
}

/// <summary>
/// Clase de enemigo
/// </summary>
public class Enemy : IAAgent
{
    /// <summary>
    /// Profundidad de la Z
    /// </summary>
    [HideInInspector]
    public Vector3 zOffset = new Vector3(0, 0, -0.2f);

    /// <summary>
    /// Tipo de enemigo
    /// </summary>
    public ENEMY_TYPE type;

    /// <summary>
    /// Lista de cartas disponibles para cada enemigo
    /// </summary>
    [HideInInspector]
    public List<GameObject> cardsActives = new List<GameObject>();

    /// <summary>
    /// Informacion del enemigo
    /// </summary>
    public EnemyInfo info;

    /// <summary>
    /// Funcion de inicializacion de los enemigos
    /// </summary>
    /// <param name="_currentCell"></param>
    public void init(Tile _currentCell, Player _target, int _type)
    {
        currentCell = _currentCell;
        target = _target;
        setType(_type);
        transform.position = currentCell.transform.position + zOffset;
        createEnemyDeck();
        setActorType(CELLCONTAINER.ENEMY);
        info = new EnemyInfo();

    }

    /// <summary>
    /// Crea las posibles cartas que va a tener el enemigo
    /// </summary>
    private void createEnemyDeck()
    {
        int power;

        switch (type)
        {

            case ENEMY_TYPE.BASIC:

                lifeManager.resetMaxLife(3);

                power = 1;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKACTION,
                01,
                "Ataque a distancia",
                "Cuando utilices esta carta podrás matar cualquier enemigo (sin moverte de la casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.ATTACKACTION.ToString()
                );
                cardsActives.Last().AddComponent<AttackAction>();

                power = 1;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<MovementAction>();

                power = 2;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<MovementAction>();

                power = 3;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<MovementAction>();

                break;

            case ENEMY_TYPE.MEDIUM:

                lifeManager.resetMaxLife(5);

                power = 3;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKANDMOVEMENT,
                01,
                "Ataque y movimiento",
                "Cuando utilices esta carta podrás matar cualquier enemigo (moviéndote a su casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.ATTACKANDMOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<AttackAndMovementAction>();

                power = 3;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<MovementAction>();

                power = 4;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<MovementAction>();

                break;

            case ENEMY_TYPE.HARD:

                lifeManager.resetMaxLife(7);

                power = 4;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKACTION,
                01,
                "Ataque y movimiento",
                "Cuando utilices esta carta podrás matar cualquier enemigo (moviéndote a su casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.ATTACKACTION.ToString()
                );
                cardsActives.Last().AddComponent<AttackAndMovementAction>();

                power = 2;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
                cardsActives.Last().AddComponent<MovementAction>();

                power = 3;

                cardsActives.Add(Card.instantiateCard(GameManager.Instance.deck.cardPrefab, this.transform, this.transform, null));
                cardsActives.Last().GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.TELEPORT,
                01,
                "Teleportación",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.TELEPORT.ToString()
                );
                cardsActives.Last().AddComponent<TeleportAction>();



                break;


        }
    }

    /// <summary>
    /// Determina el tipo de enemigo
    /// </summary>
    private void setType(int index)
    {
        type = (ENEMY_TYPE)Enum.Parse(typeof(ENEMY_TYPE), index.ToString());
    }

    /// <summary>
    /// Devuelve el avance del enemigo
    /// </summary>
    /// <returns></returns>
    public int getAvance()
    {
        return (int)type;
    }

    /// <summary>
    /// Realizamos chick encima del enemigo
    /// </summary>
    private void OnMouseDown()
    {
        if (!currentCell.assignedAction)
        { 
            if (!InfoBackground.IS_TRANSITION && GameManager.Instance.turn == TURN.PLAYER)
            { 
                GameManager.Instance.hud.enemyHUDManager.showInfo(this);
            }
            
        }
        else
        {
            currentCell.assignedAction.clickOnTile(currentCell);

        }

    }


}
