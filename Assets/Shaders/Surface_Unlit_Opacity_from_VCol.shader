// Upgrade NOTE: upgraded instancing buffer 'SurfaceUnlitOpacityfromVCol' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Surface Unlit (Opacity from VCol)"
{
	Properties
	{
		[PerRendererData]_Color("Color", Color) = (1,1,1,0)
		[PerRendererData]_Opacity("Opacity", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 2.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		UNITY_INSTANCING_BUFFER_START(SurfaceUnlitOpacityfromVCol)
			UNITY_DEFINE_INSTANCED_PROP(half4, _Color)
#define _Color_arr SurfaceUnlitOpacityfromVCol
			UNITY_DEFINE_INSTANCED_PROP(half, _Opacity)
#define _Opacity_arr SurfaceUnlitOpacityfromVCol
		UNITY_INSTANCING_BUFFER_END(SurfaceUnlitOpacityfromVCol)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half4 _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			o.Emission = _Color_Instance.rgb;
			half _Opacity_Instance = UNITY_ACCESS_INSTANCED_PROP(_Opacity_arr, _Opacity);
			o.Alpha = ( _Opacity_Instance * i.vertexColor ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;689;983;351;829.4786;310.8188;1;True;False
Node;AmplifyShaderEditor.VertexColorNode;3;-471.9617,-5.266041;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-558.5444,-90.37543;Half;False;InstancedProperty;_Opacity;Opacity;1;1;[PerRendererData];Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-260.5445,-93.37543;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;-489.006,-255.8629;Half;False;InstancedProperty;_Color;Color;0;1;[PerRendererData];Create;True;0;0;False;0;1,1,1,0;0.9150943,0.1467604,0.1467604,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;-98.73435,-280.5939;Half;False;True;0;Half;ASEMaterialInspector;0;0;Unlit;Surface Unlit (Opacity from VCol);False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;9;0
WireConnection;8;1;3;0
WireConnection;24;2;11;0
WireConnection;24;9;8;0
ASEEND*/
//CHKSM=5B6231D4AEC56C38132145DDB140A06972D4AF9F