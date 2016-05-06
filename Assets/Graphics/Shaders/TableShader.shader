// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32845,y:32595,varname:node_2865,prsc:2|diff-5420-OUT,spec-2644-OUT,gloss-4254-OUT,normal-2759-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:31905,y:31938,varname:node_6343,prsc:2|A-5260-OUT,B-3535-OUT;n:type:ShaderForge.SFN_Multiply,id:5798,x:31905,y:32861,varname:node_5798,prsc:2|A-1177-RGB,B-244-OUT;n:type:ShaderForge.SFN_Multiply,id:9680,x:31905,y:33755,varname:node_9680,prsc:2|A-7759-OUT,B-8840-OUT;n:type:ShaderForge.SFN_VertexColor,id:1499,x:31343,y:32724,varname:node_1499,prsc:2;n:type:ShaderForge.SFN_Add,id:2837,x:32303,y:32597,varname:node_2837,prsc:2|A-6343-OUT,B-5798-OUT;n:type:ShaderForge.SFN_Add,id:5420,x:32585,y:32594,varname:node_5420,prsc:2|A-2837-OUT,B-9680-OUT;n:type:ShaderForge.SFN_Tex2d,id:1177,x:31905,y:32682,ptovrint:False,ptlb:Felt,ptin:_Felt,varname:node_1177,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6768298a4e8b0914582f9fb6fa111123,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5463,x:31905,y:33590,ptovrint:False,ptlb:Stone,ptin:_Stone,varname:node_5463,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3e560cd6594a3f3459db4969f17ffacd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7660,x:31905,y:31770,ptovrint:False,ptlb:Wood,ptin:_Wood,varname:node_7660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4d5ba12171a65ae43b0c84171bc6189c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7900,x:31905,y:32530,varname:node_7900,prsc:2|A-1999-OUT,B-244-OUT;n:type:ShaderForge.SFN_Vector1,id:1999,x:31905,y:32466,varname:node_1999,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:6084,x:31905,y:33242,varname:node_6084,prsc:2|A-5230-OUT,B-8840-OUT;n:type:ShaderForge.SFN_Vector1,id:5230,x:31905,y:33177,varname:node_5230,prsc:2,v1:0.4;n:type:ShaderForge.SFN_Multiply,id:2308,x:31905,y:32140,varname:node_2308,prsc:2|A-4600-OUT,B-3535-OUT;n:type:ShaderForge.SFN_Vector1,id:4600,x:31905,y:32073,varname:node_4600,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Add,id:3016,x:32303,y:32751,varname:node_3016,prsc:2|A-2308-OUT,B-7900-OUT;n:type:ShaderForge.SFN_Add,id:2644,x:32585,y:32748,varname:node_2644,prsc:2|A-3016-OUT,B-6084-OUT;n:type:ShaderForge.SFN_Multiply,id:9517,x:31905,y:32325,varname:node_9517,prsc:2|A-943-OUT,B-3535-OUT;n:type:ShaderForge.SFN_Vector1,id:943,x:31905,y:32260,varname:node_943,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6546,x:31905,y:33052,varname:node_6546,prsc:2|A-1489-OUT,B-244-OUT;n:type:ShaderForge.SFN_Vector1,id:1489,x:31905,y:32992,varname:node_1489,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:7642,x:31905,y:33440,varname:node_7642,prsc:2|A-8623-OUT,B-8840-OUT;n:type:ShaderForge.SFN_Vector1,id:8623,x:31905,y:33370,varname:node_8623,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Add,id:4563,x:32303,y:32906,varname:node_4563,prsc:2|A-9517-OUT,B-6546-OUT;n:type:ShaderForge.SFN_Add,id:4254,x:32585,y:32904,varname:node_4254,prsc:2|A-4563-OUT,B-7642-OUT;n:type:ShaderForge.SFN_Relay,id:3535,x:31588,y:32524,varname:node_3535,prsc:2|IN-1499-B;n:type:ShaderForge.SFN_Relay,id:244,x:31588,y:32745,varname:node_244,prsc:2|IN-1499-R;n:type:ShaderForge.SFN_Relay,id:8840,x:31588,y:32978,varname:node_8840,prsc:2|IN-1499-G;n:type:ShaderForge.SFN_Tex2d,id:6277,x:32158,y:31770,ptovrint:False,ptlb:WoodNormal,ptin:_WoodNormal,varname:node_6277,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4cc44bb80ecef1242bc961d4910c326d,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:2572,x:32241,y:32321,ptovrint:False,ptlb:FeltNormal,ptin:_FeltNormal,varname:_WoodNormal_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:231fec610ef2f7a4a81cab3bbca829c3,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:394,x:32250,y:33500,ptovrint:False,ptlb:StoneNormal,ptin:_StoneNormal,varname:_FeltNormal_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:622b325d655784b4ebc557d712171a95,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:1101,x:32250,y:33664,varname:node_1101,prsc:2|A-394-RGB,B-8840-OUT;n:type:ShaderForge.SFN_Add,id:7519,x:32450,y:33114,varname:node_7519,prsc:2|A-6645-OUT,B-1101-OUT;n:type:ShaderForge.SFN_Multiply,id:6645,x:32241,y:32170,varname:node_6645,prsc:2|A-2572-RGB,B-244-OUT;n:type:ShaderForge.SFN_Add,id:2759,x:32655,y:33114,varname:node_2759,prsc:2|A-7250-OUT,B-7519-OUT;n:type:ShaderForge.SFN_Multiply,id:7250,x:32158,y:31941,varname:node_7250,prsc:2|A-6277-RGB,B-3535-OUT;n:type:ShaderForge.SFN_Color,id:3307,x:31494,y:33693,ptovrint:False,ptlb:Gold,ptin:_Gold,varname:node_3307,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7279412,c2:0.5873733,c3:0,c4:1;n:type:ShaderForge.SFN_Blend,id:7759,x:31682,y:33693,varname:node_7759,prsc:2,blmd:12,clmp:True|SRC-3307-RGB,DST-5463-RGB;n:type:ShaderForge.SFN_Blend,id:5260,x:31691,y:31770,varname:node_5260,prsc:2,blmd:12,clmp:True|SRC-1564-RGB,DST-7660-RGB;n:type:ShaderForge.SFN_Color,id:1564,x:31503,y:31770,ptovrint:False,ptlb:Red,ptin:_Red,varname:_Gold_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4191176,c2:0.1386786,c3:0.1386786,c4:1;proporder:7660-1177-5463-6277-2572-394-3307-1564;pass:END;sub:END;*/

