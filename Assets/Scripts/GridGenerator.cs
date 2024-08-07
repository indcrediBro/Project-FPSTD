using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private class GridCell
    {
        public Vector3 worldPosition;
        public bool isEmpty = true;
        public GameObject trap;
    }

    public int m_gridWidth, m_gridHeight;
    public float m_cellSize;
    public GameObject m_floorPrefab;
    private GridCell[,] m_grid;

    void Start()
    {
        m_grid = new GridCell[m_gridWidth, m_gridHeight];
        for (int x = 0; x < m_gridWidth; x++)
        {
            for (int y = 0; y < m_gridHeight; y++)
            {
                m_grid[x, y] = new GridCell();
                Vector3 cellPosition = new Vector3(x * m_cellSize, 0, y * m_cellSize);
                m_grid[x, y].worldPosition = cellPosition;

                GameObject floor = Instantiate(m_floorPrefab, cellPosition, Quaternion.identity, this.transform);
            }
        }
    }

    
}
