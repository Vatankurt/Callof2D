using Photon.Pun;
using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
       PhotonNetwork.Instantiate(PlayerPrefab.name, GetNewSpawnPosition(), Quaternion.identity);
    }

    public Vector2 GetNewSpawnPosition()
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}