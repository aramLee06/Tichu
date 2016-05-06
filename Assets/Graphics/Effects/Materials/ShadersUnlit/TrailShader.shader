// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33045,y:32730,varname:node_3138,prsc:2|emission-5180-OUT,clip-3305-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32516,y:33135,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.6413794,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2564,x:32516,y:32930,ptovrint:False,ptlb:TrailTex,ptin:_TrailTex,varname:node_2564,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b3842af0226ca5741a217c190ddef4d9,ntxv:0,isnm:False|UVIN-814-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9856,x:32831,y:33110,varname:node_9856,prsc:2|A-3383-OUT,B-7241-RGB;n:type:ShaderForge.SFN_Multiply,id:9169,x:32831,y:32970,varname:node_9169,prsc:2|A-5630-OUT,B-9856-OUT;n:type:ShaderForge.SFN_Slider,id:3221,x:32674,y:32755,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_3221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Time,id:268,x:32288,y:32392,varname:node_268,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4769,x:32516,y:32453,varname:node_4769,prsc:2|A-3415-OUT,B-268-T;n:type:ShaderForge.SFN_Vector1,id:3415,x:32516,y:32375,varname:node_3415,prsc:2,v1:5;n:type:ShaderForge.SFN_Sin,id:1589,x:32516,y:32580,varname:node_1589,prsc:2|IN-4769-OUT;n:type:ShaderForge.SFN_RemapRange,id:5630,x:32516,y:32732,varname:node_5630,prsc:2,frmn:-1,frmx:1,tomn:0.8,tomx:1|IN-1589-OUT;n:type:ShaderForge.SFN_Multiply,id:5180,x:32831,y:32831,varname:node_5180,prsc:2|A-3221-OUT,B-9169-OUT;n:type:ShaderForge.SFN_TexCoord,id:8615,x:32313,y:32604,varname:node_8615,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:814,x:32313,y:32782,varname:node_814,prsc:2,spu:0.2,spv:0.7|UVIN-8615-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:732,x:32311,y:33128,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_732,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ec7897b664db38b42849d79ca60f642a,ntxv:0,isnm:False|UVIN-6184-UVOUT;n:type:ShaderForge.SFN_Panner,id:6184,x:32311,y:32937,varname:node_6184,prsc:2,spu:0.5,spv:0.5|UVIN-8615-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:8044,x:32311,y:33303,varname:node_8044,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-732-R;n:type:ShaderForge.SFN_Blend,id:3383,x:32526,y:33296,varname:node_3383,prsc:2,blmd:8,clmp:True|SRC-7241-RGB,DST-8044-OUT;n:type:ShaderForge.SFN_RemapRange,id:52,x:32074,y:32841,varname:node_52,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8615-UVOUT;n:type:ShaderForge.SFN_Length,id:9636,x:32093,y:32649,varname:node_9636,prsc:2|IN-52-OUT;n:type:ShaderForge.SFN_OneMinus,id:5017,x:32167,y:32505,varname:node_5017,prsc:2|IN-9636-OUT;n:type:ShaderForge.SFN_Multiply,id:6090,x:32808,y:32403,varname:node_6090,prsc:2|A-6103-OUT,B-5017-OUT;n:type:ShaderForge.SFN_Vector1,id:9869,x:32808,y:32336,varname:node_9869,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Blend,id:3305,x:33113,y:32506,varname:node_3305,prsc:2,blmd:0,clmp:True|SRC-6090-OUT,DST-2564-A;n:type:ShaderForge.SFN_RemapRange,id:6103,x:32532,y:32194,varname:node_6103,prsc:2,frmn:-1,frmx:1,tomn:1,tomx:2|IN-1589-OUT;proporder:7241-2564-3221-732;pass:END;sub:END;*/

Shader "Shader Forge/TrailShader" {
    Properties {
        _Color ("Color", Color) = (1,0.6413794,0,1)
        _TrailTex ("TrailTex", 2D) = "white" {}
        _Glow ("Glow", Range(0, 1)) = 1
        _Mask ("Mask", 2D) = "white" {}
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
            uniform sampler2D _TrailTex; uniform float4 _TrailTex_ST;
            uniform float _Glow;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
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
                float4 node_268 = _Time + _TimeEditor;
                float node_1589 = sin((5.0*node_268.g));
                float node_6103 = (node_1589*0.5+1.5);
                float4 node_2088 = _Time + _TimeEditor;
                float2 node_814 = (i.uv0+node_2088.g*float2(0.2,0.7));
                float4 _TrailTex_var = tex2D(_TrailTex,TRANSFORM_TEX(node_814, _TrailTex));
                clip(saturate(min((node_6103*(1.0 - length((i.uv0*2.0+-1.0)))),_TrailTex_var.a)) - 0.5);
////// Lighting:
////// Emissive:
                float2 node_6184 = (i.uv0+node_2088.g*float2(0.5,0.5));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_6184, _Mask));
                float3 emissive = (_Glow*((node_1589*0.09999999+0.9)*(saturate((_Color.rgb+_Mask_var.r.r))*_Color.rgb)));
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
            uniform float4 _TimeEditor;
            uniform sampler2D _TrailTex; uniform float4 _TrailTex_ST;
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
                float4 node_268 = _Time + _TimeEditor;
                float node_1589 = sin((5.0*node_268.g));
                float node_6103 = (node_1589*0.5+1.5);
                float4 node_9011 = _Time + _TimeEditor;
                float2 node_814 = (i.uv0+node_9011.g*float2(0.2,0.7));
                float4 _TrailTex_var = tex2D(_TrailTex,TRANSFORM_TEX(node_814, _TrailTex));
                clip(saturate(min((node_6103*(1.0 - length((i.uv0*2.0+-1.0)))),_TrailTex_var.a)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
