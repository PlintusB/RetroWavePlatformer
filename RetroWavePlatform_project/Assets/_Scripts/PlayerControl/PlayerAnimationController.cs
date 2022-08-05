using System.Collections;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerMan;
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();            
        }

        void Update()
        {
            _animator.SetBool("IsGrounded", _playerMan.IsGrounded);
            _animator.SetInteger("X_move", Mathf.Abs(_playerMan.XMoveInput));
            _animator.SetFloat("Y_move", _playerMan.VerticalVelocity);

            FlipX();
        }

        void FlipX()
        {
            if (_playerMan.XMoveInput > 0 && transform.localScale.x == -1f)
                transform.localScale = new Vector2(1, 1);
            if (_playerMan.XMoveInput < 0 && transform.localScale.x == 1f)
                transform.localScale = new Vector2(-1, 1);
        }
    }
}