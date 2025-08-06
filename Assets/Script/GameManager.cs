using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.LightTransport;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _gridArea;
    [SerializeField] private RectTransform gameoverPanel;
    private Player player;
    private Health health;
    private ObjectPool pool;
    private SpawnManager spawnManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        player = _player.gameObject.GetComponent<Player>();
        health = _player.gameObject.GetComponent<Health>();
        spawnManager = _gridArea.gameObject.GetComponent<SpawnManager>();
        ResetGame();
    }

    public void ResetGame()
    {
        ObjectPool.instance.ReturnAllBullets();
        health.resetHeart();
        spawnManager.ResetStage();
        gameoverPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void Gameover()
    {
        Time.timeScale = 0;
        gameoverPanel.gameObject.SetActive(true);
    }
}
