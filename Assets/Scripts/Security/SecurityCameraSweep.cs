using UnityEngine;

namespace CyberBank.Security
{
    public class SecurityCameraSweep : MonoBehaviour
    {
        [Header("Sweep Settings")]
        public float sweepSpeed = 2.0f;
        public float sweepAngle = 45.0f;

        [Header("Detection Settings")]
        public float detectionDistance = 12.0f;
        public LayerMask playerLayer;
        public Light securitySpotlight;

        private float startYAngle;

        void Start()
        {
            startYAngle = transform.eulerAngles.y;
        }

        void Update()
        {
            SweepCamera();
            CheckForPlayerDetection();
        }

        void SweepCamera()
        {
            float yOffset = Mathf.Sin(Time.time * sweepSpeed) * sweepAngle;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, startYAngle + yOffset, transform.eulerAngles.z);
        }

        void CheckForPlayerDetection()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.LogWarning("ALERT! Player detected in security light cone!");
                    if (securitySpotlight != null) securitySpotlight.color = Color.red;
                }
            }
        }
    }
}
