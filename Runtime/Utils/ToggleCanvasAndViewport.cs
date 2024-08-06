using UnityEngine;

namespace DT.Scripts.Utils
{
    public class ToggleCanvasAndViewport : MonoBehaviour
    {
        public Canvas multiCameraCanvas;
        public Camera mainCamera;

        private bool isMultiCameraActive = false;

        public void ToggleMultiCameraCanvas()
        {
            isMultiCameraActive = !isMultiCameraActive;
            multiCameraCanvas.gameObject.SetActive(isMultiCameraActive);
        }

        public void ActivateMultiCameraCanvas()
        {
            multiCameraCanvas.gameObject.SetActive(true);
        }

        public void DeactivateMultiCameraCanvas()
        {
            multiCameraCanvas.gameObject.SetActive(false);
        }
    }
}