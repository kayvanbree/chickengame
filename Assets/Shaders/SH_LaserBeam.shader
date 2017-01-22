// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33091,y:32697,varname:node_4013,prsc:2|diff-4529-OUT,emission-8906-OUT,clip-3547-R;n:type:ShaderForge.SFN_Tex2d,id:3547,x:32226,y:32735,ptovrint:False,ptlb:node_3547,ptin:_node_3547,varname:node_3547,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9599715847d974f4b9717256f4ece8d8,ntxv:0,isnm:False|UVIN-3057-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5845,x:31874,y:32868,ptovrint:False,ptlb:node_5845,ptin:_node_5845,varname:node_5845,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fadaadcbb1618b74392f0d0dd81dc92d,ntxv:0,isnm:False|UVIN-3057-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2422,x:31125,y:32754,varname:node_2422,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:3057,x:31670,y:32904,varname:node_3057,prsc:2,spu:0,spv:-2|UVIN-6541-OUT,DIST-5339-T;n:type:ShaderForge.SFN_Time,id:5339,x:31254,y:33010,varname:node_5339,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6541,x:31343,y:32777,varname:node_6541,prsc:2|A-2422-UVOUT,B-8997-OUT;n:type:ShaderForge.SFN_Vector2,id:8997,x:31156,y:32905,varname:node_8997,prsc:2,v1:1,v2:2;n:type:ShaderForge.SFN_TexCoord,id:7745,x:32080,y:32532,varname:node_7745,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3944,x:32317,y:32572,varname:node_3944,prsc:2|A-7745-U,B-5254-OUT;n:type:ShaderForge.SFN_Vector1,id:5254,x:32049,y:32702,varname:node_5254,prsc:2,v1:1;n:type:ShaderForge.SFN_OneMinus,id:620,x:32317,y:32420,varname:node_620,prsc:2|IN-7745-U;n:type:ShaderForge.SFN_Blend,id:6094,x:32506,y:32499,varname:node_6094,prsc:2,blmd:17,clmp:True|SRC-620-OUT,DST-3944-OUT;n:type:ShaderForge.SFN_OneMinus,id:9432,x:32670,y:32561,varname:node_9432,prsc:2|IN-6094-OUT;n:type:ShaderForge.SFN_Multiply,id:4529,x:32702,y:32694,varname:node_4529,prsc:2|A-3547-RGB,B-255-RGB;n:type:ShaderForge.SFN_Color,id:255,x:32360,y:32956,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_255,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.6689656,c4:1;n:type:ShaderForge.SFN_Multiply,id:8906,x:32912,y:32789,varname:node_8906,prsc:2|A-4529-OUT,B-8878-OUT;n:type:ShaderForge.SFN_Vector1,id:8878,x:32708,y:32842,varname:node_8878,prsc:2,v1:10;proporder:3547-5845-255;pass:END;sub:END;*/

Shader "Shader Forge/SH_LaserBeam" {
    Properties {
        _node_3547 ("node_3547", 2D) = "white" {}
        _node_5845 ("node_5845", 2D) = "white" {}
        _Color ("Color", Color) = (0,1,0.6689656,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_3547; uniform float4 _node_3547_ST;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_5339 = _Time + _TimeEditor;
                float2 node_3057 = ((i.uv0*float2(1,2))+node_5339.g*float2(0,-2));
                float4 _node_3547_var = tex2D(_node_3547,TRANSFORM_TEX(node_3057, _node_3547));
                clip(_node_3547_var.r - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 node_4529 = (_node_3547_var.rgb*_Color.rgb);
                float3 diffuseColor = node_4529;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_4529*10.0);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_3547; uniform float4 _node_3547_ST;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_5339 = _Time + _TimeEditor;
                float2 node_3057 = ((i.uv0*float2(1,2))+node_5339.g*float2(0,-2));
                float4 _node_3547_var = tex2D(_node_3547,TRANSFORM_TEX(node_3057, _node_3547));
                clip(_node_3547_var.r - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_4529 = (_node_3547_var.rgb*_Color.rgb);
                float3 diffuseColor = node_4529;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_3547; uniform float4 _node_3547_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_5339 = _Time + _TimeEditor;
                float2 node_3057 = ((i.uv0*float2(1,2))+node_5339.g*float2(0,-2));
                float4 _node_3547_var = tex2D(_node_3547,TRANSFORM_TEX(node_3057, _node_3547));
                clip(_node_3547_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
