// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33602,y:32423,varname:node_4795,prsc:2|emission-597-OUT,alpha-950-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31924,y:32091,ptovrint:False,ptlb:wave,ptin:_wave,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a67eeb275837b64ead2bfa8e5f03a48,ntxv:0,isnm:False|UVIN-8643-UVOUT;n:type:ShaderForge.SFN_Color,id:797,x:31527,y:32899,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5176471,c2:0.6117647,c3:0.4,c4:1;n:type:ShaderForge.SFN_Panner,id:8643,x:31703,y:32060,varname:node_8643,prsc:2,spu:-0.01,spv:0|UVIN-7068-OUT;n:type:ShaderForge.SFN_TexCoord,id:4223,x:30899,y:31993,varname:node_4223,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:7214,x:31844,y:32314,ptovrint:False,ptlb:wave2,ptin:_wave2,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a67eeb275837b64ead2bfa8e5f03a48,ntxv:0,isnm:False|UVIN-190-UVOUT;n:type:ShaderForge.SFN_Panner,id:190,x:31674,y:32286,varname:node_190,prsc:2,spu:-0.03,spv:0.2|UVIN-7579-OUT;n:type:ShaderForge.SFN_Multiply,id:1737,x:32184,y:32237,varname:node_1737,prsc:2|A-6074-R,B-7214-R;n:type:ShaderForge.SFN_Add,id:6677,x:33056,y:32357,varname:node_6677,prsc:2|A-2276-OUT,B-9311-OUT,C-6293-OUT;n:type:ShaderForge.SFN_Lerp,id:4800,x:31869,y:33198,varname:node_4800,prsc:2|A-797-RGB,B-5268-RGB,T-4223-V;n:type:ShaderForge.SFN_Color,id:5268,x:31527,y:33072,ptovrint:False,ptlb:node_5268,ptin:_node_5268,varname:node_5268,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9513561,c2:1,c3:0.7877358,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7749,x:32300,y:33228,ptovrint:False,ptlb:node_7749,ptin:_node_7749,varname:node_7749,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7618c65f1c809f54cad819f191c12827,ntxv:0,isnm:False|UVIN-1123-OUT;n:type:ShaderForge.SFN_Tex2d,id:158,x:32300,y:33422,ptovrint:False,ptlb:backMask,ptin:_backMask,varname:node_158,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7a9a733a3d5a59847b929b24949c0d70,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:5077,x:32588,y:33320,varname:node_5077,prsc:2|A-7749-R,B-158-R;n:type:ShaderForge.SFN_Tex2d,id:9508,x:31856,y:33434,ptovrint:False,ptlb:distort,ptin:_distort,varname:node_9508,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8060f12be6af7994db7ddba6bd3a3219,ntxv:0,isnm:False|UVIN-2338-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8687,x:31782,y:33619,ptovrint:False,ptlb:distort2,ptin:_distort2,varname:_node_9508_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8060f12be6af7994db7ddba6bd3a3219,ntxv:0,isnm:False|UVIN-7172-UVOUT;n:type:ShaderForge.SFN_Panner,id:2338,x:31563,y:33417,varname:node_2338,prsc:2,spu:-0.14,spv:0|UVIN-4223-UVOUT;n:type:ShaderForge.SFN_Panner,id:7172,x:31587,y:33651,varname:node_7172,prsc:2,spu:-0.3,spv:0.1|UVIN-4223-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4451,x:32039,y:33554,varname:node_4451,prsc:2|A-9508-G,B-8687-R,C-1510-OUT;n:type:ShaderForge.SFN_Vector1,id:1510,x:31731,y:33875,varname:node_1510,prsc:2,v1:0.02;n:type:ShaderForge.SFN_Add,id:1123,x:32080,y:33326,varname:node_1123,prsc:2|A-4223-UVOUT,B-4451-OUT;n:type:ShaderForge.SFN_Vector1,id:1451,x:30986,y:32166,varname:node_1451,prsc:2,v1:6;n:type:ShaderForge.SFN_Multiply,id:7579,x:31508,y:32269,varname:node_7579,prsc:2|A-4223-UVOUT,B-436-OUT;n:type:ShaderForge.SFN_Append,id:436,x:31273,y:32269,varname:node_436,prsc:2|A-662-OUT,B-2131-OUT;n:type:ShaderForge.SFN_Vector1,id:662,x:30986,y:32269,varname:node_662,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:2131,x:31019,y:32437,varname:node_2131,prsc:2,v1:7;n:type:ShaderForge.SFN_Tex2d,id:8584,x:31919,y:32514,ptovrint:False,ptlb:bottom wave,ptin:_bottomwave,varname:_wave_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a67eeb275837b64ead2bfa8e5f03a48,ntxv:0,isnm:False|UVIN-6159-UVOUT;n:type:ShaderForge.SFN_Panner,id:6159,x:31698,y:32483,varname:node_6159,prsc:2,spu:-0.2,spv:0.3|UVIN-1283-OUT;n:type:ShaderForge.SFN_Tex2d,id:2417,x:31790,y:32720,ptovrint:False,ptlb:bottom wave2,ptin:_bottomwave2,varname:_wave3,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a67eeb275837b64ead2bfa8e5f03a48,ntxv:0,isnm:False|UVIN-5396-UVOUT;n:type:ShaderForge.SFN_Panner,id:5396,x:31669,y:32709,varname:node_5396,prsc:2,spu:0,spv:0.3|UVIN-66-OUT;n:type:ShaderForge.SFN_Multiply,id:1283,x:31503,y:32483,varname:node_1283,prsc:2|A-4223-UVOUT,B-1807-OUT;n:type:ShaderForge.SFN_Vector1,id:8579,x:30963,y:32536,varname:node_8579,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:66,x:31503,y:32692,varname:node_66,prsc:2|A-4223-UVOUT,B-9029-OUT;n:type:ShaderForge.SFN_Append,id:9029,x:31319,y:32721,varname:node_9029,prsc:2|A-9562-OUT,B-5628-OUT;n:type:ShaderForge.SFN_Vector1,id:9562,x:31032,y:32727,varname:node_9562,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:5628,x:31014,y:32860,varname:node_5628,prsc:2,v1:5;n:type:ShaderForge.SFN_Vector1,id:6199,x:31914,y:32845,varname:node_6199,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5139,x:32089,y:32562,varname:node_5139,prsc:2|A-8584-R,B-2417-B,C-6199-OUT;n:type:ShaderForge.SFN_Subtract,id:9311,x:32739,y:32537,varname:node_9311,prsc:2|A-4800-OUT,B-1781-OUT;n:type:ShaderForge.SFN_Clamp01,id:9108,x:31527,y:32668,varname:node_9108,prsc:2|IN-5139-OUT;n:type:ShaderForge.SFN_Multiply,id:1781,x:32413,y:32562,varname:node_1781,prsc:2|A-9108-OUT,B-1609-OUT;n:type:ShaderForge.SFN_Vector1,id:1609,x:32180,y:32721,varname:node_1609,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Append,id:9548,x:31239,y:32111,varname:node_9548,prsc:2|A-8331-OUT,B-1451-OUT;n:type:ShaderForge.SFN_Vector1,id:8331,x:31032,y:32111,varname:node_8331,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7068,x:31315,y:31965,varname:node_7068,prsc:2|A-4223-UVOUT,B-9548-OUT;n:type:ShaderForge.SFN_Append,id:1807,x:31302,y:32540,varname:node_1807,prsc:2|A-8579-OUT,B-9872-OUT;n:type:ShaderForge.SFN_Vector1,id:9872,x:30982,y:32615,varname:node_9872,prsc:2,v1:3;n:type:ShaderForge.SFN_Power,id:2519,x:32475,y:32218,varname:node_2519,prsc:2|VAL-1737-OUT,EXP-3684-OUT;n:type:ShaderForge.SFN_Vector1,id:3684,x:32162,y:32414,varname:node_3684,prsc:2,v1:14;n:type:ShaderForge.SFN_Multiply,id:6907,x:32808,y:33049,varname:node_6907,prsc:2|A-9104-OUT,B-8528-OUT,C-4632-OUT;n:type:ShaderForge.SFN_Vector1,id:4632,x:32324,y:33111,varname:node_4632,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Add,id:9104,x:32296,y:32954,varname:node_9104,prsc:2|A-4223-V,B-2114-OUT;n:type:ShaderForge.SFN_Slider,id:2114,x:31835,y:33036,ptovrint:False,ptlb:opa_front,ptin:_opa_front,varname:node_2114,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.370606,max:1;n:type:ShaderForge.SFN_Multiply,id:2276,x:32812,y:32236,varname:node_2276,prsc:2|A-245-OUT,B-4364-R;n:type:ShaderForge.SFN_Add,id:8201,x:33030,y:32951,varname:node_8201,prsc:2|A-2276-OUT,B-6907-OUT,C-6293-OUT;n:type:ShaderForge.SFN_Multiply,id:8783,x:33179,y:32964,varname:node_8783,prsc:2|A-8201-OUT,B-8528-OUT;n:type:ShaderForge.SFN_Clamp01,id:597,x:33219,y:32342,varname:node_597,prsc:2|IN-6677-OUT;n:type:ShaderForge.SFN_Clamp01,id:950,x:33335,y:32952,varname:node_950,prsc:2|IN-8783-OUT;n:type:ShaderForge.SFN_Clamp01,id:8528,x:32786,y:33320,varname:node_8528,prsc:2|IN-5077-OUT;n:type:ShaderForge.SFN_Tex2d,id:4364,x:32571,y:32379,ptovrint:False,ptlb:node_4364,ptin:_node_4364,varname:node_4364,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a30da4b1458e7f4ba61808748e66a53,ntxv:0,isnm:False|UVIN-4127-OUT;n:type:ShaderForge.SFN_Clamp01,id:245,x:32592,y:32186,varname:node_245,prsc:2|IN-2519-OUT;n:type:ShaderForge.SFN_Multiply,id:4127,x:32355,y:32379,varname:node_4127,prsc:2|A-4223-UVOUT,B-9963-OUT;n:type:ShaderForge.SFN_Vector1,id:9963,x:32137,y:32507,varname:node_9963,prsc:2,v1:1.7;n:type:ShaderForge.SFN_Tex2d,id:4257,x:32580,y:32677,ptovrint:False,ptlb:node_4257,ptin:_node_4257,varname:node_4257,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1123-OUT;n:type:ShaderForge.SFN_Multiply,id:6293,x:32853,y:32693,varname:node_6293,prsc:2|A-1737-OUT,B-4257-R,C-5237-OUT;n:type:ShaderForge.SFN_Vector1,id:5237,x:32641,y:32888,varname:node_5237,prsc:2,v1:2;proporder:6074-797-7214-5268-7749-158-9508-8687-8584-2417-2114-4364-4257;pass:END;sub:END;*/

