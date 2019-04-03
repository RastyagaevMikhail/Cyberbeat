// Upgrade NOTE: upgraded instancing buffer 'VertexColorIntesity' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Vertex Color Intesity"
{
	Properties
	{
		[PerRendererData]_Color("Color", Color) = (1,0,0,0)
		_RPower("R Power", Range( 0 , 1)) = 0.25
		_GPower("G Power", Range( 0 , 1)) = 0.5
		_BPower("B Power", Range( 0 , 1)) = 0.75
		_OutlinePower("Outline Power", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 2.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform half _OutlinePower;
		uniform half _RPower;
		uniform half _GPower;
		uniform half _BPower;

		UNITY_INSTANCING_BUFFER_START(VertexColorIntesity)
			UNITY_DEFINE_INSTANCED_PROP(half4, _Color)
#define _Color_arr VertexColorIntesity
		UNITY_INSTANCING_BUFFER_END(VertexColorIntesity)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half4 _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			float4 lerpResult5 = lerp( ( _OutlinePower * _Color_Instance ) , ( _Color_Instance * _RPower ) , i.vertexColor.r);
			float4 lerpResult18 = lerp( lerpResult5 , ( _Color_Instance * _GPower ) , i.vertexColor.g);
			float4 lerpResult21 = lerp( lerpResult18 , ( _Color_Instance * _BPower ) , i.vertexColor.b);
			o.Emission = lerpResult21.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;689;983;351;1529.545;506.5236;1.6;True;False
Node;AmplifyShaderEditor.RangedFloatNode;10;-796.0612,161.3623;Half;False;Property;_OutlinePower;Outline Power;4;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-802,-124.5;Half;False;Property;_RPower;R Power;1;0;Create;True;0;0;False;0;0.25;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;-743.1797,-298.3606;Half;False;InstancedProperty;_Color;Color;0;1;[PerRendererData];Create;True;0;0;False;0;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;9;-682,-542.5;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-797.1797,-25.9606;Half;False;Property;_GPower;G Power;2;0;Create;True;0;0;False;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-451,50.5;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-450,-197.5;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-792.1797,59.0394;Half;False;Property;_BPower;B Power;3;0;Create;True;0;0;False;0;0.75;0.274;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-455.1797,-68.96063;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;5;-282,-308.5;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;18;4.820313,-287.9606;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-459.1797,164.0394;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;21;256.4203,-241.5606;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;38;486.9815,-293.6534;Half;False;True;0;Half;ASEMaterialInspector;0;0;Unlit;Vertex Color Intesity;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;10;0
WireConnection;12;1;24;0
WireConnection;13;0;24;0
WireConnection;13;1;11;0
WireConnection;19;0;24;0
WireConnection;19;1;20;0
WireConnection;5;0;12;0
WireConnection;5;1;13;0
WireConnection;5;2;9;1
WireConnection;18;0;5;0
WireConnection;18;1;19;0
WireConnection;18;2;9;2
WireConnection;22;0;24;0
WireConnection;22;1;23;0
WireConnection;21;0;18;0
WireConnection;21;1;22;0
WireConnection;21;2;9;3
WireConnection;38;2;21;0
ASEEND*/
//CHKSM=64DEB1C8B7988A37B4A7539D12CF8293E6614352