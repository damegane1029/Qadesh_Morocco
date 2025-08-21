
Shader "Custom/billboard_Y"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _amplitude ("amplitude",float) = 2
        _R ("R", Range(0, 1)) = 1
        _G ("G", Range(0, 1)) = 1
        _B ("B", Range(0, 1)) = 1
        _Tint ("Tint", Color) = (1,1,1,1)

    }
    SubShader
    {
   Blend SrcAlpha One


    Tags { "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "DisableBatching" = "True" 
            }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };





            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _amplitude;
            float _R;
            float _G;
            float _B;
            fixed4 _Tint;

            v2f vert (appdata v)
            {
                v2f o;
                {
                    float3 viewPos = UnityObjectToViewPos(float3(0, 0, 0));
                    float3 scaleRotPos = mul((float3x3)unity_ObjectToWorld, v.vertex);                
                    float3x3 ViewRotY = float3x3(
                        1, UNITY_MATRIX_V._m01, 0,
                        0, UNITY_MATRIX_V._m11, 0,
                        0, UNITY_MATRIX_V._m21, -1
                    );
                    viewPos += mul(ViewRotY, scaleRotPos);
                    o.vertex = mul(UNITY_MATRIX_P, float4(viewPos, 1));

                }
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                col *= _Tint ;
                col.r *= _R ;
                col.g *= _G ;
                col.b *= _B ;
                col *= _amplitude ;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}