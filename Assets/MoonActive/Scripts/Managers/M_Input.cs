using MoonActive.Scripts.Controller;
using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class M_Input : MonoBehaviour
    {
        Camera _camera;
        Ray _ray;
        RaycastHit _hit;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                Ray();
        }

        void Ray()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, LayerMask.GetMask("Button")))
            {
                var rb = _hit.collider.attachedRigidbody;

                if (rb.TryGetComponent(out ButtonController script))
                {
                    Debug.Log("Button FOUND ! ");
                    script.OnClick();
                }
            }
        }
    }
}
