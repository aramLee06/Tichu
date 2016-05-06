// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32974,y:32827,varname:node_3138,prsc:2|diff-7581-RGB,emission-8290-OUT,clip-7581-A;n:type:ShaderForge.SFN_Color,id:7241,x:32205,y:33222,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4344827,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7581,x:32450,y:32522,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a9265bed1b87fec48bb28ad263624d7b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2228,x:32313,y:32878,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8790138bf4dec3f4fb28c133e0302b80,ntxv:0,isnm:False|UVIN-9315-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8136,x:32422,y:33158,varname:node_8136,prsc:2|A-2228-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Multiply,id:1872,x:32683,y:33306,varname:node_1872,prsc:2|A-8136-OUT,B-9724-OUT;n:type:ShaderForge.SFN_Time,id:3367,x:32205,y:33451,varname:node_3367,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8565,x:32420,y:33451,varname:node_8565,prsc:2|A-5476-OUT,B-3367-T;n:type:ShaderForge.SFN_ValueProperty,id:5476,x:32205,y:33389,ptovrint:False,ptlb:PulseSpeed,ptin:_PulseSpeed,varname:node_5476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Sin,id:490,x:32420,y:33587,varname:node_490,prsc:2|IN-8565-OUT;n:type:ShaderForge.SFN_RemapRange,id:9724,x:32602,y:33587,varname:node_9724,prsc:2,frmn:-1,frmx:1,tomn:0.3,tomx:0.8|IN-490-OUT;n:type:ShaderForge.SFN_TexCoord,id:9315,x:31551,y:33016,varname:node_9315,prsc:2,uv:0;n:type:ShaderForge.SFN_Rotator,id:845,x:31796,y:32947,varname:node_845,prsc:2|UVIN-9315-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3386,x:31484,y:32560,ptovrint:False,ptlb:Flow,ptin:_Flow,varname:node_3386,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d1f0a4af40d797c438059e67b3446c92,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:8909,x:31687,y:32560,varname:node_8909,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3386-RGB;n:type:ShaderForge.SFN_RemapRange,id:2738,x:31853,y:32552,varname:node_2738,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-8909-OUT;n:type:ShaderForge.SFN_Time,id:3982,x:31321,y:32867,varname:node_3982,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6895,x:31551,y:32867,varname:node_6895,prsc:2|A-5173-OUT,B-3982-T;n:type:ShaderForge.SFN_Vector1,id:5173,x:31551,y:32796,varname:node_5173,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Multiply,id:9704,x:32008,y:32785,varname:node_9704,prsc:2|A-3370-OUT,B-7033-OUT;n:type:ShaderForge.SFN_Multiply,id:7883,x:32193,y:32691,varname:node_7883,prsc:2|A-9519-OUT,B-9704-OUT;n:type:ShaderForge.SFN_Multiply,id:3370,x:32029,y:32509,varname:node_3370,prsc:2|A-5141-OUT,B-2738-OUT;n:type:ShaderForge.SFN_Vector1,id:5141,x:31687,y:32434,varname:node_5141,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:9519,x:32181,y:32477,varname:node_9519,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:2429,x:32347,y:32720,varname:node_2429,prsc:2|A-7883-OUT,B-845-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1538,x:31971,y:33222,ptovrint:False,ptlb:GleamTex,ptin:_GleamTex,varname:node_1538,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:41372ff1cca74074794341b95f671cfc,ntxv:0,isnm:False|UVIN-2429-OUT;n:type:ShaderForge.SFN_Multiply,id:638,x:32403,y:33323,varname:node_638,prsc:2|A-5554-OUT,B-6171-OUT;n:type:ShaderForge.SFN_Multiply,id:547,x:32052,y:33453,varname:node_547,prsc:2|A-7241-RGB,B-1538-RGB;n:type:ShaderForge.SFN_Sin,id:5864,x:31381,y:33016,varname:node_5864,prsc:2|IN-6895-OUT;n:type:ShaderForge.SFN_RemapRange,id:7033,x:31381,y:33157,varname:node_7033,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-5864-OUT;n:type:ShaderForge.SFN_RemapRange,id:5554,x:32602,y:33757,varname:node_5554,prsc:2,frmn:-1,frmx:1,tomn:0.5,tomx:0.3|IN-490-OUT;n:type:ShaderForge.SFN_Blend,id:3114,x:32667,y:33071,varname:node_3114,prsc:2,blmd:15,clmp:True|SRC-638-OUT,DST-1872-OUT;n:type:ShaderForge.SFN_Add,id:8290,x:32667,y:32928,varname:node_8290,prsc:2|A-6065-OUT,B-3114-OUT;n:type:ShaderForge.SFN_Multiply,id:6065,x:32882,y:32653,varname:node_6065,prsc:2|A-7141-RGB,B-7581-RGB;n:type:ShaderForge.SFN_Color,id:7141,x:32882,y:32513,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_7141,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6171,x:32205,y:33644,varname:node_6171,prsc:2|A-3671-OUT,B-547-OUT;n:type:ShaderForge.SFN_Slider,id:3671,x:32048,y:33795,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_3671,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:7241-7581-2228-5476-3386-1538-7141-3671;pass:END;sub:END;*/

