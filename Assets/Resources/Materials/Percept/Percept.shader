// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32228,y:32536|emission-323-RGB,alpha-31-OUT;n:type:ShaderForge.SFN_TexCoord,id:5,x:33934,y:32848,uv:0;n:type:ShaderForge.SFN_If,id:17,x:32812,y:33007|A-105-OUT,B-288-OUT,GT-117-OUT,EQ-107-OUT,LT-292-OUT;n:type:ShaderForge.SFN_Multiply,id:31,x:32479,y:32759|A-323-A,B-119-OUT;n:type:ShaderForge.SFN_RemapRange,id:52,x:33748,y:32848,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5-UVOUT;n:type:ShaderForge.SFN_Length,id:105,x:33172,y:32992|IN-52-OUT;n:type:ShaderForge.SFN_Vector1,id:107,x:33016,y:33132,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:109,x:33120,y:32563,ptlb:angle,ptin:_angle,glob:False,v1:90;n:type:ShaderForge.SFN_Vector1,id:113,x:33339,y:32833,v1:180;n:type:ShaderForge.SFN_Pi,id:114,x:33339,y:32886;n:type:ShaderForge.SFN_Divide,id:115,x:33151,y:32833|A-113-OUT,B-114-OUT;n:type:ShaderForge.SFN_Multiply,id:116,x:32949,y:32733|A-178-OUT,B-115-OUT;n:type:ShaderForge.SFN_Vector1,id:117,x:33016,y:33078,v1:0;n:type:ShaderForge.SFN_If,id:119,x:32649,y:32782|A-116-OUT,B-224-OUT,GT-281-OUT,EQ-17-OUT,LT-17-OUT;n:type:ShaderForge.SFN_Vector2,id:176,x:33537,y:32620,v1:0,v2:1;n:type:ShaderForge.SFN_ArcCos,id:178,x:33151,y:32709|IN-206-OUT;n:type:ShaderForge.SFN_Dot,id:206,x:33339,y:32688,dt:0|A-176-OUT,B-264-OUT;n:type:ShaderForge.SFN_Divide,id:224,x:32926,y:32563|A-109-OUT,B-347-OUT;n:type:ShaderForge.SFN_Normalize,id:264,x:33526,y:32709|IN-52-OUT;n:type:ShaderForge.SFN_Vector1,id:281,x:32832,y:32816,v1:0;n:type:ShaderForge.SFN_Vector1,id:288,x:33016,y:33026,v1:1;n:type:ShaderForge.SFN_Vector1,id:292,x:33016,y:33179,v1:1;n:type:ShaderForge.SFN_Color,id:323,x:32731,y:32532,ptlb:Color,ptin:_Color,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:347,x:33113,y:32639,v1:2;proporder:109-323;pass:END;sub:END;*/

Shader "Shader Forge/Percept" {
    Properties {
        _angle ("angle", Float ) = 90
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
            uniform float _angle;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                float2 node_52 = (i.uv0.rg*2.0+-1.0);
                float node_119_if_leA = step((acos(dot(float2(0,1),normalize(node_52)))*(180.0/3.141592654)),(_angle/2.0));
                float node_119_if_leB = step((_angle/2.0),(acos(dot(float2(0,1),normalize(node_52)))*(180.0/3.141592654)));
                float node_17_if_leA = step(length(node_52),1.0);
                float node_17_if_leB = step(1.0,length(node_52));
                float node_17 = lerp((node_17_if_leA*1.0)+(node_17_if_leB*0.0),1.0,node_17_if_leA*node_17_if_leB);
/// Final Color:
                return fixed4(finalColor,(_Color.a*lerp((node_119_if_leA*node_17)+(node_119_if_leB*0.0),node_17,node_119_if_leA*node_119_if_leB)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
