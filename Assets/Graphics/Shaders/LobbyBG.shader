// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32842,y:32754,varname:node_3138,prsc:2|emission-8877-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32649,y:32421,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9926471,c2:0.6777383,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2638,x:32375,y:32466,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_2638,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:40f2059a0321fd04ca0373977b9c5485,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1036,x:32375,y:32938,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_1036,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b9ab55e8e0fb70243bec0a68f10752a9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8505,x:32000,y:32828,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_8505,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a28feb60112241146af1bc5d64b88862,ntxv:0,isnm:False|UVIN-7835-OUT;n:type:ShaderForge.SFN_Multiply,id:7791,x:32649,y:32582,varname:node_7791,prsc:2|A-7241-RGB,B-3177-OUT;n:type:ShaderForge.SFN_Multiply,id:7921,x:32847,y:32582,varname:node_7921,prsc:2|A-7380-OUT,B-7791-OUT;n:type:ShaderForge.SFN_Add,id:8877,x:32649,y:32730,varname:node_8877,prsc:2|A-7091-OUT,B-7921-OUT;n:type:ShaderForge.SFN_OneMinus,id:9512,x:32375,y:32776,varname:node_9512,prsc:2|IN-1036-RGB;n:type:ShaderForge.SFN_Multiply,id:3177,x:32375,y:33109,varname:node_3177,prsc:2|A-1036-RGB,B-3634-OUT;n:type:ShaderForge.SFN_Multiply,id:7091,x:32375,y:32637,varname:node_7091,prsc:2|A-9909-OUT,B-9512-OUT;n:type:ShaderForge.SFN_RemapRange,id:3634,x:32000,y:33003,varname:node_3634,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1.85|IN-8505-RGB;n:type:ShaderForge.SFN_ValueProperty,id:7380,x:32847,y:32522,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_7380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.5;n:type:ShaderForge.SFN_Tex2d,id:3859,x:31973,y:32410,ptovrint:False,ptlb:Wood,ptin:_Wood,varname:node_3859,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4d5ba12171a65ae43b0c84171bc6189c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:9909,x:32375,y:32276,varname:node_9909,prsc:2,blmd:15,clmp:True|SRC-6529-OUT,DST-2638-RGB;n:type:ShaderForge.SFN_RemapRange,id:6529,x:32169,y:32448,varname:node_6529,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.9|IN-3859-RGB;n:type:ShaderForge.SFN_Time,id:9250,x:31792,y:33274,varname:node_9250,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3083,x:32000,y:33210,varname:node_3083,prsc:2|A-1146-OUT,B-9250-T;n:type:ShaderForge.SFN_Vector1,id:1146,x:31792,y:33210,varname:node_1146,prsc:2,v1:1;n:type:ShaderForge.SFN_Sin,id:4221,x:32000,y:33360,varname:node_4221,prsc:2|IN-3083-OUT;n:type:ShaderForge.SFN_RemapRange,id:827,x:32192,y:33360,varname:node_827,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-4221-OUT;n:type:ShaderForge.SFN_TexCoord,id:9329,x:31576,y:32832,varname:node_9329,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:5642,x:31792,y:32832,varname:node_5642,prsc:2|A-9329-V,B-8334-OUT;n:type:ShaderForge.SFN_OneMinus,id:8334,x:31576,y:32998,varname:node_8334,prsc:2|IN-9329-U;n:type:ShaderForge.SFN_Lerp,id:7835,x:31792,y:33046,varname:node_7835,prsc:2|A-9329-UVOUT,B-5642-OUT,T-827-OUT;proporder:7241-2638-1036-8505-7380-3859;pass:END;sub:END;*/

Shader "Shader Forge/LobbyBG" {
    Properties {
        _Color ("Color", Color) = (0.9926471,0.6777383,0,1)
        _Tex ("Tex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _Glow ("Glow", Float ) = 2.5
        _Wood ("Wood", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Glow;
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
////// Lighting:
////// Emissive:
                float4 _Wood_var = tex2D(_Wood,TRANSFORM_TEX(i.uv0, _Wood));
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(i.uv0, _Tex));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 node_9250 = _Time + _TimeEditor;
                float node_4221 = sin((1.0*node_9250.g));
                float2 node_7835 = lerp(i.uv0,float2(i.uv0.g,(1.0 - i.uv0.r)),(node_4221*0.5+0.5));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_7835, _Noise));
                float3 emissive = ((saturate(( (_Wood_var.rgb*0.9+0.0) > 0.5 ? max(_Tex_var.rgb,2.0*((_Wood_var.rgb*0.9+0.0)-0.5)) : min(_Tex_var.rgb,2.0*(_Wood_var.rgb*0.9+0.0)) ))*(1.0 - _Mask_var.rgb))+(_Glow*(_Color.rgb*(_Mask_var.rgb*(_Noise_var.rgb*1.85+0.0)))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
