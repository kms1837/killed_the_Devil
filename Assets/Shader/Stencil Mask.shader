﻿Shader "Custom/Stencil Mask" {
	Properties
    {
        [PerRendererData] _MainTex  ("Sprite Texture", 2D) = "white" {}
        [MaterialToggle]  PixelSnap ("Pixel snap", Float)  = 0
        _Color ("Tint", Color) = (255, 255, 255, 255)
    }
 
    SubShader
    {
        Tags
        {
            "Queue"				= "Transparent"
            "RenderType"		= "Transparent"
            "IgnoreProjector"	= "True"
            "PreviewType"		= "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        //
        ColorMask RGB
        AlphaTest Greater 0.5
        //
 
        Cull 		Off
        Lighting 	Off
        ZWrite 		Off
        Fog { Mode Off }
        
        Blend SrcAlpha OneMinusSrcAlpha
        //Blend One OneMinusSrcAlpha
 
        Pass
        {
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }
     
	        CGPROGRAM
	            #pragma vertex vert
	            #pragma fragment frag
	            #pragma multi_compile DUMMY PIXELSNAP_ON
	            #include "UnityCG.cginc"
	     
	            struct appdata_t
	            {
	                float4 vertex   : POSITION;
	                float4 color    : COLOR;
	                float2 texcoord : TEXCOORD0;
	            };
	 
	            struct v2f
	            {
	                float4 vertex   : SV_POSITION;
	                fixed4 color    : COLOR;
	                half2 texcoord  : TEXCOORD0;
	            };
	            
	            sampler2D _MainTex;
	            fixed4 _Color;
	 
	            v2f vert(appdata_t IN)
	            {
	                v2f OUT;
	                OUT.vertex 	 = mul(UNITY_MATRIX_MVP, IN.vertex);
	                OUT.texcoord = IN.texcoord;
	                OUT.color 	 = IN.color * _Color;
	                
	                #ifdef PIXELSNAP_ON
	                OUT.vertex 	 = UnityPixelSnap (OUT.vertex);
	                #endif
	 
	                return OUT;
	            }
	 
	            fixed4 frag(v2f IN) : SV_Target
	            {
	                fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
	                if (c.a<0.1) discard;            //Most IMPORTANT working Code
	                c.rgb *= c.a;
	                return c;
	            }
	        ENDCG
        }
    }
}
