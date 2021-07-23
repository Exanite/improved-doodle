using UnityEngine;

namespace Exanite.ImprovedDoodle
{
    public class Painter : MonoBehaviour
    {
        public GameObject brushPrefab;

        public new Camera camera;
        public DoodleCanvas doodleCanvas;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (doodleCanvas.Plane.Raycast(ray, out var distance))
                {
                    var position = ray.GetPoint(distance);
                    var rotation = Random.rotation;

                    doodleCanvas.Paint(brushPrefab, position, rotation);
                }
            }
        }
    }
}