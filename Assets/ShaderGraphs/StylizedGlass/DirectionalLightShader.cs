using UnityEngine;

namespace ShaderGraphs.StylizedGlass
{
    [ExecuteInEditMode]
    public class DirectionalLightShader : MonoBehaviour
    {
        void Update()
        {
            Shader.SetGlobalVector("_LightDirectionVec", -transform.forward);
        }
    }
}
