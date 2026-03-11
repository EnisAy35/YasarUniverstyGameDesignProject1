using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 moveDirection;
    public float rotationSpeed = 720f; 

    void Update()
    {
        ProccessInputs();
        RotatePlayer();
        UpdateAnims(); // Animasyon durumunu her karede kontrol et
    }

    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void RotatePlayer()
    {
        if (moveDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            
            // Sprite yukarı bakıyorsa -90 eklemiştik, aynen devam
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle - 90);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void UpdateAnims()
    {
        // Eğer hareket vektörünün uzunluğu 0'dan büyükse isMoving true olur
        bool isMoving = moveDirection.magnitude > 0;
        
        // Animator'deki "isMoving" parametresini güncelle
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }
}