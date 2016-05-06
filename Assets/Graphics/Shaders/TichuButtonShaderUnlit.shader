// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32904,y:32837,varname:node_3138,prsc:2|diff-7581-RGB,emission-3200-OUT,clip-7581-A;n:type:ShaderForge.SFN_Color,id:7241,x:32085,y:32680,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4344827,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7581,x:32354,y:32564,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ccf1541a5f05f1143b0bbd9a1ea9b6c2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2228,x:32320,y:32767,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:30fdd1aa1439a9243ade24127d835575,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8136,x:32320,y:33061,varname:node_8136,prsc:2|A-2228-RGB,B-1613-OUT;n:type:ShaderForge.SFN_Time,id:3367,x:32085,y:33040,varname:node_3367,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8565,x:32085,y:32828,varname:node_8565,prsc:2|A-5476-OUT,B-3367-TSL;n:type:ShaderForge.SFN_ValueProperty,id:5476,x:32085,y:32983,ptovrint:False,ptlb:GleamSpeed,ptin:_GleamSpeed,varname:node_5476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.03;n:type:ShaderForge.SFN_Tex2d,id:7004,x:32085,y:32482,ptovrint:False,ptlb:GleamTex,ptin:_GleamTex,varname:node_7004,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44a2cc0423acc1a478a72255770a325e,ntxv:0,isnm:False|UVIN-9386-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3968,x:31781,y:32662,varname:node_3968,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1613,x:32320,y:32934,varname:node_1613,prsc:2|A-3900-OUT,B-7241-RGB;n:type:ShaderForge.SFN_Add,id:3900,x:32902,y:32528,varname:node_3900,prsc:2|A-8341-OUT,B-1245-OUT;n:type:ShaderForge.SFN_Length,id:2206,x:32719,y:32442,varname:node_2206,prsc:2|IN-5546-OUT;n:type:ShaderForge.SFN_RemapRange,id:5546,x:32533,y:32442,varname:node_5546,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3968-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:7491,x:32902,y:32399,varname:node_7491,prsc:2|IN-2206-OUT;n:type:ShaderForge.SFN_RemapRange,id:8341,x:32902,y:32664,varname:node_8341,prsc:2,frmn:0,frmx:1,tomn:0.2,tomx:0.6|IN-7491-OUT;n:type:ShaderForge.SFN_Rotator,id:9386,x:31781,y:32813,varname:node_9386,prsc:2|UVIN-3968-UVOUT,SPD-8565-OUT;n:type:ShaderForge.SFN_Multiply,id:1245,x:32658,y:33101,varname:node_1245,prsc:2|A-2232-OUT,B-7004-RGB;n:type:ShaderForge.SFN_Sin,id:110,x:32114,y:33222,varname:node_110,prsc:2|IN-7027-OUT;n:type:ShaderForge.SFN_Multiply,id:7027,x:31910,y:33222,varname:node_7027,prsc:2|A-2524-OUT,B-3367-T;n:type:ShaderForge.SFN_ValueProperty,id:2524,x:31910,y:33121,ptovrint:False,ptlb:PulseSpeed,ptin:_PulseSpeed,varname:node_2524,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_RemapRange,id:2232,x:32320,y:33222,varname:node_2232,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-110-OUT;n:type:ShaderForge.SFN_Multiply,id:8723,x:32647,y:32798,varname:node_8723,prsc:2|A-4516-RGB,B-98-OUT;n:type:ShaderForge.SFN_Color,id:4516,x:32647,y:32637,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_4516,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:3200,x:32647,y:32940,varname:node_3200,prsc:2|A-8723-OUT,B-8136-OUT;n:type:ShaderForge.SFN_Blend,id:98,x:31749,y:33050,varname:node_98,prsc:2,blmd:10,clmp:True|SRC-7581-RGB,DST-2801-RGB;n:type:ShaderForge.SFN_Tex2d,id:2801,x:31781,y:32491,ptovrint:False,ptlb:Wood,ptin:_Wood,varname:node_2801,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4d5ba12171a65ae43b0c84171bc6189c,ntxv:0,isnm:False;proporder:7241-7581-2228-5476-7004-2524-4516-2801;pass:END;sub:END;*/

Shader "Shader Forge/TichuButtonShaderUnlit" {
    Properties {
        _Color ("Color", Color) = (1,0.4344827,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _GleamSpeed ("GleamSpeed", Float ) = 0.03
        _GleamTex ("GleamTex", 2D) = "white" {}
        _PulseSpeed ("PulseSpeed", Float ) = 5
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
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 node_3367 = _Time + _TimeEditor;
                float4 node_770 = _Time + _TimeEditor;
                float node_9386_ang = node_770.g;
                float node_9386_spd = (_GleamSpeed*node_3367.r);
                float node_9386_cos = cos(node_9386_spd*node_9386_ang);
                float node_9386_sin = sin(node_9386_spd*node_9386_ang);
                float2 node_9386_piv = float2(0.5,0.5);
                float2 node_9386 = (mul(i.uv0-node_9386_piv,float2x2( node_9386_cos, -node_9386_sin, node_9386_sin, node_9386_cos))+node_9386_piv);
                float4 _GleamTex_var = tex2D(_GleamTex,TRANSFORM_TEX(node_9386, _GleamTex));
                float3 emissive = ((_Tint.rgb*saturate(( _Wood_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_Wood_var.rgb-0.5))*(1.0-_Tex_var.rgb)) : (2.0*_Wood_var.rgb*_Tex_var.rgb) )))+(_Mask_var.rgb*((((1.0 - length((i.uv0*2.0+-1.0)))*0.4+0.2)+((sin((_PulseSpeed*node_3367.g))*0.5+0.5)*_GleamTex_var.rgb))*_Color.rgb)));
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
