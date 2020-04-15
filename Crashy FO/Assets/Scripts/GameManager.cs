using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject readyPrompt;
    public GameObject gameOverPanel;
    public GameObject scorePanel;

    [Header("Medals")]
    public GameObject bronzeMedal;
    public GameObject silverMedal;
    public GameObject goldMedal;
    public GameObject platinumMedal;

    [Header("Text References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScore;

    [Header("Themes")]
    public SpriteRenderer playerSprite;
    public Sprite playerDaySprite;
    public Sprite playerNightSprite;

    public SpriteRenderer background;
    public SpriteRenderer ground;

    public Sprite dayBackground;
    public Sprite nightBackground;
    public Sprite dayGround;
    public Sprite nightGround;

    [Header("Animator")]
    public Animator playerAnim;
    public RuntimeAnimatorController dayAnimController;
    public RuntimeAnimatorController nightAnimController;

    public static int score = 0;
    public static int highScore = 0;
    public static bool gameIsOver = false;

    private void Awake()
    {
        int theme = PlayerPrefs.GetInt("Theme", -1);

        if (theme == -1)
        {
            background.sprite = nightBackground;
            ground.sprite = nightGround;
            playerSprite.sprite = playerNightSprite;
            playerAnim.runtimeAnimatorController = nightAnimController;
        }
        else if (theme == 1)
        {
            background.sprite = dayBackground;
            ground.sprite = dayGround;
            playerSprite.sprite = playerDaySprite;
            playerAnim.runtimeAnimatorController = dayAnimController;
        }

        Time.timeScale = 0f;
        score = 0;
        gameIsOver = false;
        gameOverPanel.SetActive(false);
        scorePanel.SetActive(false);
        readyPrompt.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameIsOver)
        {
            readyPrompt.SetActive(false);
            scorePanel.SetActive(true);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        playerAnim.SetBool("playerCrashed", true);
        gameOverPanel.SetActive(true);
        scorePanel.SetActive(false);
        gameIsOver = true;
        currentScore.text = score.ToString();
        bestScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        if (score <= 15)
        {
            bronzeMedal.SetActive(true);
        }
        else if (score <= 30 && score > 15)
        {
            silverMedal.SetActive(true);
        }
        else if (score <= 50 && score > 30)
        {
            goldMedal.SetActive(true);
        }
        else if (score < 50)
        {
            platinumMedal.SetActive(true);
        }

        Time.timeScale = 0f;
        
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
