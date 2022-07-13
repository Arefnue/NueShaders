using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShaderGraphs.InteractiveRipple
{
    public class InteractiveRippleController : MonoBehaviour
    {
        private Material _material;
        private Color _lastColor;
        private Color _startColor;
        private Camera _mainCam;
        
        private struct ShaderPropertyIDs
        {
            public int BaseColor;
            public int RippleColor;
            public int RippleCenter;
            public int RippleStartTime;
        }

        private ShaderPropertyIDs _shaderProps;
        private void Start()
        {
           
            _mainCam = Camera.main;
            _material =GetComponent<MeshRenderer>().material;

            _shaderProps = new ShaderPropertyIDs()
            {
                BaseColor = Shader.PropertyToID("_BaseColor"),
                RippleColor = Shader.PropertyToID("_RippleColor"),
                RippleCenter = Shader.PropertyToID("_RippleCenter"),
                RippleStartTime = Shader.PropertyToID("_RippleStartTime")
            };
            
            _startColor = _material.GetColor(_shaderProps.BaseColor);
            _lastColor = _startColor;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;

                var ray = _mainCam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, _mainCam.nearClipPlane));

                if (Physics.Raycast(ray,out var hit) && hit.collider.gameObject == gameObject)
                {
                    var rippleColor = Color.HSVToRGB(Random.value, 1, 1);
                    _material.SetVector(_shaderProps.RippleCenter,hit.point);
                    _material.SetFloat(_shaderProps.RippleStartTime,Time.time);
                    _material.SetColor(_shaderProps.BaseColor,_lastColor);
                    _material.SetColor(_shaderProps.RippleColor,rippleColor);

                    _lastColor = rippleColor;
                }
                
                
            }
        }
    }
}
