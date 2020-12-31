using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;

    [Header("Data")]
    [SerializeField] private float jumpForce = 8f;

    private Rigidbody2D body2d;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.gameIsOver)
        {
            body2d.velocity += Vector2.up * jumpForce;

            audioManager.Play("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Boundary"))
        {
            gameManager.GameOver();
        }
    }
}