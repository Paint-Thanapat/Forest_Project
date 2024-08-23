using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapGrid : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public struct MapObstacleObject
    {
        public GameObject obstacle;
        public Animator obstacleAnim;
    }

    public bool IsObstacleActive { get; private set; } = false;
    public MapObstacleObject mainObstacle { get; private set; }
    [SerializeField] private MapObstacleObject[] obstacles;
    public int gridID;

    private void Start()
    {
        InitializeObstacles();
    }

    private void InitializeObstacles()
    {
        foreach (var mapObs in obstacles)
        {
            mapObs.obstacle.SetActive(false);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            int randomMainObstacle = Random.Range(0, obstacles.Length);

            SetMainObstacle(randomMainObstacle);

            NetworkMapManager.SendSelectMainObstacle(gridID, randomMainObstacle);
        }
    }

    public void SetMainObstacle(int obsIndex)
    {
        mainObstacle = obstacles[obsIndex];
    }

    public void EnableMainObstacle()
    {
        if (IsObstacleActive) return;

        IsObstacleActive = true;

        mainObstacle.obstacle.SetActive(true);
    }


}
