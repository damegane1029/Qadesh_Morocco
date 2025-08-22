using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.SceneManagement;
using System.Linq;


namespace Myxy.Glass.Editor
{
    public class SwitchPlatform : IActiveBuildTargetChanged
    {
        public int callbackOrder { get { return 0; } }
        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            var glassShader = Shader.Find("Myxy/Glass/Glass");
            var glassShaderQuest = Shader.Find("Myxy/Glass/GlassQuest");
            var targets = Resources.FindObjectsOfTypeAll<MeshRenderer>()
                    .Where(renderer => renderer.gameObject.name != "Template")
                    .Select(renderer => renderer.sharedMaterial)
                    .Where(material => (material?.shader == glassShader || material?.shader == glassShaderQuest));

            foreach (var target in targets)
            {
                if (newTarget == BuildTarget.Android)
                {
                    target.shader = glassShaderQuest;
                }
                else
                {
                    target.shader = glassShader;
                }
            }

            var glassStreamShader = Shader.Find("Myxy/Glass/Stream");
            var glassStreamShaderQuest = Shader.Find("Myxy/Glass/StreamQuest");
            var streamTargets = Resources.FindObjectsOfTypeAll<SkinnedMeshRenderer>()
                    .Select(renderer => renderer.sharedMaterial)
                    .Where(material => (material?.shader == glassStreamShader || material?.shader == glassStreamShaderQuest));

            foreach (var target in streamTargets)
            {
                if (newTarget == BuildTarget.Android)
                {
                    target.shader = glassStreamShaderQuest;
                }
                else
                {
                    target.shader = glassStreamShader;
                }
            }

       
        }
    }
}
