using UnityEngine;
using DialogueEditor;
using UnityEngine.InputSystem;

public class ConversationInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public class ExampleInputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }
        void Update()
        {
            if (Gamepad.current != null && Gamepad.current.dpad.up.wasPressedThisFrame)
            {
                Debug.Log("D-pad Up Pressed!");
                Submit();
            }
        }
        public void Submit()
        {
            if (ConversationManager.Instance.IsConversationActive)
                ConversationManager.Instance.PressSelectedOption();
        }


    }
}
