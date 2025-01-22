using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    public float gas = 100f;
    public float gasConsumptionRate = 10f;
    public float moveSpeed = 5f;       
    public float horizontalSpeed = 5f;  
    public float roadWidth = 2.5f;     
    
    private Vector2 startPosition;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;      
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        startPosition = transform.position;
    }
    
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameActive) return;
        
        
        Vector2 currentPosition = rb.position;
        
        Vector2 upwardMovement = Vector2.up * moveSpeed;
        
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 horizontalMovement = Vector2.right * horizontalInput * horizontalSpeed;
        Vector2 newPosition = currentPosition + (upwardMovement + horizontalMovement) * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -roadWidth, roadWidth);
        rb.MovePosition(newPosition);
    }
    
    private void Update()
    {
        if (!GameManager.Instance.isGameActive) return;
        
        gas -= gasConsumptionRate * Time.deltaTime;
        GameManager.Instance.UpdateGasUI(gas);
        
        if (gas <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    public void ResetPlayer()
    {
        gas = 100f;
        rb.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GasItem"))
        {
            gas += 30f;
            Destroy(other.gameObject);
        }
    }
}