Shader "Shader Forge/TableShader" {
    Properties {
        _Wood ("Wood", 2D) = "white" {}
        _Felt ("Felt", 2D) = "white" {}
        _Stone ("Stone", 2D) = "white" {}
        _WoodNormal ("WoodNormal", 2D) = "bump" {}
        _FeltNormal ("FeltNormal", 2D) = "bump" {}
        _StoneNormal ("StoneNormal", 2D) = "bump" {}
        _Gold ("Gold", Color) = (0.7279412,0.5873733,0,1)
        _Red ("Red", Color) = (0.4191176,0.1386786,0.1386786,1)
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Felt; uniform float4 _Felt_ST;
            uniform sampler2D _Stone; uniform float4 _Stone_ST;
            uniform sampler2D _Wood; uniform float4 _Wood_ST;
            uniform sampler2D _WoodNormal; uniform float4 _WoodNormal_ST;
            uniform sampler2D _FeltNormal; uniform float4 _FeltNormal_ST;
            uniform sampler2D _StoneNormal; uniform float4 _StoneNormal_ST;
            uniform float4 _Gold;
            uniform float4 _Red;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _WoodNormal_var = UnpackNormal(tex2D(_WoodNormal,TRANSFORM_TEX(i.uv0, _WoodNormal)));
                float node_3535 = i.vertexColor.b;
                float3 _FeltNormal_var = UnpackNormal(tex2D(_FeltNormal,TRANSFORM_TEX(i.uv0, _FeltNormal)));
                float node_244 = i.vertexColor.r;
                float3 _StoneNormal_var = UnpackNormal(tex2D(_StoneNormal,TRANSFORM_TEX(i.uv0, _StoneNormal)));
                float node_8840 = i.vertexColor.g;
                float3 normalLocal = ((_WoodNormal_var.rgb*node_3535)+((_FeltNormal_var.rgb*node_244)+(_StoneNormal_var.rgb*node_8840)));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = (((0.5*node_3535)+(0.0*node_244))+(0.3*node_8840));
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_2644 = (((0.2*node_3535)+(0.0*node_244))+(0.4*node_8840));
                float3 specularColor = float3(node_2644,node_2644,node_2644);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float4 _Wood_var = tex2D(_Wood,TRANSFORM_TEX(i.uv0, _Wood));
                float4 _Felt_var = tex2D(_Felt,TRANSFORM_TEX(i.uv0, _Felt));
                float4 _Stone_var = tex2D(_Stone,TRANSFORM_TEX(i.uv0, _Stone));
                float3 diffuseColor = (((saturate((_Red.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Red.rgb-0.5))*(1.0-_Wood_var.rgb)) : (2.0*_Red.rgb*_Wood_var.rgb)) )*node_3535)+(_Felt_var.rgb*node_244))+(saturate((_Gold.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Gold.rgb-0.5))*(1.0-_Stone_var.rgb)) : (2.0*_Gold.rgb*_Stone_var.rgb)) )*node_8840));
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Felt; uniform float4 _Felt_ST;
            uniform sampler2D _Stone; uniform float4 _Stone_ST;
            uniform sampler2D _Wood; uniform float4 _Wood_ST;
            uniform sampler2D _WoodNormal; uniform float4 _WoodNormal_ST;
            uniform sampler2D _FeltNormal; uniform float4 _FeltNormal_ST;
            uniform sampler2D _StoneNormal; uniform float4 _StoneNormal_ST;
            uniform float4 _Gold;
            uniform float4 _Red;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _WoodNormal_var = UnpackNormal(tex2D(_WoodNormal,TRANSFORM_TEX(i.uv0, _WoodNormal)));
                float node_3535 = i.vertexColor.b;
                float3 _FeltNormal_var = UnpackNormal(tex2D(_FeltNormal,TRANSFORM_TEX(i.uv0, _FeltNormal)));
                float node_244 = i.vertexColor.r;
                float3 _StoneNormal_var = UnpackNormal(tex2D(_StoneNormal,TRANSFORM_TEX(i.uv0, _StoneNormal)));
                float node_8840 = i.vertexColor.g;
                float3 normalLocal = ((_WoodNormal_var.rgb*node_3535)+((_FeltNormal_var.rgb*node_244)+(_StoneNormal_var.rgb*node_8840)));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = (((0.5*node_3535)+(0.0*node_244))+(0.3*node_8840));
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_2644 = (((0.2*node_3535)+(0.0*node_244))+(0.4*node_8840));
                float3 specularColor = float3(node_2644,node_2644,node_2644);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float4 _Wood_var = tex2D(_Wood,TRANSFORM_TEX(i.uv0, _Wood));
                float4 _Felt_var = tex2D(_Felt,TRANSFORM_TEX(i.uv0, _Felt));
                float4 _Stone_var = tex2D(_Stone,TRANSFORM_TEX(i.uv0, _Stone));
                float3 diffuseColor = (((saturate((_Red.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Red.rgb-0.5))*(1.0-_Wood_var.rgb)) : (2.0*_Red.rgb*_Wood_var.rgb)) )*node_3535)+(_Felt_var.rgb*node_244))+(saturate((_Gold.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Gold.rgb-0.5))*(1.0-_Stone_var.rgb)) : (2.0*_Gold.rgb*_Stone_var.rgb)) )*node_8840));
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Felt; uniform float4 _Felt_ST;
            uniform sampler2D _Stone; uniform float4 _Stone_ST;
            uniform sampler2D _Wood; uniform float4 _Wood_ST;
            uniform float4 _Gold;
            uniform float4 _Red;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _Wood_var = tex2D(_Wood,TRANSFORM_TEX(i.uv0, _Wood));
                float node_3535 = i.vertexColor.b;
                float4 _Felt_var = tex2D(_Felt,TRANSFORM_TEX(i.uv0, _Felt));
                float node_244 = i.vertexColor.r;
                float4 _Stone_var = tex2D(_Stone,TRANSFORM_TEX(i.uv0, _Stone));
                float node_8840 = i.vertexColor.g;
                float3 diffColor = (((saturate((_Red.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Red.rgb-0.5))*(1.0-_Wood_var.rgb)) : (2.0*_Red.rgb*_Wood_var.rgb)) )*node_3535)+(_Felt_var.rgb*node_244))+(saturate((_Gold.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Gold.rgb-0.5))*(1.0-_Stone_var.rgb)) : (2.0*_Gold.rgb*_Stone_var.rgb)) )*node_8840));
                float node_2644 = (((0.2*node_3535)+(0.0*node_244))+(0.4*node_8840));
                float3 specColor = float3(node_2644,node_2644,node_2644);
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float roughness = 1.0 - (((0.5*node_3535)+(0.0*node_244))+(0.3*node_8840));
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
