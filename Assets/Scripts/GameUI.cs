using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
   
    public Image fadeImage;
    public GameObject gameOver;
    public GameObject winner;

    public Text score;
    public Text timer;

    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;

    public AudioClip menuTheme;
    public AudioClip mainTheme;

    // Use this for initialization
    void Start () {        
        
        FindObjectOfType<PlayerStats>().OnDeath += OnGameOver;
        FindObjectOfType<PlayerStats>().Winner += Win;
        
    }

    private void Update()
    {
        score.text = Scorekeeper.score.ToString();
        timer.text = Scorekeeper.timer.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {     
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
     

    }

    void Pause(){       
       
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;           

    }

    void OnGameOver()
    {
        
        //StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOver.SetActive(true);
        AudioManager.instance.PlaySound2D("playerDeath");
        Time.timeScale = 0f;
               

    }
   
    void Win()
    {
       
        winner.SetActive(true);
        //AudioManager.instance.PlaySound2D("win");
        Time.timeScale = 0f;

    }
	
	IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadeImage.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    //UI Button
    public void StartNewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Application.LoadLevel("Main");
    }

    public void LoadMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
