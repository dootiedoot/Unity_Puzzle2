// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-7997-OUT,spec-358-OUT,gloss-1813-OUT;n:type:ShaderForge.SFN_Slider,id:358,x:32615,y:33230,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:32570,y:33144,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_VertexColor,id:2630,x:31910,y:32492,varname:node_2630,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:9085,x:31920,y:32764,varname:node_9085,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7997,x:32407,y:32530,varname:node_7997,prsc:2|A-2630-RGB,B-4617-OUT,C-3066-RGB;n:type:ShaderForge.SFN_If,id:8865,x:32194,y:32666,varname:node_8865,prsc:2|A-9085-U,B-5593-OUT,GT-9000-OUT,EQ-9000-OUT,LT-6298-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9000,x:31923,y:33074,ptovrint:False,ptlb:Normal Brightness,ptin:_NormalBrightness,varname:node_9000,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_If,id:7254,x:32279,y:32801,varname:node_7254,prsc:2|A-9085-V,B-5593-OUT,GT-9000-OUT,EQ-9000-OUT,LT-6298-OUT;n:type:ShaderForge.SFN_Multiply,id:7333,x:32418,y:32687,varname:node_7333,prsc:2|A-8865-OUT,B-7254-OUT;n:type:ShaderForge.SFN_If,id:8047,x:32320,y:33163,varname:node_8047,prsc:2|A-9085-V,B-6151-OUT,GT-6010-OUT,EQ-9000-OUT,LT-9000-OUT;n:type:ShaderForge.SFN_If,id:4165,x:32334,y:32982,varname:node_4165,prsc:2|A-9085-U,B-6151-OUT,GT-6010-OUT,EQ-9000-OUT,LT-9000-OUT;n:type:ShaderForge.SFN_Multiply,id:8957,x:32529,y:33117,varname:node_8957,prsc:2|A-4165-OUT,B-8047-OUT;n:type:ShaderForge.SFN_Multiply,id:4617,x:32570,y:32829,varname:node_4617,prsc:2|A-7333-OUT,B-8957-OUT;n:type:ShaderForge.SFN_OneMinus,id:6151,x:32117,y:33002,varname:node_6151,prsc:2|IN-5593-OUT;n:type:ShaderForge.SFN_OneMinus,id:6298,x:31940,y:33184,varname:node_6298,prsc:2|IN-6017-OUT;n:type:ShaderForge.SFN_Slider,id:6017,x:31872,y:33348,ptovrint:False,ptlb:Outline Intensity,ptin:_OutlineIntensity,varname:node_6017,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3540895,max:1;n:type:ShaderForge.SFN_Slider,id:5593,x:31831,y:32945,ptovrint:False,ptlb:Outline Size,ptin:_OutlineSize,varname:node_5593,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.07264958,max:0.5;n:type:ShaderForge.SFN_Multiply,id:1842,x:32348,y:33314,varname:node_1842,prsc:2|A-6298-OUT,B-7737-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7737,x:32189,y:33444,ptovrint:False,ptlb:3D Lightness,ptin:_3DLightness,varname:node_7737,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:6656,x:32588,y:33336,varname:node_6656,prsc:2|A-1842-OUT,B-6017-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:9081,x:32802,y:33366,varname:node_9081,prsc:2,min:1,max:50|IN-6656-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:6010,x:32445,y:33444,ptovrint:False,ptlb:3D Look,ptin:_3DLook,varname:node_6010,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-6298-OUT,B-9081-OUT;n:type:ShaderForge.SFN_Color,id:3066,x:31920,y:32641,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_3066,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:358-1813-9000-6017-5593-6010-7737-3066;pass:END;sub:END;*/

