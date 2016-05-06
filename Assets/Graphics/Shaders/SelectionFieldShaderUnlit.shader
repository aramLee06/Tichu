// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33088,y:32752,varname:node_3138,prsc:2|emission-4159-OUT,clip-7581-A;n:type:ShaderForge.SFN_Color,id:7241,x:32527,y:33280,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4344827,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7581,x:32592,y:32715,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19f442b1c188e444c9f66b7ab7d15f51,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2228,x:32527,y:32963,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5e3b0a0489283f0489a72ca29e99928b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8136,x:32842,y:33194,varname:node_8136,prsc:2|A-2228-RGB,B-1613-OUT;n:type:ShaderForge.SFN_Time,id:3367,x:32226,y:33374,varname:node_3367,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8565,x:32226,y:33160,varname:node_8565,prsc:2|A-5476-OUT,B-3367-T;n:type:ShaderForge.SFN_ValueProperty,id:5476,x:32226,y:33315,ptovrint:False,ptlb:GleamSpeed,ptin:_GleamSpeed,varname:node_5476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.5;n:type:ShaderForge.SFN_Tex2d,id:7004,x:32226,y:32832,ptovrint:False,ptlb:GleamTex,ptin:_GleamTex,varname:node_7004,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44a2cc0423acc1a478a72255770a325e,ntxv:0,isnm:False|UVIN-647-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3968,x:32011,y:33031,varname:node_3968,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:647,x:32226,y:33004,varname:node_647,prsc:2,spu:1,spv:0|UVIN-3968-UVOUT,DIST-8565-OUT;n:type:ShaderForge.SFN_Multiply,id:1613,x:32527,y:33120,varname:node_1613,prsc:2|A-7004-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Multiply,id:8986,x:32011,y:32580,varname:node_8986,prsc:2|A-9360-OUT,B-2355-T;n:type:ShaderForge.SFN_Time,id:2355,x:32011,y:32452,varname:node_2355,prsc:2;n:type:ShaderForge.SFN_Sin,id:9375,x:32011,y:32702,varname:node_9375,prsc:2|IN-8986-OUT;n:type:ShaderForge.SFN_RemapRange,id:2716,x:32011,y:32848,varname:node_2716,prsc:2,frmn:-1,frmx:1,tomn:0.2,tomx:1|IN-9375-OUT;n:type:ShaderForge.SFN_Multiply,id:9315,x:32826,y:33059,varname:node_9315,prsc:2|A-2716-OUT,B-8136-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9360,x:32011,y:32401,ptovrint:False,ptlb:PulseSpeed,ptin:_PulseSpeed,varname:node_9360,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:7437,x:32803,y:32696,varname:node_7437,prsc:2|A-8897-RGB,B-6319-OUT;n:type:ShaderForge.SFN_Color,id:8897,x:32803,y:32529,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_8897,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:4159,x:32824,y:32919,varname:node_4159,prsc:2|A-7437-OUT,B-9315-OUT;n:type:ShaderForge.SFN_Blend,id:6319,x:32401,y:32515,varname:node_6319,prsc:2,blmd:10,clmp:True|SRC-7581-RGB,DST-364-RGB;n:type:ShaderForge.SFN_Tex2d,id:364,x:32401,y:32728,ptovrint:False,ptlb:Wood,ptin:_Wood,varname:node_364,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4d5ba12171a65ae43b0c84171bc6189c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_RemapRange,id:1414,x:32271,y:31899,varname:node_1414,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1;proporder:7241-7581-2228-5476-7004-9360-8897-364;pass:END;sub:END;*/

Shader "Shader Forge/SelectionFieldShaderUnlitFIX" {
    Properties {
        _Color ("Color", Color) = (1,0.4344827,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _GleamSpeed ("GleamSpeed", Float ) = -0.5
        _GleamTex ("GleamTex", 2D) = "white" {}
        _PulseSpeed ("PulseSpeed", Float ) = 2
        _Tint ("Tint", Color) = (0.5,0.5,0.5,1)
        _Wood ("Wood", 2D) = "white" {}
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
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _GleamSpeed;
            uniform sampler2D _GleamTex; uniform float4 _GleamTex_ST;
            uniform float _PulseSpeed;
            uniform float4 _Tint;
            uniform sampler2D _Wood; uniform float4 _Wood_ST;
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
                float4 node_2355 = _Time + _TimeEditor;
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 node_3367 = _Time + _TimeEditor;
                float2 node_647 = (i.uv0+(_GleamSpeed*node_3367.g)*float2(1,0));
                float4 _GleamTex_var = tex2D(_GleamTex,TRANSFORM_TEX(node_647, _GleamTex));
                float3 node_8136 = (_Mask_var.rgb*(_GleamTex_var.rgb*_Color.rgb));
                float3 node_9315 = ((sin((_PulseSpeed*node_2355.g))*0.4+0.6)*node_8136);
                float3 emissive = ((_Tint.rgb*saturate(( _Wood_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_Wood_var.rgb-0.5))*(1.0-_Tex_var.rgb)) : (2.0*_Wood_var.rgb*_Tex_var.rgb) )))+node_9315);
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
