// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32803,y:32809,varname:node_4795,prsc:2|emission-1942-OUT,alpha-8155-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32281,y:33330,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9e3b899ce856a7b4ba06e40f9ee21f2e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:9248,x:32095,y:33330,varname:node_9248,prsc:2,v1:8;n:type:ShaderForge.SFN_Tex2d,id:4150,x:32095,y:32946,ptovrint:False,ptlb:node_4150,ptin:_node_4150,varname:node_4150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a67eeb275837b64ead2bfa8e5f03a48,ntxv:0,isnm:False|UVIN-7374-UVOUT;n:type:ShaderForge.SFN_Panner,id:7374,x:31832,y:32914,varname:node_7374,prsc:2,spu:-0.5,spv:0.3|UVIN-2431-OUT;n:type:ShaderForge.SFN_TexCoord,id:6324,x:31192,y:32831,varname:node_6324,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:1735,x:32095,y:33160,ptovrint:False,ptlb:node_4150_copy,ptin:_node_4150_copy,varname:_node_4150_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:218b990700f79cd48ac748566f8c6101,ntxv:0,isnm:False|UVIN-2308-UVOUT;n:type:ShaderForge.SFN_Panner,id:2308,x:31852,y:33128,varname:node_2308,prsc:2,spu:0.2,spv:0.1|UVIN-3198-OUT;n:type:ShaderForge.SFN_Multiply,id:8180,x:32486,y:33066,varname:node_8180,prsc:2|A-4150-R,B-1735-R,C-9248-OUT,D-6074-R;n:type:ShaderForge.SFN_Multiply,id:3198,x:31640,y:33054,varname:node_3198,prsc:2|A-6324-UVOUT,B-606-OUT;n:type:ShaderForge.SFN_Vector1,id:1112,x:31171,y:33304,varname:node_1112,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Append,id:606,x:31407,y:33187,varname:node_606,prsc:2|A-4786-OUT,B-1112-OUT;n:type:ShaderForge.SFN_Vector1,id:4786,x:31164,y:33199,varname:node_4786,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:2431,x:31640,y:32812,varname:node_2431,prsc:2|A-6324-UVOUT,B-5677-OUT;n:type:ShaderForge.SFN_Vector1,id:9852,x:31171,y:33062,varname:node_9852,prsc:2,v1:1;n:type:ShaderForge.SFN_Append,id:5677,x:31407,y:32945,varname:node_5677,prsc:2|A-4147-OUT,B-9852-OUT;n:type:ShaderForge.SFN_Vector1,id:4147,x:31164,y:32957,varname:node_4147,prsc:2,v1:5;n:type:ShaderForge.SFN_Vector1,id:1942,x:32522,y:32938,varname:node_1942,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:8155,x:32640,y:33129,varname:node_8155,prsc:2|IN-8180-OUT;proporder:6074-4150-1735;pass:END;sub:END;*/

Shader "Shader Forge/Watercourse" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_4150 ("node_4150", 2D) = "white" {}
        _node_4150_copy ("node_4150_copy", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform sampler2D _node_4150; uniform float4 _node_4150_ST;
            uniform sampler2D _node_4150_copy; uniform float4 _node_4150_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_1942 = 1.0;
                float3 emissive = float3(node_1942,node_1942,node_1942);
                float3 finalColor = emissive;
                float4 node_8554 = _Time;
                float2 node_7374 = ((i.uv0*float2(5.0,1.0))+node_8554.g*float2(-0.5,0.3));
                float4 _node_4150_var = tex2D(_node_4150,TRANSFORM_TEX(node_7374, _node_4150));
                float2 node_2308 = ((i.uv0*float2(3.0,0.5))+node_8554.g*float2(0.2,0.1));
                float4 _node_4150_copy_var = tex2D(_node_4150_copy,TRANSFORM_TEX(node_2308, _node_4150_copy));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                fixed4 finalRGBA = fixed4(finalColor,saturate((_node_4150_var.r*_node_4150_copy_var.r*8.0*_MainTex_var.r)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
