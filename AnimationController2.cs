using UnityEngine;
using System.Collections;

    public class AnimationController2 : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody rb;
        public Collider rightHandCollider;
        public Collider leftHandCollider;
        public Collider rightFootCollider;
        public Collider leftFootCollider;

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
        public string readyIdle = "IdleAnimation";

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

            DisableHitBoxes();
        }

        void Update()
        {
            bool isMoving = false;

            Vector3 movement = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.Play(walkForwardAnimation);
                movement = transform.forward * moveSpeed * Time.deltaTime;
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.Play(walkBackwardAnimation);
                movement = -transform.forward * moveSpeed * Time.deltaTime;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                animator.Play(turnLAnimation);
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                animator.Play(turnRAnimation);
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                animator.Play(runForwardAnimation);
                movement = transform.forward * runSpeed * Time.deltaTime;
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Hook Punch");
                PerformAttack(hookPunchAnimation, rightHandCollider);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("Kicking");
                PerformAttack(kickingAnimation, rightFootCollider);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                Debug.Log("Punch 2");
                PerformAttack(punch1Animation, rightHandCollider);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("Right Punch");
                PerformAttack(punchAnimation, leftHandCollider);
            }
            if (Input.GetKey(KeyCode.Space) && isGrounded)
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
            if (!isMoving && isGrounded)
            {
                animator.Play(readyIdle);
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
    }