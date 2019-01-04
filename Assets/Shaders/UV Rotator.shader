// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "UV Rotate"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
		_Diffuse("Diffuse", 2D) = "white" {}
		_Speed("Speed", Range( -5 , 5)) = 1
		_Power("Power", Range( 0 , 5)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 2.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa 
		struct Input
		{
			half2 uv_texcoord;
		};

		uniform sampler2D _Diffuse;
		uniform half _Speed;
		uniform half _Power;
		uniform half4 _Color;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float mulTime9 = _Time.y * _Speed;
			float cos7 = cos( mulTime9 );
			float sin7 = sin( mulTime9 );
			float2 rotator7 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos7 , -sin7 , sin7 , cos7 )) + float2( 0.5,0.5 );
			half4 tex2DNode3 = tex2D( _Diffuse, rotator7 );
			o.Emission = ( ( ( tex2DNode3 * _Power ) + tex2DNode3 ) * _Color ).rgb;
			o.Alpha = ( tex2DNode3.a * _Color.a );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;49;1920;1031;1326.175;821.2797;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;13;-1559.791,-235.9487;Half;False;Property;_Speed;Speed;2;0;Create;True;0;0;False;0;1;3.57;-5;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;9;-1260.791,-283.9487;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1286.791,-420.9487;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;7;-1025.791,-297.9487;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;3;-786,-326;Float;True;Property;_Diffuse;Diffuse;1;0;Create;True;0;0;False;0;302951faffe230848aa0d3df7bb70faa;f395b545ff3134537a5fe7224184785a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-733.175,-453.2797;Float;False;Property;_Power;Power;3;0;Create;True;0;0;False;0;0;4.35;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-373.175,-525.2797;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-309.175,-336.2797;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;-727.0085,-131.0084;Half;False;Property;_Color;Color;0;0;Create;True;0;0;False;0;1,1,1,0;0,0.2,0.4,0.4509804;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-112.0085,-302.0084;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-419.0085,-122.0084;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;17;167,-347;Half;False;True;0;Half;ASEMaterialInspector;0;0;Unlit;UV Rotate;False;False;False;False;True;True;True;True;True;True;True;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;13;0
WireConnection;7;0;8;0
WireConnection;7;2;9;0
WireConnection;3;1;7;0
WireConnection;21;0;3;0
WireConnection;21;1;20;0
WireConnection;19;0;21;0
WireConnection;19;1;3;0
WireConnection;15;0;19;0
WireConnection;15;1;16;0
WireConnection;18;0;3;4
WireConnection;18;1;16;4
WireConnection;17;2;15;0
WireConnection;17;9;18;0
ASEEND*/
//CHKSM=29384924276C1B4BD9E79B68D2FB6EA2E96DC3A2