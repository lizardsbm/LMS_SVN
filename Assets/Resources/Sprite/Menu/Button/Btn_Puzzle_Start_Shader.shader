// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32780,y:32726,varname:node_3138,prsc:2|emission-4359-OUT;n:type:ShaderForge.SFN_Tex2d,id:3242,x:32371,y:32763,ptovrint:False,ptlb:node_3242,ptin:_node_3242,varname:node_3242,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dfc499118f47fee429337e4c8081398f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2402,x:32334,y:32948,ptovrint:False,ptlb:node_2402,ptin:_node_2402,varname:node_2402,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b8bae27882a796f4bb432144c9646337,ntxv:0,isnm:False|UVIN-5211-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7483,x:31460,y:32966,varname:node_7483,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:719,x:31689,y:33013,varname:node_719,prsc:2,spu:0.2,spv:0.2|UVIN-8090-OUT;n:type:ShaderForge.SFN_Add,id:4359,x:32628,y:32867,varname:node_4359,prsc:2|A-3242-RGB,B-6930-OUT;n:type:ShaderForge.SFN_Multiply,id:6930,x:32588,y:33046,varname:node_6930,prsc:2|A-2402-A,B-2038-OUT;n:type:ShaderForge.SFN_Slider,id:2038,x:32509,y:33310,ptovrint:False,ptlb:node_2038,ptin:_node_2038,varname:node_2038,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.06086956,max:0.2;n:type:ShaderForge.SFN_Multiply,id:3267,x:32142,y:33163,varname:node_3267,prsc:2|A-9445-OUT,B-7440-OUT,C-2240-OUT;n:type:ShaderForge.SFN_Add,id:5519,x:32346,y:33163,varname:node_5519,prsc:2|A-3267-OUT,B-7483-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8090,x:31829,y:32684,varname:node_8090,prsc:2|A-7483-UVOUT,B-4256-OUT;n:type:ShaderForge.SFN_Vector1,id:4256,x:31616,y:32746,varname:node_4256,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Panner,id:564,x:31651,y:33251,varname:node_564,prsc:2,spu:-0.1,spv:-0.5|UVIN-8090-OUT;n:type:ShaderForge.SFN_Slider,id:2240,x:32025,y:33446,ptovrint:False,ptlb:Move,ptin:_Move,varname:node_2240,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.607486,max:5;n:type:ShaderForge.SFN_Panner,id:5211,x:32077,y:32874,varname:node_5211,prsc:2,spu:-1,spv:0|UVIN-5519-OUT;n:type:ShaderForge.SFN_Tex2d,id:9536,x:31886,y:33035,ptovrint:False,ptlb:node_9536,ptin:_node_9536,varname:node_9536,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8060f12be6af7994db7ddba6bd3a3219,ntxv:0,isnm:False|UVIN-719-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7339,x:31790,y:33259,ptovrint:False,ptlb:node_9536_copy,ptin:_node_9536_copy,varname:_node_9536_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8060f12be6af7994db7ddba6bd3a3219,ntxv:0,isnm:False|UVIN-564-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9445,x:32041,y:33069,varname:node_9445,prsc:2|A-9536-R,B-5633-OUT;n:type:ShaderForge.SFN_Multiply,id:7440,x:31947,y:33313,varname:node_7440,prsc:2|A-7339-R,B-8492-OUT;n:type:ShaderForge.SFN_Slider,id:8492,x:31653,y:33545,ptovrint:False,ptlb:intense2,ptin:_intense2,varname:node_8492,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3158569,max:1;n:type:ShaderForge.SFN_Slider,id:5633,x:31476,y:33455,ptovrint:False,ptlb:intense1,ptin:_intense1,varname:node_5633,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3966261,max:1;proporder:3242-2402-2038-2240-9536-7339-5633-8492;pass:END;sub:END;*/

Shader "Shader Forge/Btn_Puzzle_Start_Shader" {
    Properties {
        _node_3242 ("node_3242", 2D) = "white" {}
        _node_2402 ("node_2402", 2D) = "white" {}
        _node_2038 ("node_2038", Range(0, 0.2)) = 0.06086956
        _Move ("Move", Range(0, 5)) = 1.607486
        _node_9536 ("node_9536", 2D) = "white" {}
        _node_9536_copy ("node_9536_copy", 2D) = "white" {}
        _intense1 ("intense1", Range(0, 1)) = 0.3966261
        _intense2 ("intense2", Range(0, 1)) = 0.3158569
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _node_3242; uniform float4 _node_3242_ST;
            uniform sampler2D _node_2402; uniform float4 _node_2402_ST;
            uniform float _node_2038;
            uniform float _Move;
            uniform sampler2D _node_9536; uniform float4 _node_9536_ST;
            uniform sampler2D _node_9536_copy; uniform float4 _node_9536_copy_ST;
            uniform float _intense2;
            uniform float _intense1;
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
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _node_3242_var = tex2D(_node_3242,TRANSFORM_TEX(i.uv0, _node_3242));
                float4 node_3237 = _Time;
                float2 node_8090 = (i.uv0*0.5);
                float2 node_719 = (node_8090+node_3237.g*float2(0.2,0.2));
                float4 _node_9536_var = tex2D(_node_9536,TRANSFORM_TEX(node_719, _node_9536));
                float2 node_564 = (node_8090+node_3237.g*float2(-0.1,-0.5));
                float4 _node_9536_copy_var = tex2D(_node_9536_copy,TRANSFORM_TEX(node_564, _node_9536_copy));
                float2 node_5211 = ((((_node_9536_var.r*_intense1)*(_node_9536_copy_var.r*_intense2)*_Move)+i.uv0)+node_3237.g*float2(-1,0));
                float4 _node_2402_var = tex2D(_node_2402,TRANSFORM_TEX(node_5211, _node_2402));
                float3 emissive = (_node_3242_var.rgb+(_node_2402_var.a*_node_2038));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
