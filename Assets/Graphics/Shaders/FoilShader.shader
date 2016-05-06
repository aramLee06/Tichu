// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33010,y:32813,varname:node_3138,prsc:2|emission-1310-OUT,clip-1328-A;n:type:ShaderForge.SFN_Color,id:7241,x:32396,y:33270,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.0186527,c2:0.8113701,c3:0.8455882,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2593,x:32362,y:32929,ptovrint:False,ptlb:Foil,ptin:_Foil,varname:node_2593,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd19f65007c65cb45a7cd24aa31887a9,ntxv:0,isnm:False|UVIN-4470-OUT;n:type:ShaderForge.SFN_Tex2d,id:1328,x:32396,y:32748,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_1328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:35f8395bdd7108542aa1130d7626a996,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7822,x:32139,y:33088,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_7822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9b225532e6df4654fa671399dccd0b89,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5843,x:32623,y:33225,varname:node_5843,prsc:2|A-2593-RGB,B-866-OUT;n:type:ShaderForge.SFN_TexCoord,id:6411,x:31884,y:32877,varname:node_6411,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:1254,x:32139,y:32877,varname:node_1254,prsc:2,spu:0,spv:0.2|UVIN-6411-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7448,x:32665,y:32803,varname:node_7448,prsc:2|A-5400-OUT,B-4709-OUT;n:type:ShaderForge.SFN_RemapRange,id:866,x:32331,y:33088,varname:node_866,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-7822-RGB;n:type:ShaderForge.SFN_Multiply,id:4709,x:32665,y:33056,varname:node_4709,prsc:2|A-7241-RGB,B-5843-OUT;n:type:ShaderForge.SFN_Blend,id:2818,x:32665,y:32625,varname:node_2818,prsc:2,blmd:17,clmp:True|SRC-7448-OUT,DST-1328-RGB;n:type:ShaderForge.SFN_Tex2d,id:2226,x:31746,y:32410,ptovrint:False,ptlb:Flow,ptin:_Flow,varname:node_2226,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1cdd308733f3e8e489e94365626af373,ntxv:0,isnm:False|UVIN-6707-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8174,x:31911,y:32427,varname:node_8174,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2226-RGB;n:type:ShaderForge.SFN_RemapRange,id:5058,x:32100,y:32455,varname:node_5058,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-8174-OUT;n:type:ShaderForge.SFN_Multiply,id:5021,x:32296,y:32509,varname:node_5021,prsc:2|A-1487-OUT,B-5058-OUT;n:type:ShaderForge.SFN_Time,id:3813,x:31370,y:32715,varname:node_3813,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9472,x:31555,y:32671,varname:node_9472,prsc:2|A-308-OUT,B-3813-T;n:type:ShaderForge.SFN_Sin,id:6795,x:31746,y:32671,varname:node_6795,prsc:2|IN-9472-OUT;n:type:ShaderForge.SFN_RemapRange,id:103,x:31941,y:32671,varname:node_103,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-6795-OUT;n:type:ShaderForge.SFN_Multiply,id:3188,x:32199,y:32679,varname:node_3188,prsc:2|A-5021-OUT,B-103-OUT;n:type:ShaderForge.SFN_Multiply,id:365,x:32556,y:32510,varname:node_365,prsc:2|A-5127-OUT,B-3188-OUT;n:type:ShaderForge.SFN_Vector1,id:5127,x:32556,y:32451,varname:node_5127,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:4470,x:32727,y:32451,varname:node_4470,prsc:2|A-365-OUT,B-1254-UVOUT;n:type:ShaderForge.SFN_Lerp,id:5400,x:31890,y:33185,varname:node_5400,prsc:2|A-7785-OUT,B-3099-OUT,T-103-OUT;n:type:ShaderForge.SFN_Multiply,id:153,x:32996,y:32661,varname:node_153,prsc:2|A-4694-OUT,B-2818-OUT;n:type:ShaderForge.SFN_Multiply,id:3397,x:31610,y:32877,varname:node_3397,prsc:2|A-3518-OUT,B-6411-U;n:type:ShaderForge.SFN_Append,id:6707,x:31527,y:32460,varname:node_6707,prsc:2|A-3397-OUT,B-679-OUT;n:type:ShaderForge.SFN_Multiply,id:679,x:31610,y:33022,varname:node_679,prsc:2|A-1476-OUT,B-6411-V;n:type:ShaderForge.SFN_ValueProperty,id:3518,x:31408,y:32877,ptovrint:False,ptlb:UScale,ptin:_UScale,varname:node_3518,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:1476,x:31408,y:33003,ptovrint:False,ptlb:VScale,ptin:_VScale,varname:_node_3518_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:3099,x:31890,y:33121,ptovrint:False,ptlb:MaxGlow,ptin:_MaxGlow,varname:_node_3518_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_ValueProperty,id:7785,x:31890,y:33046,ptovrint:False,ptlb:MinGlow,ptin:_MinGlow,varname:_node_3518_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:308,x:31555,y:32609,ptovrint:False,ptlb:MorphSpeed,ptin:_MorphSpeed,varname:_node_3518_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.5;n:type:ShaderForge.SFN_ValueProperty,id:4694,x:32996,y:32587,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_4694,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1487,x:32296,y:32442,ptovrint:False,ptlb:FlowStrength,ptin:_FlowStrength,varname:_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:1310,x:33280,y:32690,ptovrint:False,ptlb:Selected,ptin:_Selected,varname:node_1310,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1328-RGB,B-153-OUT;proporder:7241-1328-2593-7822-2226-3518-1476-3099-7785-308-4694-1487-1310;pass:END;sub:END;*/

