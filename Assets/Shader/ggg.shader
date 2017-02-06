// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:10,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1461,x:34475,y:33035,varname:node_1461,prsc:2|emission-2113-OUT,alpha-8482-OUT;n:type:ShaderForge.SFN_Tex2d,id:2076,x:33417,y:32846,ptovrint:False,ptlb:node_2076,ptin:_node_2076,varname:node_2076,prsc:2,tex:ed427824c6ff5bd48a06684e5ba04076,ntxv:0,isnm:False|UVIN-5113-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1362,x:33722,y:32845,varname:node_1362,prsc:2|A-2076-RGB,B-9222-RGB;n:type:ShaderForge.SFN_Color,id:1340,x:33811,y:33065,ptovrint:False,ptlb:node_1340,ptin:_node_1340,varname:node_1340,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_VertexColor,id:9222,x:33283,y:33214,varname:node_9222,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4027,x:34016,y:33045,varname:node_4027,prsc:2|A-1362-OUT,B-1340-RGB;n:type:ShaderForge.SFN_Multiply,id:8482,x:33969,y:33329,varname:node_8482,prsc:2|A-2076-A,B-9222-A,C-1340-A;n:type:ShaderForge.SFN_Multiply,id:2113,x:34231,y:33025,varname:node_2113,prsc:2|A-1540-OUT,B-4027-OUT,C-8482-OUT;n:type:ShaderForge.SFN_Slider,id:1540,x:33834,y:32729,ptovrint:False,ptlb:node_1540,ptin:_node_1540,varname:node_1540,prsc:2,min:0,cur:0.9154898,max:5;n:type:ShaderForge.SFN_TexCoord,id:5113,x:33144,y:32853,varname:node_5113,prsc:2,uv:0;proporder:2076-1340-1540;pass:END;sub:END;*/

Shader "Shader Forge/ggg" {
    Properties {
        _node_2076 ("node_2076", 2D) = "white" {}
        _node_1340 ("node_1340", Color) = (0.5,0.5,0.5,1)
        _node_1540 ("node_1540", Range(0, 5)) = 0.9154898
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+10"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _node_2076; uniform float4 _node_2076_ST;
            uniform float4 _node_1340;
            uniform float _node_1540;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _node_2076_var = tex2D(_node_2076,TRANSFORM_TEX(i.uv0, _node_2076));
                float node_8482 = (_node_2076_var.a*i.vertexColor.a*_node_1340.a);
                float3 emissive = (_node_1540*((_node_2076_var.rgb*i.vertexColor.rgb)*_node_1340.rgb)*node_8482);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_8482);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
