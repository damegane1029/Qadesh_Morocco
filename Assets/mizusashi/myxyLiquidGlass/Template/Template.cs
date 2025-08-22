using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class Template : MonoBehaviour
{
    const int shapeTextureSize = 32;
    [SerializeField, Range(0,1)]
    private float[] radius = new float[shapeTextureSize];
    [SerializeField]
    private string fileName;
    void Update()
    {
        var target = this.GetComponent<MeshRenderer>().sharedMaterial;
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

    public void GenerateShapeTexture()
    {
        Texture2D shapeTex = new Texture2D(shapeTextureSize, 32, TextureFormat.RGBAFloat, false);
        for (int i=0; i<shapeTextureSize; i++)
        {
            var colors = new Color[32];
            for (int j=0; j<32; j++)
            {
                colors[j] = new Color(radius[i], radius[i], radius[i], 1);
            }
            shapeTex.SetPixels(i,0,1,32, colors, 0);
        }
        byte[] bytes = shapeTex.EncodeToEXR(Texture2D.EXRFlags.CompressZIP);
        File.WriteAllBytes(Application.dataPath + "/myxyLiquidGlass/Template/" + fileName + ".exr", bytes);
    }
}
