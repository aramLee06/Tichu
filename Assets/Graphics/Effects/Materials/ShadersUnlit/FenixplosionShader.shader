// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32875,y:32676,varname:node_3138,prsc:2|emission-9898-OUT,clip-2515-A;n:type:ShaderForge.SFN_Color,id:7241,x:32440,y:32469,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.8482759,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2515,x:32537,y:32722,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_2515,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:abec2e1b976a6de4eaf03db16db6d767,ntxv:0,isnm:False|UVIN-1575-OUT;n:type:ShaderForge.SFN_Multiply,id:9898,x:32905,y:32443,varname:node_9898,prsc:2|A-579-OUT,B-7037-OUT;n:type:ShaderForge.SFN_Slider,id:579,x:32748,y:32371,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_579,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:2;n:type:ShaderForge.SFN_Tex2d,id:3264,x:31664,y:32263,ptovrint:False,ptlb:Flow,ptin:_Flow,varname:node_3264,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3054cf642642fad4185bf57d165bf239,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:7102,x:31878,y:32313,varname:node_7102,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3264-RGB;n:type:ShaderForge.SFN_RemapRange,id:7046,x:32110,y:32390,varname:node_7046,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-7102-OUT;n:type:ShaderForge.SFN_Time,id:8370,x:31688,y:32531,varname:node_8370,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1563,x:31878,y:32531,varname:node_1563,prsc:2|A-1330-OUT,B-8370-T;n:type:ShaderForge.SFN_Sin,id:5444,x:31688,y:32674,varname:node_5444,prsc:2|IN-1563-OUT;n:type:ShaderForge.SFN_RemapRange,id:5208,x:31878,y:32674,varname:node_5208,prsc:2,frmn:-1,frmx:1,tomn:-1,tomx:1|IN-5444-OUT;n:type:ShaderForge.SFN_Multiply,id:6238,x:32110,y:32653,varname:node_6238,prsc:2|A-7046-OUT,B-5208-OUT;n:type:ShaderForge.SFN_Multiply,id:5770,x:32110,y:32790,varname:node_5770,prsc:2|A-4742-OUT,B-6238-OUT;n:type:ShaderForge.SFN_TexCoord,id:5706,x:31933,y:32900,varname:node_5706,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:1575,x:32303,y:32882,varname:node_1575,prsc:2|A-5770-OUT,B-5706-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:4742,x:32110,y:32591,ptovrint:False,ptlb:DistortionIntensity,ptin:_DistortionIntensity,varname:node_4742,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_ValueProperty,id:1330,x:31878,y:32477,ptovrint:False,ptlb:DistortionSpeed,ptin:_DistortionSpeed,varname:node_1330,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:5795,x:32440,y:32022,ptovrint:False,ptlb:Color_2,ptin:_Color_2,varname:node_5795,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5172414,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:3230,x:32440,y:32162,varname:node_3230,prsc:2|A-1851-OUT,B-6890-OUT,T-4239-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5671,x:31927,y:32134,varname:node_5671,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5706-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1851,x:32440,y:31856,varname:node_1851,prsc:2|A-5795-RGB,B-3491-OUT;n:type:ShaderForge.SFN_Multiply,id:6890,x:32440,y:32305,varname:node_6890,prsc:2|A-7241-RGB,B-314-OUT;n:type:ShaderForge.SFN_OneMinus,id:36,x:31927,y:31981,varname:node_36,prsc:2|IN-5671-OUT;n:type:ShaderForge.SFN_Blend,id:7037,x:32708,y:32554,varname:node_7037,prsc:2,blmd:1,clmp:True|SRC-3230-OUT,DST-2515-RGB;n:type:ShaderForge.SFN_RemapRange,id:3491,x:32158,y:31921,varname:node_3491,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-36-OUT;n:type:ShaderForge.SFN_RemapRange,id:314,x:32110,y:32134,varname:node_314,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-5671-OUT;n:type:ShaderForge.SFN_Multiply,id:2647,x:31368,y:32531,varname:node_2647,prsc:2|A-7484-OUT,B-8370-T;n:type:ShaderForge.SFN_ValueProperty,id:7484,x:31368,y:32475,ptovrint:False,ptlb:ColVarSpeed,ptin:_ColVarSpeed,varname:node_7484,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Sin,id:4849,x:31285,y:32711,varname:node_4849,prsc:2|IN-2647-OUT;n:type:ShaderForge.SFN_RemapRange,id:4239,x:31475,y:32711,varname:node_4239,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-4849-OUT;proporder:7241-2515-579-3264-4742-1330-5795-7484;pass:END;sub:END;*/

Shader "Shader Forge/FenixplosionShader" {
    Properties {
        _Color ("Color", Color) = (1,0.8482759,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Glow ("Glow", Range(0, 2)) = 2
        _Flow ("Flow", 2D) = "white" {}
        _DistortionIntensity ("DistortionIntensity", Float ) = 0.15
        _DistortionSpeed ("DistortionSpeed", Float ) = 2
        _Color_2 ("Color_2", Color) = (1,0.5172414,0,1)
        _ColVarSpeed ("ColVarSpeed", Float ) = 10
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
            uniform float _Glow;
            uniform sampler2D _Flow; uniform float4 _Flow_ST;
            uniform float _DistortionIntensity;
            uniform float _DistortionSpeed;
            uniform float4 _Color_2;
            uniform float _ColVarSpeed;
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
                float4 _Flow_var = tex2D(_Flow,TRANSFORM_TEX(i.uv0, _Flow));
                float4 node_8370 = _Time + _TimeEditor;
                float2 node_1575 = ((_DistortionIntensity*((_Flow_var.rgb.rg*1.0+-0.5)*(sin((_DistortionSpeed*node_8370.g))*1.0+0.0)))+i.uv0);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_1575, _Tex));
                clip(_Tex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float node_5671 = i.uv0.r;
                float3 node_3230 = lerp((_Color_2.rgb*((1.0 - node_5671)*0.5+0.5)),(_Color.rgb*(node_5671*0.5+0.5)),(sin((_ColVarSpeed*node_8370.g))*0.5+0.5));
                float3 emissive = (_Glow*saturate((node_3230*_Tex_var.rgb)));
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
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform sampler2D _Flow; uniform float4 _Flow_ST;
            uniform float _DistortionIntensity;
            uniform float _DistortionSpeed;
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
                float4 _Flow_var = tex2D(_Flow,TRANSFORM_TEX(i.uv0, _Flow));
                float4 node_8370 = _Time + _TimeEditor;
                float2 node_1575 = ((_DistortionIntensity*((_Flow_var.rgb.rg*1.0+-0.5)*(sin((_DistortionSpeed*node_8370.g))*1.0+0.0)))+i.uv0);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_1575, _Tex));
                clip(_Tex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
