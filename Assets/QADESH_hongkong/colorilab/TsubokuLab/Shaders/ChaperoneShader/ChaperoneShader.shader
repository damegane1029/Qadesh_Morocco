// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:1,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.03773582,fgcg:0.03366162,fgcb:0.01940191,fgca:1,fgde:0.007,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6435,x:33501,y:32605,varname:node_6435,prsc:2|diff-9227-OUT,spec-8763-OUT,gloss-4449-OUT,normal-6638-RGB,emission-8290-OUT,alpha-1370-OUT,clip-8387-OUT;n:type:ShaderForge.SFN_ViewPosition,id:9864,x:31581,y:33166,varname:node_9864,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:1553,x:31581,y:33041,varname:node_1553,prsc:2;n:type:ShaderForge.SFN_Distance,id:1223,x:31787,y:33041,varname:node_1223,prsc:2|A-1553-XYZ,B-9864-XYZ;n:type:ShaderForge.SFN_Color,id:9547,x:32547,y:32536,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_9547,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:4449,x:32580,y:32796,ptovrint:False,ptlb:Roughness,ptin:_Roughness,varname:node_4449,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_Slider,id:8763,x:32580,y:32712,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_8763,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:5488,x:31630,y:32894,ptovrint:False,ptlb:Far,ptin:_Far,varname:node_5488,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:100;n:type:ShaderForge.SFN_Slider,id:2489,x:31630,y:32972,ptovrint:False,ptlb:Near,ptin:_Near,varname:node_2489,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:100;n:type:ShaderForge.SFN_InverseLerp,id:9993,x:32011,y:32930,varname:node_9993,prsc:2|A-5488-OUT,B-2489-OUT,V-1223-OUT;n:type:ShaderForge.SFN_Clamp01,id:6941,x:32183,y:32930,varname:node_6941,prsc:2|IN-9993-OUT;n:type:ShaderForge.SFN_Tex2d,id:4148,x:32547,y:32360,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4148,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2886-OUT;n:type:ShaderForge.SFN_Multiply,id:9196,x:32737,y:32516,varname:node_9196,prsc:2|A-4148-RGB,B-9547-RGB;n:type:ShaderForge.SFN_Tex2d,id:6638,x:33046,y:32750,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6638,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8862,x:32946,y:32903,varname:node_8862,prsc:2|A-4148-A,B-9547-A,C-6941-OUT;n:type:ShaderForge.SFN_Vector1,id:7240,x:32737,y:32634,varname:node_7240,prsc:2,v1:0;n:type:ShaderForge.SFN_ToggleProperty,id:2940,x:33060,y:32433,ptovrint:False,ptlb:IsEmissive,ptin:_IsEmissive,varname:node_2940,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_If,id:9227,x:33267,y:32433,varname:node_9227,prsc:2|A-2940-OUT,B-7240-OUT,GT-7240-OUT,EQ-9196-OUT,LT-9196-OUT;n:type:ShaderForge.SFN_If,id:8290,x:33267,y:32566,varname:node_8290,prsc:2|A-2940-OUT,B-7240-OUT,GT-9196-OUT,EQ-7240-OUT,LT-7240-OUT;n:type:ShaderForge.SFN_Vector1,id:9095,x:32183,y:33060,varname:node_9095,prsc:2,v1:0.499;n:type:ShaderForge.SFN_Add,id:8387,x:32408,y:33014,varname:node_8387,prsc:2|A-6941-OUT,B-9095-OUT;n:type:ShaderForge.SFN_Slider,id:9191,x:32002,y:32269,ptovrint:False,ptlb:TexScale,ptin:_TexScale,varname:node_9191,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1,max:100;n:type:ShaderForge.SFN_TexCoord,id:9471,x:31993,y:32349,varname:node_9471,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2886,x:32339,y:32327,varname:node_2886,prsc:2|A-9191-OUT,B-9471-UVOUT;n:type:ShaderForge.SFN_Fmod,id:964,x:32345,y:32509,varname:node_964,prsc:2|A-935-OUT,B-2291-OUT;n:type:ShaderForge.SFN_Slider,id:2291,x:31630,y:32701,ptovrint:False,ptlb:SlitInterbal,ptin:_SlitInterbal,varname:node_2291,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:5756,x:33116,y:32992,varname:node_5756,prsc:2|A-8862-OUT,B-9433-OUT;n:type:ShaderForge.SFN_Time,id:979,x:31787,y:32425,varname:node_979,prsc:2;n:type:ShaderForge.SFN_Add,id:935,x:32189,y:32509,varname:node_935,prsc:2|A-6810-OUT,B-1241-OUT,C-7229-OUT;n:type:ShaderForge.SFN_Slider,id:7991,x:31630,y:32622,ptovrint:False,ptlb:TimeScale,ptin:_TimeScale,varname:node_7991,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:1241,x:31993,y:32509,varname:node_1241,prsc:2|A-979-T,B-7991-OUT;n:type:ShaderForge.SFN_Vector1,id:7794,x:31787,y:32766,varname:node_7794,prsc:2,v1:-1;n:type:ShaderForge.SFN_Divide,id:6665,x:32189,y:32645,varname:node_6665,prsc:2|A-964-OUT,B-2291-OUT;n:type:ShaderForge.SFN_Multiply,id:6810,x:31993,y:32745,varname:node_6810,prsc:2|A-1223-OUT,B-7794-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1370,x:33284,y:32957,ptovrint:False,ptlb:UseAnimation,ptin:_UseAnimation,varname:node_1370,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8862-OUT,B-5756-OUT;n:type:ShaderForge.SFN_Vector1,id:6230,x:31787,y:32543,varname:node_6230,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:7850,x:32189,y:32778,varname:node_7850,prsc:2|IN-8594-OUT;n:type:ShaderForge.SFN_Abs,id:8594,x:32345,y:32645,varname:node_8594,prsc:2|IN-6665-OUT;n:type:ShaderForge.SFN_OneMinus,id:9433,x:32345,y:32778,varname:node_9433,prsc:2|IN-7850-OUT;n:type:ShaderForge.SFN_Vector1,id:7229,x:31993,y:32628,varname:node_7229,prsc:2,v1:10;proporder:9547-4148-9191-2940-6638-4449-8763-5488-2489-1370-2291-7991;pass:END;sub:END;*/

