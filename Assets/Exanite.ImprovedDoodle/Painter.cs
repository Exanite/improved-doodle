using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Exanite.ImprovedDoodle
{
    public class Painter : MonoBehaviour
    {
        [Header("Configuration")]
        public GameObject[] brushes;
        public Camera camera;
        public DoodleCanvas doodleCanvas;

        public Vector3 hsv = new Vector3(0, 1, 1);
        public float scrollHueChangeAmountDegrees = 5;

        [Header("Do not edit")]
        public GameObject currentBrush;

        private void Start()
        {
            currentBrush = brushes[0];
        }

        private void Update()
        {
            for (var i = 0; i < 9; i++)
            {
                if (Input.GetKey(KeyCode.Alpha1 + i))
                {
                    if (brushes[i])
                    {
                        currentBrush = brushes[i];
                    }
                }
            }

            hsv.x += Input.mouseScrollDelta.y * (scrollHueChangeAmountDegrees / 360);
            hsv.x = Mathf.Repeat(hsv.x, 1);

            if (Input.GetKey(KeyCode.Mouse0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (doodleCanvas.Plane.Raycast(ray, out var distance))
                {
                    var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                    var position = ray.GetPoint(distance);
                    var rotation = Random.rotation;

                    doodleCanvas.Paint(currentBrush, color, position, rotation);
                }
            }
        }
    }
}