using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    public float maxWalkSpeed = 25.0f;
    public float jumpForce = 25.0f;

    GameDirector gameDirector;

    public AudioClip coinSound;
    public AudioClip obstacleSound;
    public AudioClip jumpSound;
    public AudioClip finishSound; 
    AudioSource audioSource;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rigid2D.linearVelocity = new Vector2(moveX * maxWalkSpeed, rigid2D.linearVelocity.y);

        if (moveX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);
        }

        animator?.SetFloat("Speed", Mathf.Abs(moveX));

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigid2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator?.SetBool("isJumping", true);
            PlaySound(jumpSound); 
        }

        if (animator?.GetBool("isJumping") == true && IsGrounded() && rigid2D.linearVelocity.y <= 0.01f)
        {
            animator.SetBool("isJumping", false);
        }

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, 0f, 150f);
        clampedPos.y = Mathf.Clamp(clampedPos.y, -2f, 23f);
        transform.position = clampedPos;
    }

    bool IsGrounded()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 0.9f);
        float rayLength = 0.3f;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin, Vector2.down * rayLength, hit.collider ? Color.green : Color.red);

        if (hit.collider != null)
        {
            Debug.Log("Ground hit: " + hit.collider.gameObject.name);
        }

        return hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Tag==Coin");
            PlaySound(coinSound); 
            gameDirector.GetCoin();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Tag==Obstacle");
            PlaySound(obstacleSound);
            gameDirector.GetObstacle();
        }
        else if (other.CompareTag("Finish"))
        {
            PlaySound(finishSound); 
            StartCoroutine(LoadEndSceneAfterSound(finishSound.length));
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    IEnumerator LoadEndSceneAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("EndScene");
    }
}