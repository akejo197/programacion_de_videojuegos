using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class AutoRunner : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 1.1f;
    public int maxJumps = 2;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private int jumpCount;

    private bool isRunning = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (rb == null) Debug.LogError("Falta Rigidbody en " + gameObject.name);

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        jumpCount = maxJumps;
    }

    void Update()
    {
        // se detecta el boton space 
        bool spacePressed = false;
        #if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null) spacePressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        #endif
        #if ENABLE_LEGACY_INPUT_MANAGER
        if (!spacePressed) spacePressed = Input.GetKeyDown(KeyCode.Space);
        #endif

        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded)
        {
            jumpCount = maxJumps;
        }

        if (isRunning && spacePressed && (isGrounded || jumpCount > 0))
        {
            DoJump();
        }

        // Actualizar animación
        if (animator != null)
        {
            float currentSpeed = isRunning ? runSpeed : 0f;
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    void FixedUpdate()
    {
        if (!isRunning) return;

        Vector3 forward = transform.forward * runSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forward);

        rb.angularVelocity = Vector3.zero;
    }

    void DoJump()
    {
        Vector3 v = rb.linearVelocity;
        v.y = 0f;
        rb.linearVelocity = v;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount--;
        Debug.Log("Salto ejecutado, jumps restantes: " + jumpCount);
        if (animator != null) animator.SetTrigger("Jump");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta"))
        {
            isRunning = false;
            runSpeed = 0f;

            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);   
                animator.SetTrigger("Win");       // 
            }

            Debug.Log("¡Has llegado a la meta!");
        }
    }
}
