using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer ground;

    [SerializeField] private Sprite dayBackground;
    [SerializeField] private Sprite nightBackground;
    [SerializeField] private Sprite dayGround;
    [SerializeField] private Sprite nightGround;

    [SerializeField] private Image themeBtnImage;
    [SerializeField] private Sprite dayButtonSprite;
    [SerializeField] private Sprite nightButtonSprite;

    [SerializeField] private Image soundBtnImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    [SerializeField] private float maxVolume = 0.7f;

    private enum ThemeSelect { Night = -1, Day = 1 };

    private int theme;
    private int isPlaying;

    private void Awake()
    {
        theme = PlayerPrefs.GetInt("Theme", -1);
        isPlaying = PlayerPrefs.GetInt("isPlaying", 1);

        // Sets up Themes
        if (theme == -1)
        {
            background.sprite = nightBackground;
            ground.sprite = nightGround;
            themeBtnImage.sprite = nightButtonSprite;
        }
        else if (theme == 1)
        {
            background.sprite = dayBackground;
            ground.sprite = dayGround;
            themeBtnImage.sprite = dayButtonSprite;
        }

        // Sets up Audio
        if (isPlaying == 1)
        {
            foreach (Sound sounds in FindObjectOfType<AudioManager>().sounds)
            {
                sounds.source.volume = maxVolume;
                sounds.source.UnPause();
            }
            soundBtnImage.sprite = soundOnSprite;
        }
        else if (isPlaying == 0)
        {
            foreach (Sound sounds in FindObjectOfType<AudioManager>().sounds)
            {
                sounds.source.volume = 0f;
                sounds.source.Pause();
            }
            soundBtnImage.sprite = soundOffSprite;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeTheme()
    {
        theme = PlayerPrefs.GetInt("Theme", -1) * -1;
        PlayerPrefs.SetInt("Theme", theme);

        if (theme == -1)
        {
            background.sprite = nightBackground;
            ground.sprite = nightGround;
            themeBtnImage.sprite = nightButtonSprite;
        }
        else if (theme == 1)
        {
            background.sprite = dayBackground;
            ground.sprite = dayGround;
            themeBtnImage.sprite = dayButtonSprite;
        }
    }

    public void SetVolume()
    {
        isPlaying = PlayerPrefs.GetInt("isPlaying");

        if (isPlaying == 1)
        {
            foreach (Sound sounds in FindObjectOfType<AudioManager>().sounds)
            {
                sounds.source.volume = 0f;
                sounds.source.Pause();
            }

            soundBtnImage.sprite = soundOffSprite;
            PlayerPrefs.SetInt("isPlaying", 0);
        }
        else if (isPlaying == 0)
        {
            foreach (Sound sounds in FindObjectOfType<AudioManager>().sounds)
            {
                sounds.source.volume = maxVolume;
                sounds.source.UnPause();
            }
            soundBtnImage.sprite = soundOnSprite;
            PlayerPrefs.SetInt("isPlaying", 1);
        }
    }
}