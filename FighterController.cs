using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class FighterController : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody rb;
        public Collider rightHandCollider;
        public Collider leftHandCollider;
        public Collider rightFootCollider;
        public Collider leftFootCollider;

        private Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();

        public string walkForwardAnimation = "walk_forward";
        public string walkBackwardAnimation = "walk_backwards";
        public string runForwardAnimation = "run_forward";
        public string turnLAnimation = "turn_L";
        public string turnRAnimation = "turn_R";
        public string jumpAnimation = "jump";

        public string hookPunchAnimation = "RightHookAnimation";
        public string kickingAnimation = "KickingAnimation";
        public string punch1Animation = "Punch2Animation";
        public string punchAnimation = "RightPunch";

        public float moveSpeed = 2f;
        public float runSpeed = 4f;
        public float turnSpeed = 60f;
        public float jumpHeight = 5f;
        public float collisionCheckDistance;
        private bool isGrounded;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();

            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.freezeRotation = true;

            LoadControls();
            DisableHitBoxes();
        }

        void Update()
        {
            bool isMoving = false;

            Vector3 movement = Vector3.zero;

            // KeyCode forwardKey = (KeyCode)PlayerPrefs.GetInt("Forward", (int)KeyCode.W);
            // KeyCode backwardKey = (KeyCode)PlayerPrefs.GetInt("Backward", (int)KeyCode.S);
            // KeyCode leftKey = (KeyCode)PlayerPrefs.GetInt("Left", (int)KeyCode.A);
            // KeyCode rightKey = (KeyCode)PlayerPrefs.GetInt("Right", (int)KeyCode.D);
            // KeyCode jumpKey = (KeyCode)PlayerPrefs.GetInt("Jump", (int)KeyCode.Space);
            // KeyCode punchKey = (KeyCode)PlayerPrefs.GetInt("Punch", (int)KeyCode.Alpha6);
            // KeyCode hookPunchKey = (KeyCode)PlayerPrefs.GetInt("HookPunch", (int)KeyCode.Alpha3);
            // KeyCode kickKey = (KeyCode)PlayerPrefs.GetInt("Kick", (int)KeyCode.Alpha4);
            // KeyCode punch2Key = (KeyCode)PlayerPrefs.GetInt("Punch2", (int)KeyCode.Alpha5);

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("MoveForward")))
            {
                animator.Play(walkForwardAnimation);
                movement = transform.forward * moveSpeed * Time.deltaTime;
                isMoving = true;
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("MoveBackward")))
            {
                animator.Play(walkBackwardAnimation);
                movement = -transform.forward * moveSpeed * Time.deltaTime;
                isMoving = true;
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("TurnLeft")))
            {
                animator.Play(turnLAnimation);
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                isMoving = true;
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("TurnRIght")))
            {
                animator.Play(turnRAnimation);
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                isMoving = true;
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("Punch")))
            {
                PerformAttack(punchAnimation, rightHandCollider);
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("Punch2")))
            {
                PerformAttack(punch1Animation, rightHandCollider);
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("HookPunch")))
            {
                PerformAttack(hookPunchAnimation, rightHandCollider);
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("Kick")))
            {
                PerformAttack(kickingAnimation, rightFootCollider);
            }

            if (Input.GetKey(SettingsMenu.Instance.GetKeybind("Jump")))
            {
                Jump();
            }

            if (!Physics.Raycast(transform.position, movement.normalized, collisionCheckDistance))
            {
                if (movement != Vector3.zero)
                {
                    rb.MovePosition(rb.position + movement);
                }
            }

            GroundStatus();
        }

        void Jump()
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            animator.Play(jumpAnimation);
        }

        void GroundStatus()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
            {
                if (hit.collider != null)
                {
                    isGrounded = true;
                }
            }
            else
            {
                isGrounded = false;
            }
        }

        void PerformAttack (string animationName, Collider hitboxCollider)
        {
            animator.Play(animationName);
            EnableHitbox(hitboxCollider);
            StartCoroutine(DisableHitboxAfterDelay(hitboxCollider, .7f));
        }

        void EnableHitbox(Collider hitboxCollider)
        {
            if (hitboxCollider != null)
            {
                Debug.Log("Enabled");
                hitboxCollider.enabled = true;
            }
        }

        IEnumerator DisableHitboxAfterDelay(Collider hitboxCollider, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (hitboxCollider != null)
            {
                hitboxCollider.enabled = false;
            }
        }

        void DisableHitBoxes()
        {
            rightFootCollider.enabled = false;
            rightHandCollider.enabled = false;
            leftFootCollider.enabled = false;
            leftHandCollider.enabled = false;
        }

        void LoadControls()
        {
            string[] actions = {"Forward", "Backward", "Left", "Right", "Jump", "Punch", "HookPunch", "Kick", "Punch2"};

            foreach (string action in actions)
            {
                controls[action] = (KeyCode)PlayerPrefs.GetInt(action, (int)DefaultKey(action));
            }
        }

        KeyCode DefaultKey(string action)
        {
            switch (action)
            {
                case "Forward": return KeyCode.W;
                case "Backward": return KeyCode.S;
                case "Left": return KeyCode.A;
                case "Right": return KeyCode.D;
                case "Jump": return KeyCode.Space;
                case "Punch": return KeyCode.Alpha6;
                case "HookPunch": return KeyCode.Alpha3;
                case "Kick": return KeyCode.Alpha4;
                case "Punch2": return KeyCode.Alpha5;
                default: return KeyCode.None;
            }
        }
    }