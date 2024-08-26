using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [SerializeField] bool isOpen;
    [SerializeField] int shardCount;
    [SerializeField] int connectedShardCount;
    [SerializeField] Shard[] shards;

    public static EndPortal Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        shards = GameObject.FindObjectsOfType<Shard>(includeInactive: true);
        shardCount = shards.Length;
    }

    public void UpdateProgress()
    {
        connectedShardCount = 0;

        foreach (Shard shard in shards)
        {
            if (shard.isConnected)
                connectedShardCount++;
        }

        if (connectedShardCount == shardCount)
            OpenPortal();
    }

    void OpenPortal()
    {
        isOpen = true;
        Debug.Log("End Portal opened");
    }
}
