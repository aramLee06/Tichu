// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33323,y:32881,varname:node_3138,prsc:2|emission-5872-OUT,clip-5923-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32357,y:33343,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.213793,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7581,x:32653,y:32655,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dba8b785bb70f7a4eb27ae53644128bc,ntxv:0,isnm:False|UVIN-4106-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2228,x:32653,y:32836,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd13195be32e512458bddf1a5911e72b,ntxv:0,isnm:False|UVIN-4106-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8136,x:32549,y:33249,varname:node_8136,prsc:2|A-2228-RGB,B-7241-RGB;n:type:ShaderForge.SFN_TexCoord,id:9499,x:32132,y:32836,varname:node_9499,prsc:2,uv:0;n:type:ShaderForge.SFN_Length,id:5173,x:32403,y:32546,varname:node_5173,prsc:2|IN-7347-OUT;n:type:ShaderForge.SFN_RemapRange,id:7347,x:32403,y:32679,varname:node_7347,prsc:2,frmn:1,frmx:0,tomn:-1,tomx:1|IN-9499-UVOUT;n:type:ShaderForge.SFN_Posterize,id:612,x:32653,y:32347,varname:node_612,prsc:2|IN-295-OUT,STPS-4741-OUT;n:type:ShaderForge.SFN_Vector1,id:4741,x:32653,y:32284,varname:node_4741,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:5923,x:32653,y:32492,varname:node_5923,prsc:2|A-7581-A,B-612-OUT;n:type:ShaderForge.SFN_OneMinus,id:295,x:32403,y:32395,varname:node_295,prsc:2|IN-5173-OUT;n:type:ShaderForge.SFN_Multiply,id:7431,x:32846,y:33223,varname:node_7431,prsc:2|A-3372-OUT,B-8136-OUT;n:type:ShaderForge.SFN_Multiply,id:8360,x:32963,y:32685,varname:node_8360,prsc:2|A-921-RGB,B-6145-OUT;n:type:ShaderForge.SFN_Color,id:921,x:32963,y:32518,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_921,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5808823,c2:0.1879325,c3:0.1879325,c4:1;n:type:ShaderForge.SFN_Add,id:5872,x:33240,y:32686,varname:node_5872,prsc:2|A-8360-OUT,B-7431-OUT;n:type:ShaderForge.SFN_Tex2d,id:6079,x:32015,y:32652,ptovrint:False,ptlb:Wood,ptin:_Wood,varname:node_6079,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cf6ff65d782354f44b518ec27ab19827,ntxv:0,isnm:False|UVIN-4106-UVOUT;n:type:ShaderForge.SFN_Blend,id:6145,x:32963,y:32834,varname:node_6145,prsc:2,blmd:10,clmp:True|SRC-7581-RGB,DST-6079-RGB;n:type:ShaderForge.SFN_ValueProperty,id:3372,x:32846,y:33162,ptovrint:False,ptlb:GlowValue,ptin:_GlowValue,varname:node_3372,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Rotator,id:4106,x:32366,y:32890,varname:node_4106,prsc:2|UVIN-9499-UVOUT,ANG-8291-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8291,x:32147,y:33070,ptovrint:False,ptlb:Angle,ptin:_Angle,varname:node_8291,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-7581-2228-921-6079-3372-8291;pass:END;sub:END;*/

Shader "Shader Forge/ButtonRotatorUnlit" {
    Properties {
        _Color ("Color", Color) = (0,1,0.213793,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Tint ("Tint", Color) = (0.5808823,0.1879325,0.1879325,1)
        _Wood ("Wood", 2D) = "white" {}
        _GlowValue ("GlowValue", Float ) = 1
        _Angle ("Angle", Float ) = 1
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
                float node_4106_ang = _Angle;
                float node_4106_spd = 1.0;
                float node_4106_cos = cos(node_4106_spd*node_4106_ang);
                float node_4106_sin = sin(node_4106_spd*node_4106_ang);
                float2 node_4106_piv = float2(0.5,0.5);
                float2 node_4106 = (mul(i.uv0-node_4106_piv,float2x2( node_4106_cos, -node_4106_sin, node_4106_sin, node_4106_cos))+node_4106_piv);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_4106, _Tex));
                float node_4741 = 2.0;
                clip((_Tex_var.a+floor((1.0 - length((i.uv0*-2.0+1.0))) * node_4741) / (node_4741 - 1)) - 0.5);
////// Lighting:
////// Emissive:
                float4 _Wood_var = tex2D(_Wood,TRANSFORM_TEX(node_4106, _Wood));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_4106, _Mask));
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
                float node_4106_ang = _Angle;
                float node_4106_spd = 1.0;
                float node_4106_cos = cos(node_4106_spd*node_4106_ang);
                float node_4106_sin = sin(node_4106_spd*node_4106_ang);
                float2 node_4106_piv = float2(0.5,0.5);
                float2 node_4106 = (mul(i.uv0-node_4106_piv,float2x2( node_4106_cos, -node_4106_sin, node_4106_sin, node_4106_cos))+node_4106_piv);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_4106, _Tex));
                float node_4741 = 2.0;
                clip((_Tex_var.a+floor((1.0 - length((i.uv0*-2.0+1.0))) * node_4741) / (node_4741 - 1)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
