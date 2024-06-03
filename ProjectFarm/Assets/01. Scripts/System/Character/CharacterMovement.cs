using UnityEngine;

namespace H00N.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 5f;
        [SerializeField] float accel = 10f;

        private Vector3 destination = Vector3.zero;
        private float velocity = 0f;

        private void FixedUpdate()
        {
            Vector3 directionVector = destination - transform.position;
            if(directionVector.sqrMagnitude <= 0.1f)
            {
                velocity = 0f;   
                return;
            }

            velocity += accel * Time.fixedDeltaTime;
            velocity = Mathf.Min(velocity, maxSpeed);
            
            Vector3 direction = directionVector.normalized;
            transform.position += direction * velocity * Time.fixedDeltaTime;
        }

        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            velocity = 0f;
        }
    }
}
