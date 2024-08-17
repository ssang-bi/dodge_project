using UnityEngine;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; } = false;
    private static Managers _instance = null;
    private static Managers Instance 
    { 
        get 
        {
            Init();
            return _instance; 
        } 
    }

    private ResourceManager _resource = new ResourceManager();
    private PoolManager _pool = new PoolManager();
    private SceneLoadManager _scene = null;
    private UIManager _ui = new UIManager();
    private ObjectManager _object = new ObjectManager();
    private GameManager _game = new GameManager();
    private BackendManager _backend = null;
    private AdsManager _ads = new AdsManager();
    private SoundManager _sound = new SoundManager();

    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static SceneLoadManager Scene { get { return Instance?._scene; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static ObjectManager Object { get { return Instance?._object;}}
    public static GameManager Game { get { return Instance?._game; } }
    public static BackendManager Backend { get { return Instance?._backend; } }
    public static AdsManager Ads { get { return Instance?._ads; } }
    public static SoundManager Sound { get { return Instance?._sound; } }

    private static bool _isApplicationQuit = false;

    public static void Init()
    {
        if (_isApplicationQuit)
            return;
                
        if (_instance == null && Initialized == false)
        {
            Initialized = true;
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
                
                var fadeScreen = Resources.Load<GameObject>("FadeScreen");
                Instantiate(fadeScreen, go.transform);
            }

            DontDestroyOnLoad(go);
            
            _instance = go.GetComponent<Managers>();
            _instance._scene = go.AddComponent<SceneLoadManager>();
            _instance._backend = go.AddComponent<BackendManager>();
            _instance._backend.Init();
            _instance._sound.Init();

            Ads.Init();
        }
    }

    private void OnApplicationQuit()
    {
        _isApplicationQuit = true;
    }

    public static void Clear()
    {
        UI.Clear();
        Pool.Clear();
        Object.Clear();
    }
}
