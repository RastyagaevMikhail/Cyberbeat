// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Vertex Color Intesity"
{
	Properties
	{
		_Color("Color", Color) = (1,0,0,0)
		_VColor1Power("V Color 1 Power", Range( 0 , 1)) = 0
		_VColor2Power("V Color 2 Power", Range( 0 , 1)) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 2.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGB
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_color : COLOR;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_color : COLOR;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform half _VColor1Power;
			uniform half4 _Color;
			uniform half _VColor2Power;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_color = v.ase_color;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 lerpResult5 = lerp( ( _VColor1Power * _Color ) , ( _Color * _VColor2Power ) , i.ase_color);
				
				
				finalColor = lerpResult5;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16100
0;49;1920;1031;1399;460.5;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;10;-794,-22.5;Half;False;Property;_VColor1Power;V Color 1 Power;1;0;Create;True;0;0;False;0;0;0.321;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-791,80.5;Half;False;Property;_VColor2Power;V Color 2 Power;2;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-739,165.5;Half;False;Property;_Color;Color;0;0;Create;True;0;0;False;0;1,0,0,0;0,1,0.1354198,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-429,87.5;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-490,-3.5;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;9;-632,-217.5;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;5;-275,-23.5;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;14;-9,-31;Float;False;True;2;Float;ASEMaterialInspector;0;1;Vertex Color Intesity;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;0;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;13;0;8;0
WireConnection;13;1;11;0
WireConnection;12;0;10;0
WireConnection;12;1;8;0
WireConnection;5;0;12;0
WireConnection;5;1;13;0
WireConnection;5;2;9;0
WireConnection;14;0;5;0
ASEEND*/
//CHKSM=71BB4EA5AC7F63FE2030E74A1E941D5423185F1C