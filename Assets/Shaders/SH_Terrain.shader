// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33222,y:32748,varname:node_4013,prsc:2|diff-5787-OUT,spec-637-OUT;n:type:ShaderForge.SFN_Lerp,id:3854,x:32372,y:32814,varname:node_3854,prsc:2|A-5961-RGB,B-3106-RGB,T-7334-R;n:type:ShaderForge.SFN_VertexColor,id:7334,x:32130,y:33023,varname:node_7334,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:5961,x:32111,y:32681,ptovrint:False,ptlb:node_5961,ptin:_node_5961,varname:node_5961,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:68bd75d8c02bd104a90ac15757a2d797,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3106,x:32095,y:32858,ptovrint:False,ptlb:node_3106,ptin:_node_3106,varname:node_3106,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2ae128520e667f84f8d727ecff6d792b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1101,x:32449,y:33016,ptovrint:False,ptlb:node_1101,ptin:_node_1101,varname:node_1101,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e666403b151f304ebfed43403c91426,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:2288,x:32654,y:32826,varname:node_2288,prsc:2|A-3854-OUT,B-1101-RGB,T-7334-G;n:type:ShaderForge.SFN_Desaturate,id:637,x:32707,y:32990,varname:node_637,prsc:2|COL-2288-OUT;n:type:ShaderForge.SFN_Tex2d,id:394,x:32494,y:32556,ptovrint:False,ptlb:node_394,ptin:_node_394,varname:node_394,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7894606502207ce409cf3723ab77e5cc,ntxv:0,isnm:False|UVIN-8713-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5787,x:32899,y:32706,varname:node_5787,prsc:2|A-394-RGB,B-2288-OUT;n:type:ShaderForge.SFN_Panner,id:8713,x:32273,y:32501,varname:node_8713,prsc:2,spu:1,spv:1|UVIN-6117-UVOUT,DIST-4724-OUT;n:type:ShaderForge.SFN_TexCoord,id:6117,x:31679,y:32446,varname:node_6117,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:7988,x:31832,y:32611,varname:node_7988,prsc:2;n:type:ShaderForge.SFN_Divide,id:4724,x:32042,y:32544,varname:node_4724,prsc:2|A-7988-T,B-3383-OUT;n:type:ShaderForge.SFN_Vector1,id:3383,x:31962,y:32729,varname:node_3383,prsc:2,v1:60;proporder:5961-3106-1101-394;pass:END;sub:END;*/

Shader "Shader Forge/SH_Terrain" {
    Properties {
        _node_5961 ("node_5961", 2D) = "white" {}
        _node_3106 ("node_3106", 2D) = "white" {}
        _node_1101 ("node_1101", 2D) = "white" {}
        _node_394 ("node_394", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform sampler2D _node_5961; uniform float4 _node_5961_ST;
            uniform sampler2D _node_3106; uniform float4 _node_3106_ST;
            uniform sampler2D _node_1101; uniform float4 _node_1101_ST;
            uniform sampler2D _node_394; uniform float4 _node_394_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _node_5961_var = tex2D(_node_5961,TRANSFORM_TEX(i.uv0, _node_5961));
                float4 _node_3106_var = tex2D(_node_3106,TRANSFORM_TEX(i.uv0, _node_3106));
                float4 _node_1101_var = tex2D(_node_1101,TRANSFORM_TEX(i.uv0, _node_1101));
                float3 node_2288 = lerp(lerp(_node_5961_var.rgb,_node_3106_var.rgb,i.vertexColor.r),_node_1101_var.rgb,i.vertexColor.g);
                float node_637 = dot(node_2288,float3(0.3,0.59,0.11));
                float3 specularColor = float3(node_637,node_637,node_637);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_7988 = _Time + _TimeEditor;
                float2 node_8713 = (i.uv0+(node_7988.g/60.0)*float2(1,1));
                float4 _node_394_var = tex2D(_node_394,TRANSFORM_TEX(node_8713, _node_394));
                float3 diffuseColor = (_node_394_var.rgb*node_2288);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            uniform sampler2D _node_5961; uniform float4 _node_5961_ST;
            uniform sampler2D _node_3106; uniform float4 _node_3106_ST;
            uniform sampler2D _node_1101; uniform float4 _node_1101_ST;
            uniform sampler2D _node_394; uniform float4 _node_394_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _node_5961_var = tex2D(_node_5961,TRANSFORM_TEX(i.uv0, _node_5961));
                float4 _node_3106_var = tex2D(_node_3106,TRANSFORM_TEX(i.uv0, _node_3106));
                float4 _node_1101_var = tex2D(_node_1101,TRANSFORM_TEX(i.uv0, _node_1101));
                float3 node_2288 = lerp(lerp(_node_5961_var.rgb,_node_3106_var.rgb,i.vertexColor.r),_node_1101_var.rgb,i.vertexColor.g);
                float node_637 = dot(node_2288,float3(0.3,0.59,0.11));
                float3 specularColor = float3(node_637,node_637,node_637);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_7988 = _Time + _TimeEditor;
                float2 node_8713 = (i.uv0+(node_7988.g/60.0)*float2(1,1));
                float4 _node_394_var = tex2D(_node_394,TRANSFORM_TEX(node_8713, _node_394));
                float3 diffuseColor = (_node_394_var.rgb*node_2288);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
