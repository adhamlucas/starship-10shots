using System.Threading;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject enemies;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Sprite playSprite, pauseSprite;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text livesTxt;
    [SerializeField] private AudioSource music, pauseSFX;

    private int points;
    private bool started;

    public static GameController Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "0";
        mainMenu.SetActive(true);
        hud.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);

        pauseBtn.onClick.AddListener(Pause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (!started) StartGame();
            else if (gameOver.activeSelf) SceneManager.LoadScene(0);
        } 
    }

    void StartGame ()
    {
        started = true;
        mainMenu.SetActive(false);
        hud.SetActive(true);
        enemies.SetActive(true);
        ship.SetActive(true);
    }

    public void UpdateScore (int _points) 
    {
        points += _points;
        scoreTxt.text = points.ToString();
    }

    public void UpdateLives (int lives)
    {
        livesTxt.text = "x" + lives;
    }

    private void Pause ()
    {
        bool paused = pause.activeSelf;
        paused = !paused;
        pause.SetActive(paused);

        pauseSFX.Play();
        music.volume = paused ? 0.05f : 1f;

        Time.timeScale = paused ? 0 : 1;
        pauseBtn.image.sprite = paused ? playSprite : pauseSprite;
    }

    public void GameOver ()
    {
        hud.SetActive(false);
        gameOver.SetActive(true);
    }
}
