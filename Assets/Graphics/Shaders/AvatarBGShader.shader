// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2852-OUT,clip-4740-A;n:type:ShaderForge.SFN_Color,id:7241,x:32479,y:32554,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9926471,c3:0.9926471,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6809,x:32246,y:32726,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6809,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aed8120cf57c1cc44bcff3c51c05c196,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4740,x:32246,y:32907,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_4740,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:965b0e9ffa670d044b37f6c33fe0dfb6,ntxv:0,isnm:False|UVIN-5771-OUT;n:type:ShaderForge.SFN_Multiply,id:2852,x:32479,y:32705,varname:node_2852,prsc:2|A-7241-RGB,B-7697-OUT;n:type:ShaderForge.SFN_TexCoord,id:3307,x:31647,y:32787,varname:node_3307,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:7026,x:31876,y:32787,varname:node_7026,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-3307-UVOUT;n:type:ShaderForge.SFN_Add,id:3873,x:32479,y:33035,varname:node_3873,prsc:2|A-8410-OUT,B-6809-RGB;n:type:ShaderForge.SFN_Multiply,id:8577,x:31876,y:33089,varname:node_8577,prsc:2|A-8337-OUT,B-480-RGB;n:type:ShaderForge.SFN_Color,id:480,x:31650,y:33057,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_480,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4191176,c2:0.2896842,c3:0.2896842,c4:1;n:type:ShaderForge.SFN_OneMinus,id:8337,x:31876,y:32952,varname:node_8337,prsc:2|IN-7026-OUT;n:type:ShaderForge.SFN_Multiply,id:8410,x:32070,y:33089,varname:node_8410,prsc:2|A-7432-OUT,B-8577-OUT;n:type:ShaderForge.SFN_Time,id:7352,x:31650,y:33267,varname:node_7352,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5128,x:31876,y:33214,varname:node_5128,prsc:2|A-6215-OUT,B-7352-T;n:type:ShaderForge.SFN_Vector1,id:6215,x:31650,y:33214,varname:node_6215,prsc:2,v1:3;n:type:ShaderForge.SFN_Sin,id:6490,x:32070,y:33214,varname:node_6490,prsc:2|IN-5128-OUT;n:type:ShaderForge.SFN_RemapRange,id:7432,x:32246,y:33214,varname:node_7432,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-6490-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:7697,x:32479,y:32869,ptovrint:False,ptlb:GlowOn,ptin:_GlowOn,varname:node_7697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6809-RGB,B-3873-OUT;n:type:ShaderForge.SFN_Append,id:3053,x:31736,y:32323,varname:node_3053,prsc:2|A-5530-OUT,B-3307-V;n:type:ShaderForge.SFN_OneMinus,id:5530,x:31736,y:32460,varname:node_5530,prsc:2|IN-3307-U;n:type:ShaderForge.SFN_SwitchProperty,id:4602,x:31736,y:32620,ptovrint:False,ptlb:FlipY,ptin:_FlipY,varname:node_4602,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3307-UVOUT,B-3053-OUT;n:type:ShaderForge.SFN_OneMinus,id:3059,x:32024,y:32620,varname:node_3059,prsc:2|IN-4602-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:5771,x:32024,y:32486,ptovrint:False,ptlb:FlipX,ptin:_FlipX,varname:node_5771,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-4602-OUT,B-3059-OUT;proporder:7241-4740-6809-480-7697-4602-5771;pass:END;sub:END;*/

Shader "Shader Forge/AvatarBGShader" {
    Properties {
        _Tint ("Tint", Color) = (1,0.9926471,0.9926471,1)
        _Mask ("Mask", 2D) = "white" {}
        [PerRendererData] _MainTex ("MainTex", 2D) = "white" {}
        _Glow ("Glow", Color) = (0.4191176,0.2896842,0.2896842,1)
        [MaterialToggle] _GlowOn ("GlowOn", Float ) = 0
        [MaterialToggle] _FlipY ("FlipY", Float ) = 0
        [MaterialToggle] _FlipX ("FlipX", Float ) = 0
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
            uniform float4 _Tint;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _Glow;
            uniform fixed _GlowOn;
            uniform fixed _FlipY;
            uniform fixed _FlipX;
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
                float2 _FlipY_var = lerp( i.uv0, float2((1.0 - i.uv0.r),i.uv0.g), _FlipY );
                float2 _FlipX_var = lerp( _FlipY_var, (1.0 - _FlipY_var), _FlipX );
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(_FlipX_var, _Mask));
                clip(_Mask_var.a - 0.5);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_7352 = _Time + _TimeEditor;
                float3 emissive = (_Tint.rgb*lerp( _MainTex_var.rgb, (((sin((3.0*node_7352.g))*0.5+0.5)*((1.0 - i.uv0.g)*_Glow.rgb))+_MainTex_var.rgb), _GlowOn ));
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
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform fixed _FlipY;
            uniform fixed _FlipX;
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
                float2 _FlipY_var = lerp( i.uv0, float2((1.0 - i.uv0.r),i.uv0.g), _FlipY );
                float2 _FlipX_var = lerp( _FlipY_var, (1.0 - _FlipY_var), _FlipX );
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(_FlipX_var, _Mask));
                clip(_Mask_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
