using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Modelo de datos de la tabla score
/// </summary>
public class scoreDto
{
    /// <summary>
    /// Nombre
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Puntuacion
    /// </summary>
    string Score { get; set; }

    /// <summary>
    /// Fecha
    /// </summary>
    DateTime Date { get; set; }
}

