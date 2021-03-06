﻿
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelo de datos de las decisiones de la IA
/// </summary>
[Serializable]
public class DeciderInfoDto : DtoFirebase
{
    /// <summary>
    /// Lista de acciones
    /// </summary>
    [SerializeField]
    public List<List<int>> actions { get; set; } = new List<List<int>>();


    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public DeciderInfoDto()
    {

    }

    /// <summary>
    /// Creacion de un decider Info
    /// </summary>
    /// <param name="data"></param>
    public DeciderInfoDto(int[,] data)
    {

        for (int i = 0; i < data.GetLength(0); i++)
        {
            List<int> aux = new List<int>();

            for (int j = 0; j < data.GetLength(1); j++)
            {
                aux.Add(data[i, j]);
            }

            actions.Add(aux);
        }

    }

    /// <summary>
    /// Devuelve los vectores convertidos a un array
    /// </summary>
    /// <returns></returns>
    internal int[,] toArray()
    {

        int[,] arr = new int[actions.Count, actions[0].Count];

        for (int i = 0; i < actions.Count; i++)
        {
            for (int j = 0; j < actions[i].Count; j++)
            {
                arr[i, j] = actions[i][j];
            }
        }
        return arr;
    }
}

