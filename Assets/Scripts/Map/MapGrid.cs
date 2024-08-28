using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;

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
    [SerializeField] float delayTriggerTime;

    List<Player> players = new List<Player>();

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

        // * Fix Use Only Block
        SetMainObstacle(0);

        // * Random and Send to other Client
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     int randomMainObstacle = Random.Range(0, obstacles.Length);

        //     SetMainObstacle(randomMainObstacle);

        //     NetworkMapManager.SendSelectMainObstacle(gridID, randomMainObstacle);
        // }
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

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(OnTriggerAttack());

            IEnumerator OnTriggerAttack()
            {
                yield return new WaitForSeconds(delayTriggerTime);

                TriggerDamage();
            }
        }
    }

    public void TriggerDamage()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].PV.IsMine)
                players[i].ChangeState(players[i].movementStateMachine.stopState);
            else
                NetworkGameplayManager.SendForceStopState(players[i].PV.ViewID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Player temp_player = other.gameObject.GetComponent<Player>();
            if (!players.Contains(temp_player))
            {
                players.Add(temp_player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Player temp_player = other.gameObject.GetComponent<Player>();
            if (players.Contains(temp_player))
            {
                players.Remove(temp_player);
            }
        }
    }
}
