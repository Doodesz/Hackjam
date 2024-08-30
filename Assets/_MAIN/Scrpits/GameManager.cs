using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

public class GameManager : MonoBehaviour
{    
    [SerializeField] int shardCount;
    [SerializeField] int connectedShardCount;
    [SerializeField] Shard[] shards;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        shards = GameObject.FindObjectsOfType<Shard>(includeInactive: true);
        shardCount = shards.Length;

        if (scene.name != "Main Menu")
        {
            PlayerPrefs.SetString("LastLevel", scene.name);
            PlayerPrefs.Save();

        }
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
            EndPortal.Instance.OpenPortal();
    }
}
