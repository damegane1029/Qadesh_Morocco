
// Created by myxy.
// Redistribution is prohibited.
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Myxy.Glass.Editor
{
    public static class RefreshGlass
    {
        [InitializeOnLoadMethod]
        private static void registerWindowChangedCallback()
        {
            EditorApplication.hierarchyChanged += RefreshAllGlass;
            EditorApplication.projectChanged += RefreshAllGlass;
            Undo.undoRedoPerformed += RefreshAllGlass;
            EditorSceneManager.sceneOpening += (scene, mode) => RefreshAllGlass();
            EditorSceneManager.sceneOpened += (scene, path) => RefreshAllGlass();
            EditorSceneManager.sceneSaving += (scene, path) => RefreshAllGlass();
            EditorSceneManager.sceneSaved += (scene) => RefreshAllGlass();
            EditorSceneManager.activeSceneChangedInEditMode += (current, next) =>RefreshAllGlass(); 
        }

        [UnityEditor.Callbacks.PostProcessScene(0)]
        private static void PostProcessScene()
        {
            RefreshAllGlass();
        }

        const int shapeTextureSize = 32;

        private static void RefreshAllGlass()
        {
            var glassShader = Shader.Find("Myxy/Glass/Glass");
            var glassShaderQuest = Shader.Find("Myxy/Glass/GlassQuest");
            var targets = Resources.FindObjectsOfTypeAll<MeshRenderer>()
                .Where(renderer => renderer.gameObject.name != "Template")
                .Select(renderer => renderer.sharedMaterial)
                .Where(material => (material?.shader == glassShader || material?.shader == glassShaderQuest));

            foreach (var target in targets)
            {
                var shapeTex = (Texture2D)target.GetTexture("_ShapeTex");
                var radius = shapeTex.GetPixels(0,0,32,1).Select(color => color.r).ToArray();
                target.SetFloatArray("_Radius", radius);
                Vector4[] crcoef = new Vector4[shapeTextureSize-1];
                for (int i=0; i<shapeTextureSize-1; i++)
                {
                    float y_1 = i > 0 ? radius[i-1] : radius[0];
                    float y0 = radius[i];
                    float y1 = radius[i+1];
                    float y2 = i < shapeTextureSize-2 ? radius[i+2] : radius[shapeTextureSize-1];
                
                    crcoef[i] = new Vector4(
                        y0,
                        -0.5f*y_1+0.5f*y1,
                        y_1-2.5f*y0+2.0f*y1-0.5f*y2,
                        -0.5f*y_1+1.5f*y0-1.5f*y1+0.5f*y2
                    );
                }
                target.SetVectorArray("_CRCoef", crcoef); 
            }
        }
    }
}
