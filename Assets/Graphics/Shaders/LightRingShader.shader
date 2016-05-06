// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33155,y:32826,varname:node_3138,prsc:2|diff-7581-RGB,emission-5872-OUT,clip-7581-A;n:type:ShaderForge.SFN_Color,id:7241,x:32417,y:33311,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4344827,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7581,x:32653,y:32655,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:47989ea1e9cd67647abdc6324099aeaa,ntxv:0,isnm:False|UVIN-1883-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2228,x:32653,y:32836,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e0d5e31f53e7c9e4ba68ab2b8f43c7df,ntxv:0,isnm:False|UVIN-1883-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8136,x:32683,y:33304,varname:node_8136,prsc:2|A-2228-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Multiply,id:1872,x:32916,y:33225,varname:node_1872,prsc:2|A-6058-OUT,B-8136-OUT;n:type:ShaderForge.SFN_TexCoord,id:9499,x:32195,y:32836,varname:node_9499,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:8360,x:32872,y:32594,varname:node_8360,prsc:2|A-921-RGB,B-7581-RGB;n:type:ShaderForge.SFN_Color,id:921,x:32872,y:32441,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_921,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:5872,x:33148,y:32632,varname:node_5872,prsc:2|A-8360-OUT,B-1872-OUT;n:type:ShaderForge.SFN_RemapRange,id:1784,x:31123,y:33517,varname:node_1784,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1;n:type:ShaderForge.SFN_ValueProperty,id:6058,x:32420,y:33068,ptovrint:False,ptlb:GlowValue,ptin:_GlowValue,varname:node_6058,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Rotator,id:1883,x:32410,y:32884,varname:node_1883,prsc:2|UVIN-9499-UVOUT,ANG-3150-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3150,x:32128,y:33045,ptovrint:False,ptlb:Angle,ptin:_Angle,varname:node_3150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:7241-7581-2228-921-6058-3150;pass:END;sub:END;*/

Shader "Shader Forge/LightRingShader" {
    Properties {
        _Color ("Color", Color) = (1,0.4344827,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Tint ("Tint", Color) = (0.5,0.5,0.5,1)
        _GlowValue ("GlowValue", Float ) = 1
        _Angle ("Angle", Float ) = 0
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _Tint;
            uniform float _GlowValue;
            uniform float _Angle;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_1883_ang = _Angle;
                float node_1883_spd = 1.0;
                float node_1883_cos = cos(node_1883_spd*node_1883_ang);
                float node_1883_sin = sin(node_1883_spd*node_1883_ang);
                float2 node_1883_piv = float2(0.5,0.5);
                float2 node_1883 = (mul(i.uv0-node_1883_piv,float2x2( node_1883_cos, -node_1883_sin, node_1883_sin, node_1883_cos))+node_1883_piv);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_1883, _Tex));
                clip(_Tex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_1883, _Mask));
                float3 emissive = ((_Tint.rgb*_Tex_var.rgb)+(_GlowValue*(_Mask_var.rgb*_Color.rgb)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform float _Angle;
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
                float node_1883_ang = _Angle;
                float node_1883_spd = 1.0;
                float node_1883_cos = cos(node_1883_spd*node_1883_ang);
                float node_1883_sin = sin(node_1883_spd*node_1883_ang);
                float2 node_1883_piv = float2(0.5,0.5);
                float2 node_1883 = (mul(i.uv0-node_1883_piv,float2x2( node_1883_cos, -node_1883_sin, node_1883_sin, node_1883_cos))+node_1883_piv);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_1883, _Tex));
                clip(_Tex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
