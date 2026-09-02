using UnityEngine;

namespace CyberBank.Player
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationSelector : MonoBehaviour
    {
        [Header("Animator Reference")]
        public Animator animator;

        [Header("Available Motion Clips")]
        public string idleStateName = "Idle";
        public string walkStateName = "Walk";
        public string runStateName = "Run";
        public string jumpTriggerName = "Jump";
        public string hackStateName = "HackTyping";

        private string currentState;

        void Start()
        {
            if (animator == null) animator = GetComponent<Animator>();
            PlayClip(idleStateName);
        }

        void Update()
        {
            // Preset Clip Selectors
            if (Input.GetKeyDown(KeyCode.Alpha1)) PlayClip(idleStateName);
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlayClip(walkStateName);
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlayClip(runStateName);
            if (Input.GetKeyDown(KeyCode.Alpha4)) TriggerJump();
            if (Input.GetKeyDown(KeyCode.Alpha5)) PlayClip(hackStateName);
        }

        public void PlayClip(string stateName)
        {
            if (currentState == stateName) return;

            animator.CrossFade(stateName, 0.15f);
            currentState = stateName;
            Debug.Log($"Playing Animation Clip: {stateName}");
        }

        public void TriggerJump()
        {
            animator.SetTrigger(jumpTriggerName);
            Debug.Log("Triggered Jump Clip!");
        }

        public void SetSpeedParameter(float speed)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}
