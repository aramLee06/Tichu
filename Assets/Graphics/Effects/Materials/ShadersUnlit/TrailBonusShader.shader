// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32833,y:32698,varname:node_3138,prsc:2|emission-7836-OUT,clip-5375-A;n:type:ShaderForge.SFN_Color,id:7241,x:32163,y:32827,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:5375,x:32163,y:32631,ptovrint:False,ptlb:ParticleTex,ptin:_ParticleTex,varname:node_5375,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:847410f9c01f1cf49bb1413dc04d1aad,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7836,x:32898,y:32560,varname:node_7836,prsc:2|A-715-OUT,B-3-OUT;n:type:ShaderForge.SFN_TexCoord,id:8737,x:32163,y:32453,varname:node_8737,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:7251,x:32163,y:32271,varname:node_7251,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8737-UVOUT;n:type:ShaderForge.SFN_Length,id:9614,x:32521,y:32470,varname:node_9614,prsc:2|IN-7251-OUT;n:type:ShaderForge.SFN_Multiply,id:6244,x:32548,y:32692,varname:node_6244,prsc:2|A-5375-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Blend,id:3,x:32898,y:32383,varname:node_3,prsc:2,blmd:8,clmp:True|SRC-3016-OUT,DST-6244-OUT;n:type:ShaderForge.SFN_Slider,id:715,x:32741,y:32310,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_715,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.948718,max:1.5;n:type:ShaderForge.SFN_Multiply,id:3016,x:32521,y:32336,varname:node_3016,prsc:2|A-6789-OUT,B-9614-OUT;n:type:ShaderForge.SFN_Vector1,id:6789,x:32521,y:32257,varname:node_6789,prsc:2,v1:2;proporder:7241-5375-715;pass:END;sub:END;*/

Shader "Shader Forge/TrailBonusShader" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _ParticleTex ("ParticleTex", 2D) = "black" {}
        _Intensity ("Intensity", Range(0, 1.5)) = 0.948718
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
            uniform sampler2D _ParticleTex; uniform float4 _ParticleTex_ST;
            uniform float _Intensity;
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
                float4 _ParticleTex_var = tex2D(_ParticleTex,TRANSFORM_TEX(i.uv0, _ParticleTex));
                clip(_ParticleTex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float node_9614 = length((i.uv0*2.0+-1.0));
                float3 node_6244 = (_ParticleTex_var.rgb*_Color.rgb);
                float3 emissive = (_Intensity*saturate(((2.0*node_9614)+node_6244)));
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
            uniform sampler2D _ParticleTex; uniform float4 _ParticleTex_ST;
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
                float4 _ParticleTex_var = tex2D(_ParticleTex,TRANSFORM_TEX(i.uv0, _ParticleTex));
                clip(_ParticleTex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
