// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32974,y:32827,varname:node_3138,prsc:2|diff-7581-RGB,emission-377-OUT,clip-7581-A;n:type:ShaderForge.SFN_Color,id:7241,x:32491,y:33255,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.3931033,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7581,x:32150,y:32638,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3a44179055c8726438fb7727b7f139d9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2228,x:32150,y:32833,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:429dc2cfa9dd25d4ab58cfbf73261a75,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8136,x:32491,y:33096,varname:node_8136,prsc:2|A-2228-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Multiply,id:2620,x:32768,y:33067,varname:node_2620,prsc:2|A-3785-OUT,B-8136-OUT;n:type:ShaderForge.SFN_Add,id:377,x:32768,y:32926,varname:node_377,prsc:2|A-4068-OUT,B-2620-OUT;n:type:ShaderForge.SFN_Color,id:5330,x:32549,y:32550,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_5330,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4068,x:32560,y:32696,varname:node_4068,prsc:2|A-5330-RGB,B-4570-OUT;n:type:ShaderForge.SFN_Tex2d,id:6862,x:32150,y:33025,ptovrint:False,ptlb:Wood,ptin:_Wood,varname:node_6862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cf6ff65d782354f44b518ec27ab19827,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:4570,x:32150,y:33196,varname:node_4570,prsc:2,blmd:10,clmp:True|SRC-7581-RGB,DST-6862-RGB;n:type:ShaderForge.SFN_ValueProperty,id:3785,x:32768,y:33222,ptovrint:False,ptlb:GlowValue,ptin:_GlowValue,varname:node_3785,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-7581-2228-5330-6862-3785;pass:END;sub:END;*/

Shader "Shader Forge/ButtonShaderUnlit" {
    Properties {
        _Color ("Color", Color) = (1,0.3931033,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Tint ("Tint", Color) = (0.5,0.5,0.5,1)
        _Wood ("Wood", 2D) = "white" {}
        _GlowValue ("GlowValue", Float ) = 1
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
            uniform sampler2D _Wood; uniform float4 _Wood_ST;
            uniform float _GlowValue;
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
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(i.uv0, _Tex));
                clip(_Tex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float4 _Wood_var = tex2D(_Wood,TRANSFORM_TEX(i.uv0, _Wood));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 emissive = ((_Tint.rgb*saturate(( _Wood_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_Wood_var.rgb-0.5))*(1.0-_Tex_var.rgb)) : (2.0*_Wood_var.rgb*_Tex_var.rgb) )))+(_GlowValue*(_Mask_var.rgb*_Color.rgb)));
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
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(i.uv0, _Tex));
                clip(_Tex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
