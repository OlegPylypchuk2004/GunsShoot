using UnityEngine;

namespace BulletSystem
{
    public class BulletMesh : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _rotationOffset;

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity.normalized) * Quaternion.Euler(_rotationOffset);
        }
    }
}