using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

namespace InfinityCube
{
    public class RotateBD : MonoBehaviour
    {

        // Start is called before the first frame update
        private Vector3 _anchor;
        private Vector3 _axis;

        public float speed = 10f;

        private int _random;
        private int _randomRot;

        private Transform cylinderAxis;

        private Transform a;
        private Transform b;
        private Transform c;
        private Transform d;

        Transform jointab;
        Transform jointac;
        Transform jointbd;

        private float rotateAngle;

        private Plane plane;
        private Vector3 rotateAxis;

        private int _adjust = 1;

        void Start()
        {
            a = transform.GetChild(0);
            b = transform.GetChild(1);
            c = transform.GetChild(2);
            d = transform.GetChild(3);

            rotateAngle = 0;
            rotateAxis = new Vector3();

            jointab = a.transform.Find("jointab");
            jointac = a.transform.Find("jointac");
            jointbd = b.transform.Find("jointbd");

        }

        // Update is called once per frame
        void Update()
        {
            _random = Random.Range(0, 3);

            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if (_random == 0)
                //    plane = new Plane(a.position, b.position, jointab.position);
                //if (_random == 1)
                //    plane = new Plane(a.position, c.position, jointac.position);
                //if (_random == 2)
                //    plane = new Plane(b.position, d.position, jointbd.position);

                plane = new Plane(a.position, b.position, jointab.position);


                if (plane.normal == Vector3.up || plane.normal == Vector3.down)
                        rotateAxis = Vector3.up;
                    if (plane.normal == Vector3.left || plane.normal == Vector3.right)
                        rotateAxis = Vector3.left;
                    if (plane.normal == Vector3.forward || plane.normal == Vector3.back)
                        rotateAxis = Vector3.forward;
                
                CrossProduct();

                _randomRot = Random.Range(0, 2);
            }
            
             if(Mathf.Abs(rotateAngle) <= 360 && rotateAxis != Vector3.zero)
            {
                if (_randomRot == 0)
                {
                    if (_adjust > 0)
                    {
                        a.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        b.RotateAround(jointab.position, rotateAxis, speed );
                        rotateAngle += speed;
                    }

                    if (_adjust < 0)
                    {
                        a.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        b.RotateAround(jointab.position, -rotateAxis,  speed );
                        rotateAngle += speed ;
                    }
                }

                if (_randomRot == 1)
                {
                    if (_adjust > 0)
                    {
                        b.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        a.RotateAround(jointab.position, -rotateAxis, speed);
                        rotateAngle += speed;
                    }

                    if (_adjust < 0)
                    {
                        b.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        a.RotateAround(jointab.position, rotateAxis, speed );
                        rotateAngle += speed ;
                    }
                }

               
            }

            if (Mathf.Abs(rotateAngle) > 360)
            {
                Reset();
            }

            if (Input.GetKeyDown(KeyCode.R))
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
            float x1 = jointab.position.x - a.position.x;
            float y1 = jointab.position.y - a.position.y;
            float z1 = jointab.position.z - a.position.z;

            float x2 = jointab.position.x - b.position.x;
            float y2 = jointab.position.y - b.position.y;
            float z2 = jointab.position.z - b.position.z;

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


