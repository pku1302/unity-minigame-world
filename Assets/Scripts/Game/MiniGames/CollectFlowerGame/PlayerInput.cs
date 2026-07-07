using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGameWorld.FlowerGame
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2Int Direction { get; private set; }

        private void Update()
        {
            Direction = Vector2Int.zero;

            HandleInput();
        }
        public void HandleInput()
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                Direction = Vector2Int.up;

            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                Direction = Vector2Int.down;

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                Direction = Vector2Int.left;

            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                Direction = Vector2Int.right;
        }
    }

}