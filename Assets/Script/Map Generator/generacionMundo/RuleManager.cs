using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manager de las reglas del algoritmo del cellular atomata
/// </summary>
public class RuleManager
{
    /// <summary>
    /// Radio por el que se calcularan los vecinos
    /// </summary>
    public string RuleGeneration { get; private set; }

    /// <summary>
    /// Survive char
    /// </summary>
    private char S_prefix { get; set; }

    /// <summary>
    /// Born char
    /// </summary>
    private char B_prefix { get; set; }

    /// <summary>
    /// Reglas para las celulas vivas
    /// </summary>
    List<int> SurviveRules { get; set; }

    /// <summary>
    /// Reglas para las celulas que deben nacer
    /// </summary>
    List<int> BornRules { get; set; }

    public RuleManager(string _ruleGeneration, char _S_prefix, char _B_prefix)
    {

        this.RuleGeneration = _ruleGeneration;
        this.S_prefix = _S_prefix;
        this.B_prefix = _B_prefix;

        SurviveRules = GetRules(S_prefix); //Las matamos
        BornRules = GetRules(B_prefix); //Las crecemos
    }

    /// <summary>
    /// Obtenemos las reglas segun determinemos en los parametros
    /// </summary>
    /// <param name="index">Prefijo de cada zona</param>
    /// <param name="separator">Separador de reglas</param>
    /// <returns></returns>
    private List<int> GetRules(char index, char separator = '/')
    {
        List<string> rules = this.RuleGeneration.Split(separator).OfType<string>().ToList();

        string surviveRules = rules.Where(m => m.Contains(index)).FirstOrDefault();

        if (surviveRules == null)
        {
            if (index == 'B')
            {
                surviveRules = rules[1];
            }
            else
            {
                surviveRules = rules[0];

            }
        }

        List<int> surviveRulesList = new List<int>();

        if (surviveRules != null)
        {
            for (int i = 0; i < surviveRules.Length; ++i)
            {
                if ((surviveRules[i] != index))
                {
                    surviveRulesList.Add(int.Parse(surviveRules[i].ToString()));
                }
            }

        }

        return surviveRulesList;
    }

    /// <summary>
    /// Aplicamos las reglas de supervivencia
    /// </summary>
    /// <param name="parent_board">Tablero de referencia</param>
    /// <param name="cell">Celda actual</param>
    /// <returns></returns>
    public Cell ApplyRules(Cell cell)
    {
        Cell next_cell = new Cell (cell);
        next_cell.Color = next_cell.Value == CELLSTYPE.DEAD ? Color.black : Color.white;

        if (next_cell.Value == CELLSTYPE.DEAD)
        {
            if (BornRules.Contains(next_cell.CountNeighborsAlive))
            {
                next_cell = new Cell(next_cell.CellInfo.x, next_cell.CellInfo.y, CELLSTYPE.ALIVE);
                next_cell.Color = Color.blue;

            }
        }
        else if (next_cell.Value == CELLSTYPE.ALIVE)
        {
            if (SurviveRules.Contains(next_cell.CountNeighborsAlive))
            {
                next_cell = new Cell(next_cell.CellInfo.x, next_cell.CellInfo.y, CELLSTYPE.ALIVE);
            }
            else
            {
                next_cell = new Cell(next_cell.CellInfo.x, next_cell.CellInfo.y, CELLSTYPE.DEAD);
                next_cell.Color = Color.red;

            }

        }


        return next_cell;
    }
}