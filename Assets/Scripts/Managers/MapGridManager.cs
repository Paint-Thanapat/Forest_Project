using System.Collections;
using Photon.Pun;
using UnityEngine;

public class MapGridManager : SceneSingleton<MapGridManager>
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(5, 5);
    [SerializeField] private Vector2 startGridPos;
    [SerializeField] private float gridDistance = 1;


    [SerializeField] private float intervalTime = 1f;
    public MapGrid[,] mapGrids { get; private set; }
    public MapGrid grid;

    private void Start()
    {
        CreateMap();

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(RandomObstacleActive());
        }
    }

    IEnumerator RandomObstacleActive()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalTime);

            Vector2Int randomGridIndex = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y));

            EnableMapGridObstacle(randomGridIndex);

            NetworkMapManager.SendEnableMapGridObstacle(randomGridIndex);
        }
    }

    public void EnableMapGridObstacle(Vector2Int gridIndex)
    {
        mapGrids[gridIndex.x, gridIndex.y].EnableMainObstacle();
    }

    public void SelectGridMainObstacle(int gridID, int selectedObsIndex)
    {
        Vector2Int _gridIndex = GetGridIndex(gridID);

        if (mapGrids == null) return;
        if (mapGrids[_gridIndex.x, _gridIndex.y] == null) return;

        mapGrids[_gridIndex.x, _gridIndex.y].SetMainObstacle(selectedObsIndex);
    }

    private void CreateMap()
    {
        mapGrids = new MapGrid[gridSize.x, gridSize.y];

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                mapGrids[i, j] = Instantiate(grid, new Vector3(startGridPos.x + (i * gridDistance), 0, startGridPos.y + (j * gridDistance)), Quaternion.identity);
                mapGrids[i, j].transform.parent = this.transform;
                mapGrids[i, j].gridID = (gridSize.x * i) + j;
            }
        }
    }

    public Vector2Int GetGridIndex(int gridIndex)
    {
        int temp_gridIndex = gridIndex;
        int x_count = 0;
        int y_count = 0;

        while (temp_gridIndex > gridSize.x)
        {
            temp_gridIndex -= gridSize.x;
            x_count++;
        }
        y_count = temp_gridIndex;

        return new Vector2Int(x_count, y_count);
    }
}