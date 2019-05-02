using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

namespace InfinityCube
{
    public class RotateWithoutJoint : MonoBehaviour
    {

        // Start is called before the first frame update
        private Vector3 _anchor;
        private Vector3 _axis;

        private float time;


        public float speed = 10f;

        private int _random = 5;

        private Transform cylinderAxis;

        private Transform a;
        private Transform b;
        private Transform c;
        private Transform d;

        Transform jointab;
        Transform jointac;
        Transform jointbd;
        Transform jointba;
        Transform jointca;
        Transform jointdb;

        private Transform buffer1;
        private Transform buffer2;
        private Transform jointbuffer;

        private float rotateAngle;

        private Plane plane;
        private Vector3 rotateAxis;

        private int _adjust = 1;

        void Start()
        {
        a = transform.Find("a");
        b = transform.Find("b");
        c = transform.Find("c");
        d = transform.Find("d");

            rotateAngle = 0;
            rotateAxis = new Vector3();

            jointab = a.transform.Find("jointab");
            jointac = a.transform.Find("jointac");
            jointbd = b.transform.Find("jointbd");
            jointba = b.transform.Find("jointba");
            jointca = c.transform.Find("jointca");
            jointdb = d.transform.Find("jointdb");


        }

        // Update is called once per frame
        void Update()
        {

            if (time >= 5 / speed)
            {
                _random = Random.Range(0, 12);
                SetRotateAnchor();
                time = 0;
            }

            time += Time.deltaTime;

            if (Mathf.Abs(rotateAngle) < 180 && rotateAxis != Vector3.zero)
            {
                if (_random == 0)
                {
                    // ab
                    d.transform.parent = b.transform;
                    b.RotateAround(jointab.position, rotateAxis, speed);
                    rotateAngle += speed;
                }

                if (_random == 1)
                {
                    // ac
                    c.RotateAround(jointac.position, rotateAxis, speed);
                    rotateAngle += speed;
                }

                if (_random == 2)
                {
                    // bd
                    d.RotateAround(jointbd.position, rotateAxis, speed);
                    rotateAngle += speed;
                }

                if (_random == 3)
                {
                    // ba
                    c.transform.parent = a.transform;
                    a.RotateAround(jointba.position, rotateAxis, speed);
                    rotateAngle += speed;
                }

                if (_random == 4)
                {
                    // ca
                    b.transform.parent = a.transform;
                    d.transform.parent = a.transform;
                    a.RotateAround(jointca.position, rotateAxis, speed);
                    rotateAngle += speed;
                }

                if (_random == 5)
                {
                    // db
                    a.transform.parent = b.transform;
                    c.transform.parent = b.transform;
                    b.RotateAround(jointdb.position, rotateAxis, speed);
                    rotateAngle += speed;
                }

                if (_random == 6)
                {
                    // a先不动，b90，a碰
                    if (rotateAngle < 90)
                    {
                        d.transform.parent = b.transform;
                        b.RotateAround(jointab.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                    else
                    {
                        c.transform.parent = a.transform;
                        a.RotateAround(jointba.position, rotateAxis, speed);
                        rotateAngle += speed;
                    }
                }

                if (_random == 7)
                {
                    // a先不动，c90，a碰
                    if (rotateAngle < 90)
                    {
                        c.RotateAround(jointac.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                    else
                    {
                        b.transform.parent = a.transform;
                        d.transform.parent = a.transform;
                        a.RotateAround(jointca.position, rotateAxis, speed);
                        rotateAngle += speed;
                    }
                }

                if (_random == 8)
                {
                    // b先不动，d90，b碰
                    if (rotateAngle < 90)
                    {
                        d.RotateAround(jointbd.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                    else
                    {
                        a.transform.parent = b.transform;
                        c.transform.parent = b.transform;
                        b.RotateAround(jointdb.position, rotateAxis, speed);
                        rotateAngle += speed;
                    }
                }

                if (_random == 9)
                {
                    // b先不动，a90，b碰
                    if (rotateAngle < 90)
                    {
                        c.transform.parent = a.transform;
                        a.RotateAround(jointba.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                    else
                    {
                        d.transform.parent = b.transform;
                        b.RotateAround(jointab.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                }

                if (_random == 10)
                {
                    // c先不动，a90，c碰
                    if (rotateAngle < 90)
                    {
                        b.transform.parent = a.transform;
                        d.transform.parent = a.transform;
                        a.RotateAround(jointca.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                    else
                    {
                        c.RotateAround(jointac.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                }

                if (_random == 11)
                {
                    // d先不动，b90，d碰
                    if (rotateAngle < 90)
                    {
                        a.transform.parent = b.transform;
                        c.transform.parent = b.transform;
                        b.RotateAround(jointdb.position, rotateAxis, speed);
                        rotateAngle += speed;
                        ResetParent();
                    }
                    else
                    {
                        d.RotateAround(jointbd.position, rotateAxis, speed);
                        rotateAngle += speed;

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

            ResetParent();
        }

        void ResetParent()
        {
            a.transform.parent = this.transform;
            b.transform.parent = this.transform;
            c.transform.parent = this.transform;
            d.transform.parent = this.transform;
        }

        void CrossProduct()
        {
            float x1 = jointbuffer.position.x - buffer1.position.x;
            float y1 = jointbuffer.position.y - buffer1.position.y;
            float z1 = jointbuffer.position.z - buffer1.position.z;

            float x2 = jointbuffer.position.x - buffer2.position.x;
            float y2 = jointbuffer.position.y - buffer2.position.y;
            float z2 = jointbuffer.position.z - buffer2.position.z;

            float x3 = y1 * z2 - y2 * z1;
            float y3 = z1 * x2 - z2 * x1;
            float z3 = x1 * y2 - x2 * y1;

            if (y3 < 0 || x3 < 0 || z3 < 0)
                _adjust = -1;
            else
                _adjust = 1;
        }

        void SetRotateAnchor()
        {
            if (_random == 0 || _random == 6)
            {
                plane = new Plane(a.position, b.position, jointab.position);
            }

            if (_random == 1 || _random == 7)
            {
                plane = new Plane(a.position, c.position, jointac.position);
            }

            if (_random == 2 || _random == 8)
            {
                plane = new Plane(b.position, d.position, jointbd.position);
            }

            if (_random == 3 || _random == 9)
            {
                plane = new Plane(b.position, a.position, jointab.position);
            }

            if (_random == 4 || _random == 10)
            {
                plane = new Plane(c.position, a.position, jointac.position);
            }

            if (_random == 5 || _random == 11)
            {
                plane = new Plane(d.position, b.position, jointbd.position);
            }

            rotateAxis = plane.normal;
        }
    }
}


