using UnityEngine;

public class Pillar : MonoBehaviour
{
    public float moveSpeed = 1.5f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.score++;

        if (GameManager.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameManager.score);
        }

        FindObjectOfType<GameManager>().UpdateScore();
    }

}
