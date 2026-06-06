namespace MoveBetweenFloor
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Movement : MonoBehaviour
    {
        [SerializeField] float speed = 3f;

        void Update()
        {
            float horizontal = Keyboard.current.aKey.isPressed ? -1 : (Keyboard.current.dKey.isPressed ? 1 : 0);

            Vector3 direction = new Vector3(horizontal, 0, 0);
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}