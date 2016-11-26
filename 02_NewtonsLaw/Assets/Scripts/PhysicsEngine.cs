using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Physics
{
    [DisallowMultipleComponent]
    public class PhysicsEngine : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private float _Mass;

        [SerializeField, Tooltip("Average velocity (distance per sec)")]
        private Vector3 _Velocity;

        private Vector3 _NetForce;

        private List<Vector3> _ForceList;

        [SerializeField, Header("Trail Renderer")]
        private bool _ShowTrails = true;

        [SerializeField]
        private LineRenderer _LineRenderer;

        [SerializeField]
        private Shader _LineShader;

        [SerializeField]
        private Color _LineColor;

        private int _NumberOfForces;

        #endregion Fields

        #region MonoBehaviours

        private void Awake()
        {
            this._ForceList = new List<Vector3>();
        }

        void Start()
        {
            if (this._Mass == 0.0f)
                throw new System.ArgumentNullException(string.Format("{0}.PhysicsEngine.Mass expected to be != 0", this.gameObject.name));
            this._NetForce = Vector3.zero;            

            this.SetupTrailRenderer();
        }
        
        void FixedUpdate()
        {
            this.UpdateTrailRenderer();
            this.UpdatePositon();
        }

        #endregion MonoBehaviours

        public void AddForce(Vector3 newForce)
        {
            this._ForceList.Add(newForce);
        }

        private void UpdatePositon()
        {
            this._NetForce = Vector3.zero;

            foreach (Vector3 force in this._ForceList)
            {
                this._NetForce += force;
            }

            this._ForceList.Clear();

            this._Velocity += this._NetForce / this._Mass;
            this.transform.position += this._Velocity * Time.deltaTime;
        }

        private void SetupTrailRenderer()
        {
            if (this._LineRenderer == null)
            {
                Debug.Log(string.Format("Added LineRenerer on {0}", this.gameObject.name));
                this._LineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            else if (!this._LineRenderer.enabled)
                this._LineRenderer.enabled = true;

            if (!(this._LineRenderer.gameObject.GetComponent<Renderer>() is LineRenderer))
                throw new System.ApplicationException("The Line Renderer should be the only Renderer on a GameObject. ");

            if (this._LineRenderer.material.shader != this._LineShader)
            {
                Debug.Log(string.Format("Changed LineRenerer.material.shader to {1} on {0}", this._LineShader.ToString(), this.gameObject.name));
                this._LineRenderer.material = new Material(this._LineShader);
            }

            if (this._LineRenderer.material.color != this._LineColor)
            {
                this._LineRenderer.SetColors(this._LineColor, this._LineColor);
            }

            this._LineRenderer.SetWidth(0.2F, 0.2F);
            this._LineRenderer.useWorldSpace = false;
        }

        private void UpdateTrailRenderer()
        {
            if (this._ShowTrails)
            {
                if (!this._LineRenderer.enabled)
                    this._LineRenderer.enabled = true;
                this._NumberOfForces = this._ForceList.Count;
                this._LineRenderer.SetVertexCount(this._NumberOfForces * 2);
                int i = 0;
                foreach (Vector3 forceVector in _ForceList)
                {
                    this._LineRenderer.SetPosition(i, Vector3.zero);
                    this._LineRenderer.SetPosition(i + 1, -forceVector);
                    i = i + 2;
                }
            }
            else
            {
                this._LineRenderer.enabled = false;
            }
        }
    }
}
