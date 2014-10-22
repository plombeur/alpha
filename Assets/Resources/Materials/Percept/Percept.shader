// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32457,y:32681|diff-81-OUT,emission-53-A,alpha-31-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2,x:33568,y:32607,ptlb:Angle,ptin:_Angle,glob:False,v1:90;n:type:ShaderForge.SFN_Min,id:3,x:33377,y:32626|A-2-OUT,B-4-OUT;n:type:ShaderForge.SFN_Vector1,id:4,x:33568,y:32670,v1:360;n:type:ShaderForge.SFN_TexCoord,id:5,x:34351,y:32812,uv:0;n:type:ShaderForge.SFN_ArcCos,id:8,x:33826,y:32804|IN-52-OUT;n:type:ShaderForge.SFN_If,id:17,x:32838,y:32921|A-25-OUT,B-41-OUT,GT-23-OUT,EQ-22-OUT,LT-22-OUT;n:type:ShaderForge.SFN_Vector1,id:22,x:33019,y:32997,v1:1;n:type:ShaderForge.SFN_Vector1,id:23,x:33019,y:32934,v1:0;n:type:ShaderForge.SFN_Multiply,id:25,x:33627,y:32804|A-8-OUT,B-26-OUT;n:type:ShaderForge.SFN_Divide,id:26,x:33733,y:33001|A-28-OUT,B-27-OUT;n:type:ShaderForge.SFN_Pi,id:27,x:33913,y:33067;n:type:ShaderForge.SFN_Vector1,id:28,x:33913,y:33001,v1:180;n:type:ShaderForge.SFN_Multiply,id:31,x:32684,y:32907|A-53-A,B-17-OUT;n:type:ShaderForge.SFN_Divide,id:41,x:33174,y:32626|A-3-OUT,B-43-OUT;n:type:ShaderForge.SFN_Vector1,id:43,x:33638,y:32457,v1:2;n:type:ShaderForge.SFN_RemapRange,id:52,x:34140,y:32812,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5-U;n:type:ShaderForge.SFN_Tex2d,id:53,x:32816,y:32469,ptlb:MainTex,ptin:_MainTex,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:80,x:32998,y:32531,ptlb:Color,ptin:_Color,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:81,x:32734,y:32659|A-53-RGB,B-80-RGB;proporder:2-53-80;pass:END;sub:END;*/

Shader "Shader Forge/Percept" {
    Properties {
        _Angle ("Angle", Float ) = 90
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Angle;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float2 node_90 = i.uv0;
                float4 node_53 = tex2D(_MainTex,TRANSFORM_TEX(node_90.rg, _MainTex));
                float3 emissive = float3(node_53.a,node_53.a,node_53.a);
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (node_53.rgb*_Color.rgb);
                finalColor += emissive;
                float node_17_if_leA = step((acos((i.uv0.r*2.0+-1.0))*(180.0/3.141592654)),(min(_Angle,360.0)/2.0));
                float node_17_if_leB = step((min(_Angle,360.0)/2.0),(acos((i.uv0.r*2.0+-1.0))*(180.0/3.141592654)));
                float node_22 = 1.0;
/// Final Color:
                return fixed4(finalColor,(node_53.a*lerp((node_17_if_leA*node_22)+(node_17_if_leB*0.0),node_22,node_17_if_leA*node_17_if_leB)));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Angle;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float2 node_91 = i.uv0;
                float4 node_53 = tex2D(_MainTex,TRANSFORM_TEX(node_91.rg, _MainTex));
                finalColor += diffuseLight * (node_53.rgb*_Color.rgb);
                float node_17_if_leA = step((acos((i.uv0.r*2.0+-1.0))*(180.0/3.141592654)),(min(_Angle,360.0)/2.0));
                float node_17_if_leB = step((min(_Angle,360.0)/2.0),(acos((i.uv0.r*2.0+-1.0))*(180.0/3.141592654)));
                float node_22 = 1.0;
/// Final Color:
                return fixed4(finalColor * (node_53.a*lerp((node_17_if_leA*node_22)+(node_17_if_leB*0.0),node_22,node_17_if_leA*node_17_if_leB)),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
