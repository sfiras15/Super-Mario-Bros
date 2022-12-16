using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //singelton game manager? why do we need it ? 
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int Coins { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;//fix the fps to 60
        NewGame();
    }
    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}.{stage}");//variables in a string
    }
    private void NewGame()
    {
        lives = 3;
        Coins = 0;
        LoadLevel(1, 1);
    }
    //public void NextLevel()
    //{
    //    LoadLevel(world, stage + 1);
    //}
    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }
    public void ResetLevel()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
        
    }
    private void GameOver()
    {
        Invoke(nameof(NewGame),3f);
    }

    public void AddCoin()
    {
        Coins++;
        if (Coins == 100)
        {
            Coins = 0;
            AddLife();
        }
    }
    public void AddLife()
    {
        lives++;
    }


}
