using UnityEngine;

namespace CyberBank.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class HackerPlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 6.0f;
        public float stealthSpeed = 3.0f;
        public float rotationSpeed = 12.0f;

        [Header("Interaction")]
        public float interactDistance = 2.5f;
        public LayerMask interactableLayer;

        [Header("Animation & Visuals")]
        public Transform visualModel;
        public float floatSpeed = 2.0f;
        public float floatHeight = 0.1f;

        private Rigidbody rb;
        private bool isCrouching = false;
        private Vector3 initialModelOffset;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; // Prevent physics tipping over

            if (visualModel != null)
            {
                initialModelOffset = visualModel.localPosition;
            }
        }

        void Update()
        {
            HandleStealthInput();
            HandleInteractionInput();
            ApplyHoverAnimation();
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;
            float currentSpeed = isCrouching ? stealthSpeed : moveSpeed;

            if (moveDir.magnitude >= 0.1f)
            {
                // Move Rigidbody
                Vector3 targetVelocity = moveDir * currentSpeed;
                rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

                // Rotate visual model toward movement direction
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }

        void HandleStealthInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
                transform.localScale = isCrouching ? new Vector3(1, 0.6f, 1) : Vector3.one;
            }
        }

        void HandleInteractionInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, interactableLayer))
                {
                    Debug.Log($"Interacting with: {hit.collider.name}");
                    hit.collider.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        void ApplyHoverAnimation()
        {
            if (visualModel != null && !isCrouching)
            {
                float newY = initialModelOffset.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
                visualModel.localPosition = new Vector3(initialModelOffset.x, newY, initialModelOffset.z);
            }
        }
    }
}
