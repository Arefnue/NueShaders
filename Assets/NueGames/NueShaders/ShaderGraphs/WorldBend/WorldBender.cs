using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShaderGraphs.WorldBend
{
    public class WorldBender : MonoBehaviour
    {
     
        public float bendSpeed=0.1f;
        [Header("CustomBend")]
        public bool useCustomBend;
        [Range(-0.0001f,0.0001f)]
        public float customXBend;
        [Range(0f,0.00005f)]
        public float customYBend;
    
        //WorldBender
        private float _maxWorldBendX = 0.0001f;
        private float _minWorldBendX = -0.0001f;
        private float _maxWorldBendY = 0f;
        private float _minWorldBendY = 0.00005f;

        private void Start()
        {
            StartCoroutine(nameof(RandomizeWorldBendRoutine));
        }

        
        private IEnumerator RandomizeWorldBendRoutine()
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;
        
            var randomXValue = 0f;
            var randomYValue = 0f;
        
            if (useCustomBend)
            {
                randomXValue = customXBend;
                randomYValue = customYBend;
            }
            else
            {
                randomXValue = Random.Range(_minWorldBendX, _maxWorldBendX);
                randomYValue = Random.Range(_minWorldBendY, _maxWorldBendY);
            }
            var initalXValue = Shader.GetGlobalFloat("_GlobalXBend");
            var initalYValue = Shader.GetGlobalFloat("_GlobalYBend");
            while (true)
            {
                timer += Time.deltaTime*bendSpeed;

                var xValue = Mathf.Lerp(initalXValue, randomXValue, timer);
                var yValue = Mathf.Lerp(initalYValue, randomYValue, timer);
            
                Shader.SetGlobalFloat("_GlobalXBend",xValue);
                Shader.SetGlobalFloat("_GlobalYBend",yValue);
            
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }

            StartCoroutine(nameof(RandomizeWorldBendRoutine));
        }
    
    }
}
