using UnityEngine;

    public class UserInput : MonoBehaviour
    {
        public int HorizontalAxis { get; private set; }
        public int VerticalAxis { get; private set; }
        public bool IsJumpButtonPressed { get; private set; }
        public bool IsPauseButtonPressed { get; private set; }

        void Update()
        {
            HorizontalAxis = (int)Input.GetAxisRaw("Horizontal");
            VerticalAxis = (int)Input.GetAxisRaw("Vertical");

            IsJumpButtonPressed = Input.GetButtonDown("Jump");
            IsPauseButtonPressed = Input.GetKeyDown(KeyCode.Escape);
        }
    }
