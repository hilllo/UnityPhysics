using UnityEngine;
using System.Collections;

namespace Game.Physics
{
    public class PhysicsEngine : MonoBehaviour
    {

        [Tooltip("Average velocity (distance per sec)")]
        [SerializeField]
        private Vector3 _Velocity;

        // Update is called once per frame
        void FixedUpdate()
        {
            this.transform.position += this._Velocity * Time.deltaTime;
        }
    }
}
