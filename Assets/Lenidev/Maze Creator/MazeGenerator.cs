using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class MazeGenerator : MonoBehaviour
{
    public int mazeSize;
    public int pathWidth;
    public int wallWidth;
    Stack<CellBlock> mazeNavStack;
    CellBlock[] mazeGrid;
    List<CellBlock> visited;

    public CellBlock cell;
    private CellBlock current;

    public float delayTimer = 0.0f;
    void Start()
    {
        visited = new List<CellBlock>();
        mazeNavStack = new Stack<CellBlock>(mazeSize*mazeSize);
        mazeGrid = new CellBlock[mazeSize*mazeSize];

        int currCellIndex = 0;
        for (int z = 0; z <mazeSize; z++)
        {
            for (int x = 0; x<mazeSize; x++)
            {
                //await Task.Delay((int)(delayTimer * 1000f));
                CellBlock cellInstance = Instantiate(cell, new Vector3(x*10, 0, z*10), Quaternion.identity);
                cellInstance.x= x;
                cellInstance.z= z;
                mazeGrid[currCellIndex] = cellInstance;
                currCellIndex++;
            }

        }

        MakeMaze();
    }



    async void MakeMaze()
    {
        int x, z;
        x = z = 0;

        var curr = mazeGrid[0];
        Dictionary<CellBlock, CellBlock.WallDirection> directionalNeighbours = GetDirectionalNeighbors(x, z);

        while (visited.Count !=  mazeGrid.Length)
        {
            await Task.Delay((int)(delayTimer * 1000f));
            curr.visited = true;
            
            
            directionalNeighbours= GetDirectionalNeighbors(curr.x, curr.z);
            if (directionalNeighbours.Count > 0)
            {
                mazeNavStack.Push(curr);
                visited.Add(curr);
                Tuple<CellBlock, CellBlock.WallDirection> dict = GetRandomNeighbourFromDictionary(directionalNeighbours);
                CellBlock nextNeighbor = dict.Item1;

                // Direction the neighbour lies relative to the current cell
                CellBlock.WallDirection dir = dict.Item2;
                switch (dir)
                {
                    case CellBlock.WallDirection.NORTH:
                        {
                            curr.AddPathDirection(dir);
                            curr.RemoveWall(dir);
                            var opp = CellBlock.WallDirection.SOUTH;
                            nextNeighbor.AddPathDirection(opp);
                            nextNeighbor.RemoveWall(opp);
                        }
                        break;case CellBlock.WallDirection.EAST:
                        {
                            curr.RemoveWall(dir);
                            curr.AddPathDirection(dir);
                            var opp = CellBlock.WallDirection.WEST;
                            nextNeighbor.AddPathDirection(opp);
                            nextNeighbor.RemoveWall(opp);
                        }
                        break;case CellBlock.WallDirection.SOUTH:
                        {
                            curr.RemoveWall(dir);
                            curr.AddPathDirection(dir);
                            var opp = CellBlock.WallDirection.NORTH;
                            nextNeighbor.AddPathDirection(opp);
                            nextNeighbor.RemoveWall(opp);
                        }
                        break;case CellBlock.WallDirection.WEST:
                        {
                            curr.RemoveWall(dir);
                            curr.AddPathDirection(dir);
                            var opp = CellBlock.WallDirection.EAST;
                            nextNeighbor.AddPathDirection(opp);
                            nextNeighbor.RemoveWall(opp);
                        }
                        break;
                }
                curr = nextNeighbor;

            }
            else if (directionalNeighbours.Count == 0 & mazeNavStack.Count > 0) {
                curr = mazeNavStack.Pop();
                continue;
            }
            

        }

    }


    // GEM Formula --> https://www.youtube.com/watch?v=D8UgRyRnvXU
    /// <summary>
    /// Find Neighbour index in a 1D array
    /// </summary>
    /// <param horizontalPositioninGrid="x"></param>
    /// <param verticalPositioninGrid="z"></param>
    /// <returns>An integer which is the index of neighbour in the 1D array</returns>
    int _getNeighbourIndex(int x, int z)
    {
        if (x < 0 || z < 0 || x > mazeSize - 1 || z > mazeSize - 1)
        {
            return - 1;
        }

        return x + z * mazeSize;
    }


    Dictionary<CellBlock, CellBlock.WallDirection> GetDirectionalNeighbors(int x, int z)
    {
        List<CellBlock> neighbors = new List<CellBlock>();
        Dictionary<CellBlock, CellBlock.WallDirection> directionalNeighbors = new Dictionary<CellBlock, CellBlock.WallDirection>();
#nullable enable
        CellBlock? top, left, bottom, right;
        top = left = bottom = right = null;

        // The Indices are reversed because the generation of cells are starting from bottom to top.
        var top_index = _getNeighbourIndex(x, z+1);
        var left_index = _getNeighbourIndex(x-1, z);
        var bottom_index = _getNeighbourIndex(x, z-1);
        var right_index = _getNeighbourIndex(x+1, z);

        // Check Top
        if (top_index > -1)
        {
            top = mazeGrid[top_index];
        }
        // Check Left
        if (left_index > -1)
        {
            left = mazeGrid[left_index];
        }
        // Check Bottom
        if (bottom_index> -1)
        {
            bottom = mazeGrid[bottom_index];
        }
        // Check Right
        if (right_index > -1)
        {
            right = mazeGrid[right_index];
        }

        CellBlock?[] _ = new CellBlock?[] { top, left, bottom, right };
        for (var i = 0; i < _.Length; i++)
        {
            CellBlock? cell = _[i];
            if (cell!=null)
            {
                if (cell.visited)
                {
                    continue;
                }
                // Direction the neighbour lies relative to the current cell
                directionalNeighbors.Add(cell, (CellBlock.WallDirection)i);
                neighbors.Add(cell);
            }
        }
        return directionalNeighbors;
    }

    Tuple<CellBlock, CellBlock.WallDirection> GetRandomNeighbourFromDictionary(Dictionary<CellBlock, CellBlock.WallDirection> cellDict)
    {
        System.Random random = new System.Random();
        CellBlock[] cells = cellDict.Keys.ToArray(); 
        var i = random.Next(cells.Length);

        return Tuple.Create(cellDict.ElementAt(i).Key, cellDict.ElementAt(i).Value);
    }
}
