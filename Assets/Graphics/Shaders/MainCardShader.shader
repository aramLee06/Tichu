// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33390,y:32683,varname:node_3138,prsc:2|emission-3214-OUT,clip-6429-A;n:type:ShaderForge.SFN_Color,id:7241,x:32757,y:32839,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843139,c2:0.3921568,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6429,x:32757,y:32656,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6429,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fa30785c839c0074bbc593f978d459ac,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:6662,x:33212,y:32912,varname:node_6662,prsc:2,blmd:12,clmp:True|SRC-6429-RGB,DST-894-OUT;n:type:ShaderForge.SFN_Lerp,id:6421,x:32970,y:32897,varname:node_6421,prsc:2|A-9383-OUT,B-1318-OUT,T-4250-OUT;n:type:ShaderForge.SFN_Time,id:4691,x:32562,y:32770,varname:node_4691,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4345,x:32562,y:32991,varname:node_4345,prsc:2|A-4888-OUT,B-4691-T;n:type:ShaderForge.SFN_Sin,id:9777,x:32757,y:33028,varname:node_9777,prsc:2|IN-4345-OUT;n:type:ShaderForge.SFN_RemapRange,id:4250,x:32970,y:33028,varname:node_4250,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-9777-OUT;n:type:ShaderForge.SFN_Multiply,id:894,x:33212,y:33077,varname:node_894,prsc:2|A-6421-OUT,B-7241-RGB;n:type:ShaderForge.SFN_ValueProperty,id:9383,x:32970,y:32755,ptovrint:False,ptlb:MinGlow,ptin:_MinGlow,varname:node_9383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1318,x:32970,y:32839,ptovrint:False,ptlb:MaxGlow,ptin:_MaxGlow,varname:_MinGlow_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.5;n:type:ShaderForge.SFN_ValueProperty,id:4888,x:32562,y:32926,ptovrint:False,ptlb:PulseSpeed,ptin:_PulseSpeed,varname:node_4888,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.5;n:type:ShaderForge.SFN_SwitchProperty,id:3214,x:33212,y:32783,ptovrint:False,ptlb:Selected,ptin:_Selected,varname:node_3214,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6429-RGB,B-6662-OUT;proporder:7241-6429-1318-9383-4888-3214;pass:END;sub:END;*/

Shader "Shader Forge/MainCardShader" {
    Properties {
        _Color ("Color", Color) = (0.07843139,0.3921568,0.7843137,1)
        [PerRendererData] _MainTex ("MainTex", 2D) = "white" {}
        _MaxGlow ("MaxGlow", Float ) = 2.5
        _MinGlow ("MinGlow", Float ) = 1
        _PulseSpeed ("PulseSpeed", Float ) = 2.5
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _MinGlow;
            uniform float _MaxGlow;
            uniform float _PulseSpeed;
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
                float4 node_4691 = _Time + _TimeEditor;
                float3 emissive = lerp( _MainTex_var.rgb, saturate((_MainTex_var.rgb > 0.5 ?  (1.0-(1.0-2.0*(_MainTex_var.rgb-0.5))*(1.0-(lerp(_MinGlow,_MaxGlow,(sin((_PulseSpeed*node_4691.g))*0.5+0.5))*_Color.rgb))) : (2.0*_MainTex_var.rgb*(lerp(_MinGlow,_MaxGlow,(sin((_PulseSpeed*node_4691.g))*0.5+0.5))*_Color.rgb))) ), _Selected );
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
