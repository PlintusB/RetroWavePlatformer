using UnityEngine;

namespace PlayerControl
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;
        public bool IsPlayerGrounded { get; private set; }

        private void FixedUpdate()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            IsPlayerGrounded = Physics2D.BoxCast(
                transform.position,
                new Vector2(0.1f, 0.05f),
                0f,
                Vector2.down,
                0f,
                _groundMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,
                new Vector2(0.1f, 0.05f));
        }

    }
}