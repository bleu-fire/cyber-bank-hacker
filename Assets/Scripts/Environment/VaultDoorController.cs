using UnityEngine;

namespace CyberBank.Environment
{
    public class VaultDoorController : MonoBehaviour
    {
        [Header("Vault Door Animation")]
        public Vector3 openPositionOffset = new Vector3(0, 5, 0);
        public Vector3 openRotationOffset = new Vector3(0, 90, 0);
        public float openSpeed = 2.0f;

        [Header("Audio & Effects")]
        public ParticleSystem sparksEffect;
        public Light vaultGlowLight;

        private bool isUnlocked = false;
        private Vector3 closedPos;
        private Quaternion closedRot;

        void Start()
        {
            closedPos = transform.position;
            closedRot = transform.rotation;
        }

        public void UnlockVault()
        {
            if (isUnlocked) return;
            isUnlocked = true;
            Debug.Log("Vault Door Unlocked!");

            if (sparksEffect != null) sparksEffect.Play();
            if (vaultGlowLight != null) vaultGlowLight.color = Color.green;
        }

        void Update()
        {
            if (isUnlocked)
            {
                Vector3 targetPos = closedPos + openPositionOffset;
                Quaternion targetRot = closedRot * Quaternion.Euler(openRotationOffset);

                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * openSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * openSpeed);
            }
        }
    }
}
