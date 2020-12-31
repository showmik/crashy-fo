using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject readyPrompt;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject scorePanel;

    [Header("Medals")]
    [SerializeField] private GameObject bronzeMedal;

    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject platinumMedal;

    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI bestScore;

    [Header("Themes")]
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private Sprite playerDaySprite;
    [SerializeField] private Sprite playerNightSprite;

    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer ground;

    [SerializeField] private Sprite dayBackground;
    [SerializeField] private Sprite nightBackground;
    [SerializeField] private Sprite dayGround;
    [SerializeField] private Sprite nightGround;

    [Header("Animator")]
    [SerializeField] private Animator playerAnim;

    [SerializeField] private RuntimeAnimatorController dayAnimController;
    [SerializeField] private RuntimeAnimatorController nightAnimController;

    public static int score = 0;
    [SerializeField] private static int highScore = 0;
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
        else if (score > 30)
        {
            goldMedal.SetActive(true);
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