Shader "Shader Forge/FoilShader" {
    Properties {
        _Color ("Color", Color) = (0.0186527,0.8113701,0.8455882,1)
        [PerRendererData] _MainTex ("MainTex", 2D) = "white" {}
        _Foil ("Foil", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Flow ("Flow", 2D) = "white" {}
        _UScale ("UScale", Float ) = 3
        _VScale ("VScale", Float ) = 1
        _MaxGlow ("MaxGlow", Float ) = 0.8
        _MinGlow ("MinGlow", Float ) = 0.2
        _MorphSpeed ("MorphSpeed", Float ) = 2.5
        _Intensity ("Intensity", Float ) = 1
        _FlowStrength ("FlowStrength", Float ) = 1
        [MaterialToggle] _Selected ("Selected", Float ) = 0
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
            uniform sampler2D _Foil; uniform float4 _Foil_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _Flow; uniform float4 _Flow_ST;
            uniform float _UScale;
            uniform float _VScale;
            uniform float _MaxGlow;
            uniform float _MinGlow;
            uniform float _MorphSpeed;
            uniform float _Intensity;
            uniform float _FlowStrength;
            uniform fixed _Selected;
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(_MainTex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float4 node_3813 = _Time + _TimeEditor;
                float node_103 = (sin((_MorphSpeed*node_3813.g))*0.5+0.5);
                float2 node_6707 = float2((_UScale*i.uv0.r),(_VScale*i.uv0.g));
                float4 _Flow_var = tex2D(_Flow,TRANSFORM_TEX(node_6707, _Flow));
                float4 node_2314 = _Time + _TimeEditor;
                float2 node_4470 = ((1.0*((_FlowStrength*(_Flow_var.rgb.rg*1.0+-0.5))*node_103))+(i.uv0+node_2314.g*float2(0,0.2)));
                float4 _Foil_var = tex2D(_Foil,TRANSFORM_TEX(node_4470, _Foil));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 emissive = lerp( _MainTex_var.rgb, (_Intensity*saturate(abs((lerp(_MinGlow,_MaxGlow,node_103)*(_Color.rgb*(_Foil_var.rgb*(_Mask_var.rgb*1.0+0.0))))-_MainTex_var.rgb))), _Selected );
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(_MainTex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "CardFallback"
    CustomEditor "ShaderForgeMaterialInspector"
}
