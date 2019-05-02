using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

namespace InfinityCube
{
    public class Rotate : MonoBehaviour
    {

        // Start is called before the first frame update
        private Vector3 _anchor;
        private Vector3 _axis;

        public float speed = 10f;

        private int _random;

        private Transform cylinderAxis;

        private Transform a;
        private Transform b;

        public Collision collision;
        public Collider collider;

        private float rotateAngle;

        private Plane plane;
        private Vector3 rotateAxis;

        private int _adjust = 1;

        void Start()
        {
            a = transform.GetChild(0);
            b = transform.GetChild(1);
            rotateAngle = 0;
            rotateAxis = new Vector3();

            Debug.Log(rotateAngle);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                plane = new Plane(a.position, b.position, b.GetChild(0).position);
                if (plane.normal == Vector3.up || plane.normal == Vector3.down)
                    rotateAxis = Vector3.up;
                if (plane.normal == Vector3.left || plane.normal == Vector3.right)
                    rotateAxis = Vector3.left;
                if (plane.normal == Vector3.forward || plane.normal == Vector3.back)
                    rotateAxis = Vector3.forward;

                CrossProduct();


                _random = Random.Range(0, 2);
            }
            
             if(Mathf.Abs(rotateAngle) < 180 && rotateAxis != Vector3.zero)
            {
                Debug.Log($"{_random}  {_adjust}");

                if (_random == 0)
                {
                    if (_adjust > 0)
                    {
                        a.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        b.RotateAround(b.GetChild(0).position, rotateAxis, speed );
                        rotateAngle += speed;
                    }

                    if (_adjust < 0)
                    {
                        a.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        b.RotateAround(b.GetChild(0).position, -rotateAxis,  speed );
                        rotateAngle += speed ;
                    }
                }

                if (_random == 1)
                {
                    if (_adjust > 0)
                    {
                        b.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        a.RotateAround(a.GetChild(0).position, -rotateAxis, speed);
                        rotateAngle += speed;
                    }

                    if (_adjust < 0)
                    {
                        b.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        a.RotateAround(a.GetChild(0).position, rotateAxis, speed );
                        rotateAngle += speed ;
                    }
                }

               
            }

            if (Mathf.Abs(rotateAngle) >= 180)
            {
                Reset();
            }



        }

        void Reset()
        {
            rotateAngle = 0;
            rotateAxis = Vector3.zero;
            a.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        void CrossProduct()
        {
            float x1 = b.GetChild(0).position.x - a.position.x;
            float y1 = b.GetChild(0).position.y - a.position.y;
            float z1 = b.GetChild(0).position.z - a.position.z;

            float x2 = b.GetChild(0).position.x - b.position.x;
            float y2 = b.GetChild(0).position.y - b.position.y;
            float z2 = b.GetChild(0).position.z - b.position.z;

            float x3 = y1 * z2 - y2 * z1;
            float y3 = z1 * x2 - z2 * x1;
            float z3 = x1 * y2 - x2 * y1;

            if (y3 < 0)
                _adjust = -1;
            else
            
                _adjust = 1;
            

        }


    }
}