Shader "Shader Forge/CardBackShaderUnlit" {
    Properties {
        _Color ("Color", Color) = (1,0.4344827,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _PulseSpeed ("PulseSpeed", Float ) = 3
        _Flow ("Flow", 2D) = "white" {}
        _GleamTex ("GleamTex", 2D) = "white" {}
        _Tint ("Tint", Color) = (0.5,0.5,0.5,1)
        _Glow ("Glow", Range(0, 1)) = 1
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
            uniform float _PulseSpeed;
            uniform sampler2D _Flow; uniform float4 _Flow_ST;
            uniform sampler2D _GleamTex; uniform float4 _GleamTex_ST;
            uniform float4 _Tint;
            uniform float _Glow;
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
                float4 node_3367 = _Time + _TimeEditor;
                float node_490 = sin((_PulseSpeed*node_3367.g));
                float4 _Flow_var = tex2D(_Flow,TRANSFORM_TEX(i.uv0, _Flow));
                float4 node_3982 = _Time + _TimeEditor;
                float4 node_9771 = _Time + _TimeEditor;
                float node_845_ang = node_9771.g;
                float node_845_spd = 1.0;
                float node_845_cos = cos(node_845_spd*node_845_ang);
                float node_845_sin = sin(node_845_spd*node_845_ang);
                float2 node_845_piv = float2(0.5,0.5);
                float2 node_845 = (mul(i.uv0-node_845_piv,float2x2( node_845_cos, -node_845_sin, node_845_sin, node_845_cos))+node_845_piv);
                float2 node_2429 = ((1.0*((1.0*(_Flow_var.rgb.rg*1.0+-0.5))*(sin((0.3*node_3982.g))*0.5+0.5)))+node_845);
                float4 _GleamTex_var = tex2D(_GleamTex,TRANSFORM_TEX(node_2429, _GleamTex));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 emissive = ((_Tint.rgb*_Tex_var.rgb)+saturate(( ((node_490*-0.09999999+0.4)*(_Glow*(_Color.rgb*_GleamTex_var.rgb))) > 0.5 ? max(((_Mask_var.rgb*_Color.rgb)*(node_490*0.25+0.55)),2.0*(((node_490*-0.09999999+0.4)*(_Glow*(_Color.rgb*_GleamTex_var.rgb)))-0.5)) : min(((_Mask_var.rgb*_Color.rgb)*(node_490*0.25+0.55)),2.0*((node_490*-0.09999999+0.4)*(_Glow*(_Color.rgb*_GleamTex_var.rgb)))) )));
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
