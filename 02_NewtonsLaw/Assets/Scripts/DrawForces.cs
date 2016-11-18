using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Version 1 of this simple component to draw lines representing force vectors
// Think of them as rocket trails if you like
namespace Game.Physics
{
    [DisallowMultipleComponent]
    public class DrawForces : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private PhysicsEngine _PhysicsEngine;

        [SerializeField]
        private bool _ShowTrails = true;       

        [SerializeField]
        private LineRenderer _LineRenderer;

        [SerializeField]
        private Shader _LineShader;

        [SerializeField]
        private Color _LineColor;

        private List<Vector3> _ForceList = new List<Vector3>();
        private int _NumberOfForces;

        // Use this for initialization
        void Start()
        {
            if (!(this.GetComponent<Renderer>() is LineRenderer))
                throw new System.ApplicationException("The Line Renderer should be the only Renderer on a GameObject. ");

            _ForceList = this._PhysicsEngine.ForceList;

            if (this._LineRenderer == null)
            {
                Debug.Log(string.Format("Added LineRenerer on {0}", this.gameObject.name));
                this._LineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            else if (!this._LineRenderer.enabled)
                this._LineRenderer.enabled = true;

            if (this._LineRenderer.material.shader != this._LineShader)
            {
                Debug.Log(string.Format("Changed LineRenerer.material.shader to {1} on {0}", this._LineShader.ToString(), this.gameObject.name));
                this._LineRenderer.material = new Material(this._LineShader);
            }

            if(this._LineRenderer.material.color != this._LineColor)
            {
                this._LineRenderer.SetColors(this._LineColor, this._LineColor);
            }                
                
            this._LineRenderer.SetWidth(0.2F, 0.2F);
            this._LineRenderer.useWorldSpace = false;
        }

        #endregion Fields

        #region MonoBehaviours

        // Update is called once per frame
        void Update()
        {
            if (this._ShowTrails)
            {
                if(!this._LineRenderer.enabled)
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

        #endregion MonoBehaviours
    }
}

 