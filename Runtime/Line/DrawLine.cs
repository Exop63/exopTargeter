using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Exop.Targeter
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLine : MonoBehaviour
    {
        [Header("Draw a debug line")]
        public bool debug=false;
        // a pointer for collision point
        [Header("Reflection point sprite")]
        public GameObject reflectionPoint;
        public StartPointData startPointData;
        private LineRenderer lineRenderer;

        private RaycastHit2D hit2D;
        private GameObject reflectionIndicator;
        private bool isTargeting=false;

        private void Awake()
        {
                this.lineRenderer = this.GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer not found!");
                Destroy(this);
            }

            Hide();
        }

         void Update() {
            if(startPointData.canShot && isTargeting) {
                setPositions();
            }
        }
        public void setPositions()
        {

            // check can draw line 
            this.hit2D = Physics2D.CircleCast(this.startPointData.startPosition, .4f, this.startPointData.direction, 200);
            // Debug.Log("Positon changeing hit2D:" + this.hit2D);
            if (hit2D)
            {
                // two position for line renderer
                Vector3[] positions = new Vector3[2];

                positions[0] = startPointData.startPosition;
                positions[1] = hit2D.centroid;

                lineRenderer.SetPositions(positions);

                this.ShowLine();
                this.showIndicator();
            }

        
        }


        private void showIndicator()
        {
            if (reflectionPoint)
            {
                if (this.reflectionIndicator == null)
                {
                    this.reflectionIndicator = Instantiate<GameObject>(reflectionPoint);
                    this.reflectionIndicator.name = "Indicator";
                }
            }
            else return;

            this.reflectionIndicator.SetActive(this.startPointData.canShot);
            this.reflectionIndicator.transform.position = this.lineRenderer.GetPosition(1);

        }
        // Show line
        public void ShowLine()
        {
            this.lineRenderer.enabled = this.startPointData.canShot;

        }
        // Hide line
        public void Hide()
        {
            isTargeting=false;

            // Debug.Log("Hide Line");
            this.lineRenderer.enabled = false;
            if (this.reflectionIndicator)
            {
                this.reflectionIndicator.SetActive(false);
            }
        }
        public void StartTargeting(){
            isTargeting=true;
        }
    }

}