Shader "Shader Forge/Water" {
    Properties {
        _wave ("wave", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5176471,0.6117647,0.4,1)
        _wave2 ("wave2", 2D) = "white" {}
        _node_5268 ("node_5268", Color) = (0.9513561,1,0.7877358,1)
        _node_7749 ("node_7749", 2D) = "white" {}
        _backMask ("backMask", 2D) = "white" {}
        _distort ("distort", 2D) = "white" {}
        _distort2 ("distort2", 2D) = "white" {}
        _bottomwave ("bottom wave", 2D) = "white" {}
        _bottomwave2 ("bottom wave2", 2D) = "white" {}
        _opa_front ("opa_front", Range(0, 1)) = 0.370606
        _node_4364 ("node_4364", 2D) = "white" {}
        _node_4257 ("node_4257", 2D) = "white" {}
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _wave; uniform float4 _wave_ST;
            uniform float4 _TintColor;
            uniform sampler2D _wave2; uniform float4 _wave2_ST;
            uniform float4 _node_5268;
            uniform sampler2D _node_7749; uniform float4 _node_7749_ST;
            uniform sampler2D _backMask; uniform float4 _backMask_ST;
            uniform sampler2D _distort; uniform float4 _distort_ST;
            uniform sampler2D _distort2; uniform float4 _distort2_ST;
            uniform sampler2D _bottomwave; uniform float4 _bottomwave_ST;
            uniform sampler2D _bottomwave2; uniform float4 _bottomwave2_ST;
            uniform float _opa_front;
            uniform sampler2D _node_4364; uniform float4 _node_4364_ST;
            uniform sampler2D _node_4257; uniform float4 _node_4257_ST;
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
                float4 node_2048 = _Time;
                float2 node_8643 = ((i.uv0*float2(0.5,6.0))+node_2048.g*float2(-0.01,0));
                float4 _wave_var = tex2D(_wave,TRANSFORM_TEX(node_8643, _wave));
                float2 node_190 = ((i.uv0*float2(0.5,7.0))+node_2048.g*float2(-0.03,0.2));
                float4 _wave2_var = tex2D(_wave2,TRANSFORM_TEX(node_190, _wave2));
                float node_1737 = (_wave_var.r*_wave2_var.r);
                float2 node_4127 = (i.uv0*1.7);
                float4 _node_4364_var = tex2D(_node_4364,TRANSFORM_TEX(node_4127, _node_4364));
                float node_2276 = (saturate(pow(node_1737,14.0))*_node_4364_var.r);
                float2 node_6159 = ((i.uv0*float2(0.5,3.0))+node_2048.g*float2(-0.2,0.3));
                float4 _bottomwave_var = tex2D(_bottomwave,TRANSFORM_TEX(node_6159, _bottomwave));
                float2 node_5396 = ((i.uv0*float2(0.5,5.0))+node_2048.g*float2(0,0.3));
                float4 _bottomwave2_var = tex2D(_bottomwave2,TRANSFORM_TEX(node_5396, _bottomwave2));
                float2 node_2338 = (i.uv0+node_2048.g*float2(-0.14,0));
                float4 _distort_var = tex2D(_distort,TRANSFORM_TEX(node_2338, _distort));
                float2 node_7172 = (i.uv0+node_2048.g*float2(-0.3,0.1));
                float4 _distort2_var = tex2D(_distort2,TRANSFORM_TEX(node_7172, _distort2));
                float2 node_1123 = (i.uv0+(_distort_var.g*_distort2_var.r*0.02));
                float4 _node_4257_var = tex2D(_node_4257,TRANSFORM_TEX(node_1123, _node_4257));
                float node_6293 = (node_1737*_node_4257_var.r*2.0);
                float3 emissive = saturate((node_2276+(lerp(_TintColor.rgb,_node_5268.rgb,i.uv0.g)-(saturate((_bottomwave_var.r*_bottomwave2_var.b*1.0))*0.2))+node_6293));
                float3 finalColor = emissive;
                float4 _node_7749_var = tex2D(_node_7749,TRANSFORM_TEX(node_1123, _node_7749));
                float4 _backMask_var = tex2D(_backMask,TRANSFORM_TEX(i.uv0, _backMask));
                float node_8528 = saturate((_node_7749_var.r+_backMask_var.r));
                fixed4 finalRGBA = fixed4(finalColor,saturate(((node_2276+((i.uv0.g+_opa_front)*node_8528*1.5)+node_6293)*node_8528)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