Shader "PicaVoxel/PicaVoxel PBR Mosaic" {
    Properties {
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        [HideInInspector]_NormalBrightness ("Normal Brightness", Float ) = 1
        _OutlineIntensity ("Outline Intensity", Range(0, 1)) = 0.3540895
        _OutlineSize ("Outline Size", Range(0, 0.5)) = 0.07264958
        [MaterialToggle] _3DLook ("3D Look", Float ) = 1
        _3DLightness ("3D Lightness", Float ) = 10
        _Tint ("Tint", Color) = (1,1,1,1)
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
            #pragma exclude_renderers gles 
            #pragma target 3.0
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _NormalBrightness;
            uniform float _OutlineIntensity;
            uniform float _OutlineSize;
            uniform float _3DLightness;
            uniform fixed _3DLook;
            uniform float4 _Tint;
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
                float3 normalDirection = i.normalDir;
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
                float gloss = _Gloss;
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
                float node_8865_if_leA = step(i.uv0.r,_OutlineSize);
                float node_8865_if_leB = step(_OutlineSize,i.uv0.r);
                float node_6298 = (1.0 - _OutlineIntensity);
                float node_7254_if_leA = step(i.uv0.g,_OutlineSize);
                float node_7254_if_leB = step(_OutlineSize,i.uv0.g);
                float node_6151 = (1.0 - _OutlineSize);
                float node_4165_if_leA = step(i.uv0.r,node_6151);
                float node_4165_if_leB = step(node_6151,i.uv0.r);
                float _3DLook_var = lerp( node_6298, clamp(((node_6298*_3DLightness)*_OutlineIntensity),1,50), _3DLook );
                float node_8047_if_leA = step(i.uv0.g,node_6151);
                float node_8047_if_leB = step(node_6151,i.uv0.g);
                float3 diffuseColor = (i.vertexColor.rgb*((lerp((node_8865_if_leA*node_6298)+(node_8865_if_leB*_NormalBrightness),_NormalBrightness,node_8865_if_leA*node_8865_if_leB)*lerp((node_7254_if_leA*node_6298)+(node_7254_if_leB*_NormalBrightness),_NormalBrightness,node_7254_if_leA*node_7254_if_leB))*(lerp((node_4165_if_leA*_NormalBrightness)+(node_4165_if_leB*_3DLook_var),_NormalBrightness,node_4165_if_leA*node_4165_if_leB)*lerp((node_8047_if_leA*_NormalBrightness)+(node_8047_if_leB*_3DLook_var),_NormalBrightness,node_8047_if_leA*node_8047_if_leB)))*_Tint.rgb); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
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
            #pragma exclude_renderers gles 
            #pragma target 3.0
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _NormalBrightness;
            uniform float _OutlineIntensity;
            uniform float _OutlineSize;
            uniform float _3DLightness;
            uniform fixed _3DLook;
            uniform float4 _Tint;
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
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_8865_if_leA = step(i.uv0.r,_OutlineSize);
                float node_8865_if_leB = step(_OutlineSize,i.uv0.r);
                float node_6298 = (1.0 - _OutlineIntensity);
                float node_7254_if_leA = step(i.uv0.g,_OutlineSize);
                float node_7254_if_leB = step(_OutlineSize,i.uv0.g);
                float node_6151 = (1.0 - _OutlineSize);
                float node_4165_if_leA = step(i.uv0.r,node_6151);
                float node_4165_if_leB = step(node_6151,i.uv0.r);
                float _3DLook_var = lerp( node_6298, clamp(((node_6298*_3DLightness)*_OutlineIntensity),1,50), _3DLook );
                float node_8047_if_leA = step(i.uv0.g,node_6151);
                float node_8047_if_leB = step(node_6151,i.uv0.g);
                float3 diffuseColor = (i.vertexColor.rgb*((lerp((node_8865_if_leA*node_6298)+(node_8865_if_leB*_NormalBrightness),_NormalBrightness,node_8865_if_leA*node_8865_if_leB)*lerp((node_7254_if_leA*node_6298)+(node_7254_if_leB*_NormalBrightness),_NormalBrightness,node_7254_if_leA*node_7254_if_leB))*(lerp((node_4165_if_leA*_NormalBrightness)+(node_4165_if_leB*_3DLook_var),_NormalBrightness,node_4165_if_leA*node_4165_if_leB)*lerp((node_8047_if_leA*_NormalBrightness)+(node_8047_if_leB*_3DLook_var),_NormalBrightness,node_8047_if_leA*node_8047_if_leB)))*_Tint.rgb); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
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
            #pragma exclude_renderers gles 
            #pragma target 3.0
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _NormalBrightness;
            uniform float _OutlineIntensity;
            uniform float _OutlineSize;
            uniform float _3DLightness;
            uniform fixed _3DLook;
            uniform float4 _Tint;
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
                
                float node_8865_if_leA = step(i.uv0.r,_OutlineSize);
                float node_8865_if_leB = step(_OutlineSize,i.uv0.r);
                float node_6298 = (1.0 - _OutlineIntensity);
                float node_7254_if_leA = step(i.uv0.g,_OutlineSize);
                float node_7254_if_leB = step(_OutlineSize,i.uv0.g);
                float node_6151 = (1.0 - _OutlineSize);
                float node_4165_if_leA = step(i.uv0.r,node_6151);
                float node_4165_if_leB = step(node_6151,i.uv0.r);
                float _3DLook_var = lerp( node_6298, clamp(((node_6298*_3DLightness)*_OutlineIntensity),1,50), _3DLook );
                float node_8047_if_leA = step(i.uv0.g,node_6151);
                float node_8047_if_leB = step(node_6151,i.uv0.g);
                float3 diffColor = (i.vertexColor.rgb*((lerp((node_8865_if_leA*node_6298)+(node_8865_if_leB*_NormalBrightness),_NormalBrightness,node_8865_if_leA*node_8865_if_leB)*lerp((node_7254_if_leA*node_6298)+(node_7254_if_leB*_NormalBrightness),_NormalBrightness,node_7254_if_leA*node_7254_if_leB))*(lerp((node_4165_if_leA*_NormalBrightness)+(node_4165_if_leB*_3DLook_var),_NormalBrightness,node_4165_if_leA*node_4165_if_leB)*lerp((node_8047_if_leA*_NormalBrightness)+(node_8047_if_leB*_3DLook_var),_NormalBrightness,node_8047_if_leA*node_8047_if_leB)))*_Tint.rgb);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
