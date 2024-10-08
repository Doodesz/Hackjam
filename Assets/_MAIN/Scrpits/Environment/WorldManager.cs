using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        // To trigger the actual world switch
        if (isChangingWorld && transition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank"))
            ChangeWorld();
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        currWorld = GameObject.FindGameObjectWithTag("world");
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
        PlayerController.Instance.isIgnoringInput = false;

        // Camera shits
        /*Camera.main.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D           //Makes issues
            = GameObject.Find("Camera Confiner").GetComponent<PolygonCollider2D>();*/
        Camera.main.GetComponent<CinemachineVirtualCamera>().Follow = null;
        Camera.main.gameObject.transform.position = targetPos;
        Camera.main.GetComponent<CinemachineVirtualCamera>().Follow = PlayerController.Instance.gameObject.transform;

        currWorld = targetWorld;
        targetWorld = null;
        targetPortalName = string.Empty;
        transition.TransitionOut();
        PlayerSFX.Instance.PlayExitPortal();
    }

    Vector3 FindPortalPositionName(string portalName)
    {
        return GameObject.Find(portalName).transform.position;
    }
}
