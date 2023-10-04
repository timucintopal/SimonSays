using MoonActive.Scripts.Controller;
using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class Input : MonoBehaviour
    {
        private Camera _camera;
        private Ray _ray;
        private RaycastHit _hit;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0))
                Ray();
        }

        void Ray()
        {
            _ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, LayerMask.GetMask("Button")))
            {
                var rb = _hit.collider.attachedRigidbody;

                if (rb.TryGetComponent(out ButtonController script))
                {
                    script.OnClick();
                }
            }
        }
    }
}
