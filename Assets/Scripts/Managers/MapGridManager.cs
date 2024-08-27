using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MapGridManager : SceneSingleton<MapGridManager>
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(5, 5);
    [SerializeField] private Vector2 centerGridPos;
    [SerializeField] private float gridSizeAndDistance = 1;

    [SerializeField] private float intervalTime = 1f;
    public MapGrid[] mapGrids { get; private set; }
    public MapGrid grid;

    List<int> allGridsIndex = new List<int>();

    public override void Awake()
    {
        base.Awake();

        CreateMap();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(RandomObstacleActive());
        }
    }

    IEnumerator RandomObstacleActive()
    {
        while (allGridsIndex.Count > 0)
        {
            yield return new WaitForSeconds(intervalTime);

            int randomGridIndex = Random.Range(0, allGridsIndex.Count);

            EnableMapGridObstacle(allGridsIndex[randomGridIndex]);

            NetworkMapManager.SendEnableMapGridObstacle(allGridsIndex[randomGridIndex]);

            allGridsIndex.RemoveAt(randomGridIndex);
        }
    }

    public void EnableMapGridObstacle(int gridIndex)
    {
        mapGrids[gridIndex].EnableMainObstacle();
    }

    public void SelectGridMainObstacle(int gridIndex, int selectedObsIndex)
    {
        if (mapGrids == null) return;
        if (mapGrids[gridIndex] == null) return;

        mapGrids[gridIndex].SetMainObstacle(selectedObsIndex);
    }

    private void CreateMap()
    {
        mapGrids = new MapGrid[gridSize.x * gridSize.y];

        Vector2 startGridPos = new Vector2(-((float)gridSize.x - 1) / 2 * gridSizeAndDistance, -((float)gridSize.y - 1) / 2 * gridSizeAndDistance);

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                int index = ((gridSize.x - 1) * i) + j;

                mapGrids[index] = Instantiate(grid, new Vector3(startGridPos.x + (i * gridSizeAndDistance) + centerGridPos.x, 0, startGridPos.y + (j * gridSizeAndDistance) + centerGridPos.y), Quaternion.identity);
                mapGrids[index].transform.parent = this.transform;
                mapGrids[index].transform.localScale = Vector3.one * gridSizeAndDistance;
                mapGrids[index].gridID = index;

                allGridsIndex.Add(mapGrids[index].gridID);
            }
        }
    }
}