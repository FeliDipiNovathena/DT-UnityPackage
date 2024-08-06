using UnityEngine;

namespace DT.Scripts.Utils
{
    public class ZoomManager : MonoBehaviour
    {
        [SerializeField] private Vector2 _fieldViewRange = new Vector2(30f, 80f);
        [SerializeField] private float _fieldViewStep = 10f;

        private Vector2 _touchZeroPrevPos;
        private Vector2 _touchOnePrevPos;
        private Vector2 _touchRotationPrevPos;
        private float _prevTouchDeltaMag;
        private bool _isZooming = false;
        private bool _isRotating = false;

        private float _originalFieldView;

        private void Start()
        {
            _touchZeroPrevPos = Vector2.zero;
            _touchOnePrevPos = Vector2.zero;
            _touchRotationPrevPos = Vector2.zero;
            _prevTouchDeltaMag = 0f;

            _originalFieldView = Camera.main.fieldOfView;
        }

        private void FixedUpdate()
        {
            RotateByTouch();
            ZoomByTouch();
        }

        private void ZoomByTouch()
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                {
                    if (_isZooming == false)
                    {
                        _isZooming = true;
                        _touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        _touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                        _prevTouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                        return;
                    }
                    Vector2 touchZeroDeltaPos = touchZero.position - _touchZeroPrevPos;
                    Vector2 touchOneDeltaPos = touchOne.position - _touchOnePrevPos;

                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                    float deltaMagnitudeDiff = _prevTouchDeltaMag - touchDeltaMag;

                    Camera.main.transform.Translate(Vector3.forward * -deltaMagnitudeDiff * 0.020f, Space.Self);

                    _touchZeroPrevPos = touchZero.position - touchZeroDeltaPos;
                    _touchOnePrevPos = touchOne.position - touchOneDeltaPos;
                    _prevTouchDeltaMag = touchDeltaMag;
                }
                else if (touchZero.phase == TouchPhase.Stationary && touchOne.phase == TouchPhase.Stationary)
                {
                    return;
                }
                else if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended)
                {
                    _isZooming = false;
                }
            }
            else
            {
                _isZooming = false;
            }
        }

        private void RotateByTouch()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began || (touch.phase == TouchPhase.Moved && !_isRotating))
                {
                    _isRotating = true;
                    _touchRotationPrevPos = touch.position;
                    return;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    if (_isRotating)
                    {
                        Vector2 touchDeltaPos = touch.position - _touchRotationPrevPos;

                        float rotationSpeed = 0.25f;
                        float rotationX = touchDeltaPos.x * rotationSpeed;
                        float rotationY = touchDeltaPos.y * rotationSpeed;

                        Camera.main.transform.Rotate(Vector3.up, rotationX, Space.World);
                        Camera.main.transform.Rotate(Vector3.right, -rotationY, Space.Self);

                        _touchRotationPrevPos = touch.position;
                    }
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    _isRotating = true;
                    return;
                }
            }
            else
            {
                _isRotating = false;
            }
        }

        public void ZoomIn()
        {
            Camera.main.fieldOfView = Mathf.Max(Camera.main.fieldOfView - _fieldViewStep, _fieldViewRange.x);
        }

        public void ZoomOut()
        {
            Camera.main.fieldOfView = Mathf.Min(Camera.main.fieldOfView + _fieldViewStep, _fieldViewRange.y);
        }

        public void ResetZoom()
        {
            Camera.main.fieldOfView = _originalFieldView;
        }
    }
}