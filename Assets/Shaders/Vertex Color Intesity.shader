// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Vertex Color Intesity"
{
	Properties
	{
		_RColor("R Color", Color) = (1,0,0,0)
		_RPower("R Power", Range( 0 , 1)) = 0.25
		_GColor("G Color", Color) = (1,0,0,0)
		_GPower("G Power", Range( 0 , 1)) = 0.5
		_BColor("B Color", Color) = (1,0,0,0)
		_BPower("B Power", Range( 0 , 1)) = 0.75
		_OutlineColor("Outline Color", Color) = (1,0,0,0)
		_OutlinePower("Outline Power", Range( 0 , 1)) = 1
		_Cubemap("Cubemap", CUBE) = "white" {}
		_SkyboxPower("Skybox Power", Range( 0 , 2)) = 0
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
				half4 ase_color : COLOR;
				half3 ase_normal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform half _OutlinePower;
			uniform half4 _OutlineColor;
			uniform half4 _RColor;
			uniform half _RPower;
			uniform half4 _GColor;
			uniform half _GPower;
			uniform half4 _BColor;
			uniform half _BPower;
			uniform samplerCUBE _Cubemap;
			uniform half _SkyboxPower;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord.xyz = ase_worldNormal;
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord1.xyz = ase_worldPos;
				
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 lerpResult5 = lerp( ( _OutlinePower * _OutlineColor ) , ( _RColor * _RPower ) , i.ase_color.r);
				float4 lerpResult18 = lerp( lerpResult5 , ( _GColor * _GPower ) , i.ase_color.g);
				float4 lerpResult21 = lerp( lerpResult18 , ( _BColor * _BPower ) , i.ase_color.b);
				float3 ase_worldNormal = i.ase_texcoord.xyz;
				float3 ase_worldPos = i.ase_texcoord1.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldReflection = reflect(-ase_worldViewDir, ase_worldNormal);
				
				
				finalColor = ( lerpResult21 + ( texCUBE( _Cubemap, ase_worldReflection ) * _SkyboxPower ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16100
0;790;1920;1058;653.1797;406.9606;1;True;False
Node;AmplifyShaderEditor.ColorNode;8;-722,471.5;Half;False;Property;_OutlineColor;Outline Color;6;0;Create;True;0;0;False;0;1,0,0,0;0,1,0.1354198,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;24;-743.1797,-298.9606;Half;False;Property;_RColor;R Color;0;0;Create;True;0;0;False;0;1,0,0,0;0,1,0.1354198,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-785,393.5;Half;False;Property;_OutlinePower;Outline Power;7;0;Create;True;0;0;False;0;1;0.321;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-781,-373.5;Half;False;Property;_RPower;R Power;1;0;Create;True;0;0;False;0;0.25;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-444,139.5;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;9;-682,-542.5;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-442,-150.5;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-789.1797,-137.9606;Half;False;Property;_GPower;G Power;3;0;Create;True;0;0;False;0;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;26;-735.1797,-51.96063;Half;False;Property;_GColor;G Color;2;0;Create;True;0;0;False;0;1,0,0,0;0,1,0.1354198,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-789.1797,124.0394;Half;False;Property;_BPower;B Power;5;0;Create;True;0;0;False;0;0.75;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;25;-725.1797,219.0394;Half;False;Property;_BColor;B Color;4;0;Create;True;0;0;False;0;1,0,0,0;0,1,0.1354198,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;5;-293,-357.5;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-449.1797,10.03937;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldReflectionVector;32;121.8203,101.0394;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;18;4.820313,-287.9606;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-440.1797,262.0394;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;29;343.8203,33.03937;Float;True;Property;_Cubemap;Cubemap;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;LockedToCube;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;359.8203,225.0394;Float;False;Property;_SkyboxPower;Skybox Power;9;0;Create;True;0;0;False;0;0;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;21;342.8203,-199.9606;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;679.8203,19.03937;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;715.8203,-143.9606;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;14;894,-191;Half;False;True;2;Half;ASEMaterialInspector;0;1;Vertex Color Intesity;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;0;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;12;0;10;0
WireConnection;12;1;8;0
WireConnection;13;0;24;0
WireConnection;13;1;11;0
WireConnection;5;0;12;0
WireConnection;5;1;13;0
WireConnection;5;2;9;1
WireConnection;19;0;26;0
WireConnection;19;1;20;0
WireConnection;18;0;5;0
WireConnection;18;1;19;0
WireConnection;18;2;9;2
WireConnection;22;0;25;0
WireConnection;22;1;23;0
WireConnection;29;1;32;0
WireConnection;21;0;18;0
WireConnection;21;1;22;0
WireConnection;21;2;9;3
WireConnection;33;0;29;0
WireConnection;33;1;34;0
WireConnection;30;0;21;0
WireConnection;30;1;33;0
WireConnection;14;0;30;0
ASEEND*/
//CHKSM=C25DBA16D3DC67F74BE35D2CEF6B92561192F31F