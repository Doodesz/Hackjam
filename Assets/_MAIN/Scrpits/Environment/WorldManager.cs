using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject currWorld;
    public GameObject targetWorld;

    public bool isChangingWorld;
    [SerializeField] string targetPortalName;

    ScreenTransition transition;

    public static WorldManager Instance;

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
        currWorld = GameObject.FindGameObjectWithTag("world");
        transition = ScreenTransition.Instance;

        foreach (GameObject world in GameObject.FindGameObjectsWithTag("world"))
        {
            if (world != currWorld) gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // To trigger the actual world switch
        if (isChangingWorld && transition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank"))
            ChangeWorld();
    }

    public void EnterWorld(GameObject worldToEnable, string thisPortalName)
    {
        transition.TransitionIn();
        targetWorld = worldToEnable;
        targetPortalName = thisPortalName;
        isChangingWorld = true;
        PlayerController.Instance.isIgnoringInput = true;
    }

    void ChangeWorld()
    {
        isChangingWorld = false;
        currWorld.SetActive(false);
        targetWorld.SetActive(true);

        // Moves player position
        Vector3 targetPos = FindPortalPositionName(targetPortalName);
        PlayerController.Instance.gameObject.transform.position = targetPos;
        Camera.main.gameObject.transform.position = targetPos;
        PlayerController.Instance.isIgnoringInput = false;

        currWorld = targetWorld;
        targetWorld = null;
        targetPortalName = string.Empty;
        transition.TransitionOut();
    }

    Vector3 FindPortalPositionName(string portalName)
    {
        return GameObject.Find(portalName).transform.position;
    }
}
