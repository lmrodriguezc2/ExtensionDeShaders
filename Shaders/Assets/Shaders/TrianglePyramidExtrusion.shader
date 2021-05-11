Shader "Geometry/TrianglePyramidExtrusion"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ExtrusionFactor("Extrusion factor", float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Cull Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2g
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct g2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ExtrusionFactor;

			v2g vert(appdata v)
			{
				v2g o;
				o.vertex = v.vertex;
				o.uv = v.uv;
				o.normal = v.normal;
				return o;
			}

			float random(float2 uv)
			{
				return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
			}

			[maxvertexcount(12)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
			{
				g2f o;

				float4 barycenter = (IN[0].vertex + IN[1].vertex + IN[2].vertex) / 3;
				float3 normal = (IN[0].normal + IN[1].normal + IN[2].normal) / 3;

				for (int i = 0; i < 3; i++) {
					int next = (i + 1) % 3;

					o.vertex = UnityObjectToClipPos(IN[i].vertex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					o.uv = TRANSFORM_TEX(IN[i].uv, _MainTex);
					o.color = fixed4(0.0, 0.0, 0.0, 1.0);
					triStream.Append(o);

					o.vertex = UnityObjectToClipPos(barycenter + float4(normal, 0.0) * _ExtrusionFactor);
					UNITY_TRANSFER_FOG(o,o.vertex);
					o.uv = TRANSFORM_TEX(IN[i].uv, _MainTex);
					float rnd1 = random(float2(o.vertex[0], o.vertex[1]));
					float rnd2 = random(float2(o.vertex[1], o.vertex[2]));
					float rnd3 = random(float2(o.vertex[2], o.vertex[0]));
					o.color = float4(rnd1, rnd2, rnd3, 1.0);
					triStream.Append(o);

					o.vertex = UnityObjectToClipPos(IN[next].vertex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					o.uv = TRANSFORM_TEX(IN[next].uv, _MainTex);
					o.color = fixed4(0.0, 0.0, 0.0, 1.0);
					triStream.Append(o);

					triStream.RestartStrip();
				}

				for (int j = 0; j < 3; j++)
				{
					o.vertex = UnityObjectToClipPos(IN[j].vertex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					o.uv = TRANSFORM_TEX(IN[j].uv, _MainTex);
					o.color = fixed4(0.0, 0.0, 0.0, 1.0);
					triStream.Append(o);
				}

				triStream.RestartStrip();
			}

			fixed4 frag(g2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * i.color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}

			ENDCG
		}
	}
}
