/*
	CSVReader by Dock. (24/8/11)
	http://starfruitgames.com
 
	usage: 
	CSVReader.SplitCsvGrid(textString)
 
	returns a 2D string array. 
 
	Drag onto a gameobject for a demo of CSV parsing.
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// CSV Reader
/// </summary>
public class CSVReader : MonoBehaviour
{
    private List<InfoCard> cardsInfo = new List<InfoCard>();
    public TextAsset csvFile;
    public void Start()
    {
        string[,] grid = SplitCsvGrid(csvFile.text);

        for (int i = 1; i < grid.GetUpperBound(1); i++)
        {
            if (grid[0, i] != null)
            {
                string info = grid[0, i];
                string[] parametres = info.Split(';');
                cardsInfo.Add(new InfoCard((ATTACKTYPE)int.Parse(parametres[0]), int.Parse(parametres[1]), parametres[2],
                    parametres[3], int.Parse(parametres[4]), int.Parse(parametres[5]), int.Parse(parametres[6]), int.Parse(parametres[7]), parametres[8], parametres[9] == "1" ? true : false));
            }

        }

    }

    // outputs the content of a 2D array, useful for checking the importer
    static public void DebugOutputGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {

                textOutput += grid[x, y];
                textOutput += "|";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }

    // splits a CSV file into a 2D string array
    static public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    // splits a CSV row 
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
    public List<InfoCard> getCardsInfo() 
    {
        return cardsInfo;
    }
}