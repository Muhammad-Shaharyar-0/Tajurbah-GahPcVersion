using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Tajurbah_Gah
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        public Action followWaypointCallBack = null;

        [Header("Main Camera Object")]
        [SerializeField] GameObject mainCamera;

        [Header("Main Camera")]
        [SerializeField] Camera camera;

        [SerializeField] int Zoomlevel = 20;

        [Header("Follow Waypoints")]
        [SerializeField] bool shouldFollowWaypoints = false;
        [SerializeField] bool onStartFollowWaypoints = false;
        [SerializeField] float speedToFollowWaypoint = 10f;
        [SerializeField] Transform waypointsMain;
        Transform[] waypoints;
        int wayPointsCount = 0;
        CoroutineQueue coroutineQueueObject;

        public bool MoveVertical;
        public bool MoveHorizontal;

        Vector3 Initialposition;
        Quaternion Initialrotation;

        bool Zoomed = false;

        public float speed;
        float DirX, DirY;

        bool resetCamera = false;


        private void Awake()
        {
            Instance = this;
            followWaypointCallBack = FollowWaypoint;
        }

        private void Start()
        {
            //Joystick.Instance.callBack = () => { resetCamera = true; };

            waypoints = new Transform[waypointsMain.childCount];
            for (int i = 0; i < waypointsMain.childCount; i++)
            {
                waypoints[i] = waypointsMain.GetChild(i);
            }

            coroutineQueueObject = new CoroutineQueue(1, StartCoroutine);

            if (shouldFollowWaypoints && onStartFollowWaypoints)
            {
                FollowWaypoint();
            }
            Initialposition = transform.position;
            Initialrotation = transform.rotation;
            Zoomed = false;
           

        }

        void FollowWaypoint()
        {
            if (wayPointsCount<waypoints.Length)
            {
                coroutineQueueObject.Run(FollowWaypointRoutine());
            }
        }

        IEnumerator FollowWaypointRoutine()
        {
            while(mainCamera.transform.position != waypoints[wayPointsCount].position)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, waypoints[wayPointsCount].position, speedToFollowWaypoint * Time.deltaTime);
                yield return null;
            }
            wayPointsCount++;
        }

        // Update is called once per frame
        void Update()
        {
            DirX = MoveVertical ? CrossPlatformInputManager.GetAxis("Vertical") * speed : 0;
            DirY = MoveHorizontal ? CrossPlatformInputManager.GetAxis("Horizontal") * -speed : 0;

            if (DirX == 0 && DirY == 0 && resetCamera == true)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), speed);

                if (transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    resetCamera = false;
                }
            }
            else
            {
                transform.Rotate(DirX, DirY, 0);
            }
        }

        public void Zoom()
        {
            if (Zoomed)
            {
                // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-0.5f);
                camera.fieldOfView = 60;
                Zoomed = false;

            }
            else
            {

                //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                camera.fieldOfView-= Zoomlevel;
                Zoomed = true;
            }
       
        }
        public void Reset()
        {
            transform.position= Initialposition;
            transform.rotation = Initialrotation;
        }
    }
}