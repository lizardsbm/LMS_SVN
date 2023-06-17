// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32223,y:32595,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d3b2d3333e8d10f499ff712067f70a6a,ntxv:0,isnm:False|UVIN-4632-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32519,y:32857,varname:node_2393,prsc:2|A-6074-R,B-2053-RGB,C-2053-B,D-2960-G;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32270,y:32962,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:2228,x:31840,y:32574,varname:node_2228,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:4632,x:32043,y:32574,varname:node_4632,prsc:2,spu:-0.1,spv:0|UVIN-2228-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2960,x:32230,y:32794,ptovrint:False,ptlb:node_2960,ptin:_node_2960,varname:node_2960,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d3b2d3333e8d10f499ff712067f70a6a,ntxv:0,isnm:False|UVIN-5018-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1118,x:31829,y:32755,varname:node_1118,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:5018,x:32032,y:32755,varname:node_5018,prsc:2,spu:0.1,spv:0|UVIN-1118-UVOUT;proporder:6074-2960;pass:END;sub:END;*/

Shader "Shader Forge/Light" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_2960 ("node_2960", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_2960; uniform float4 _node_2960_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_3925 = _Time;
                float2 node_4632 = (i.uv0+node_3925.g*float2(-0.1,0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4632, _MainTex));
                float2 node_5018 = (i.uv0+node_3925.g*float2(0.1,0));
                float4 _node_2960_var = tex2D(_node_2960,TRANSFORM_TEX(node_5018, _node_2960));
                float3 emissive = (_MainTex_var.r*i.vertexColor.rgb*i.vertexColor.b*_node_2960_var.g);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