Shader "TsubokuLab/ChaperoneShader" {
    Properties {
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _TexScale ("TexScale", Range(0.1, 100)) = 1
        [MaterialToggle] _IsEmissive ("IsEmissive", Float ) = 0
        _Normal ("Normal", 2D) = "bump" {}
        _Roughness ("Roughness", Range(0, 1)) = 0.2
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Far ("Far", Range(0, 100)) = 10
        _Near ("Near", Range(0, 100)) = 2
        [MaterialToggle] _UseAnimation ("UseAnimation", Float ) = 0
        _SlitInterbal ("SlitInterbal", Range(0, 10)) = 1
        _TimeScale ("TimeScale", Range(0, 2)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _MainColor;
            uniform float _Roughness;
            uniform float _Metallic;
            uniform float _Far;
            uniform float _Near;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform fixed _IsEmissive;
            uniform float _TexScale;
            uniform float _SlitInterbal;
            uniform float _TimeScale;
            uniform fixed _UseAnimation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Normal_var = tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_1223 = distance(i.posWorld.rgb,_WorldSpaceCameraPos);
                float node_6941 = saturate(((node_1223-_Far)/(_Near-_Far)));
                clip((node_6941+0.499) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 1.0 - _Roughness; // Convert roughness to gloss
                float perceptualRoughness = _Roughness;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float node_7240 = 0.0;
                float node_9227_if_leA = step(_IsEmissive,node_7240);
                float node_9227_if_leB = step(node_7240,_IsEmissive);
                float2 node_2886 = (_TexScale*i.uv0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2886, _MainTex));
                float3 node_9196 = (_MainTex_var.rgb*_MainColor.rgb);
                float3 diffuseColor = lerp((node_9227_if_leA*node_9196)+(node_9227_if_leB*node_7240),node_9196,node_9227_if_leA*node_9227_if_leB); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_8290_if_leA = step(_IsEmissive,node_7240);
                float node_8290_if_leB = step(node_7240,_IsEmissive);
                float3 emissive = lerp((node_8290_if_leA*node_7240)+(node_8290_if_leB*node_9196),node_7240,node_8290_if_leA*node_8290_if_leB);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                float node_8862 = (_MainTex_var.a*_MainColor.a*node_6941);
                float4 node_979 = _Time;
                fixed4 finalRGBA = fixed4(finalColor,lerp( node_8862, (node_8862*(1.0 - saturate(abs((fmod(((node_1223*(-1.0))+(node_979.g*_TimeScale)+10.0),_SlitInterbal)/_SlitInterbal))))), _UseAnimation ));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _MainColor;
            uniform float _Roughness;
            uniform float _Metallic;
            uniform float _Far;
            uniform float _Near;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform fixed _IsEmissive;
            uniform float _TexScale;
            uniform float _SlitInterbal;
            uniform float _TimeScale;
            uniform fixed _UseAnimation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Normal_var = tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_1223 = distance(i.posWorld.rgb,_WorldSpaceCameraPos);
                float node_6941 = saturate(((node_1223-_Far)/(_Near-_Far)));
                clip((node_6941+0.499) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 1.0 - _Roughness; // Convert roughness to gloss
                float perceptualRoughness = _Roughness;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float node_7240 = 0.0;
                float node_9227_if_leA = step(_IsEmissive,node_7240);
                float node_9227_if_leB = step(node_7240,_IsEmissive);
                float2 node_2886 = (_TexScale*i.uv0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2886, _MainTex));
                float3 node_9196 = (_MainTex_var.rgb*_MainColor.rgb);
                float3 diffuseColor = lerp((node_9227_if_leA*node_9196)+(node_9227_if_leB*node_7240),node_9196,node_9227_if_leA*node_9227_if_leB); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                float node_8862 = (_MainTex_var.a*_MainColor.a*node_6941);
                float4 node_979 = _Time;
                fixed4 finalRGBA = fixed4(finalColor * lerp( node_8862, (node_8862*(1.0 - saturate(abs((fmod(((node_1223*(-1.0))+(node_979.g*_TimeScale)+10.0),_SlitInterbal)/_SlitInterbal))))), _UseAnimation ),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Far;
            uniform float _Near;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float node_1223 = distance(i.posWorld.rgb,_WorldSpaceCameraPos);
                float node_6941 = saturate(((node_1223-_Far)/(_Near-_Far)));
                clip((node_6941+0.499) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
