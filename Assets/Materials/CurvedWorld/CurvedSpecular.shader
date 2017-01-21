// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/CurvedWorldSpec" {
    Properties {
        // Diffuse texture
        _Color ("Main Color", Color) = (1,1,1,1)
        _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
        _SpecMap ("Specular Level (R) Specular Gloss (G)", 2D) = "grey" {} // Add a slot for the new specular texture to the properties - specular value in the red channel, gloss value in the green channel.
        _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
        _MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        // Degree of curvature
        _Curvature ("Curl Over X", Float) = 0.001
        _Curvature2 ("Twist Over Z", Float) = 0.001
        _Curvature3 ("Curve Over Y", Float) = 0.001
        _Curvature4 ("Scale Width", Float) = 0.001
        _Curvature5 ("Sin", Float) = 0.001
    }
    SubShader {
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        LOD 400
        Cull Off
        
        CGPROGRAM
        // Surface shader function is called surf, and vertex preprocessor function is called vert
        // addshadow used to add shadow collector and caster passes following vertex modification
        #pragma surface surf Lambert vertex:vert addshadow
        #pragma exclude_renderers flash
 
        // Access the shaderlab properties
        uniform sampler2D _MainTex;
        uniform float _Curvature;
        uniform float _Curvature2;
        uniform float _Curvature3;
        uniform float _Curvature4;
        uniform float _Curvature5;
        sampler2D _BumpMap;
        fixed4 _Color;
        half _Shininess;
        sampler2D _SpecMap; // New specular texture declaration so we can sample it.
 
        // Basic input structure to the shader function
        // requires only a single set of UV texture mapping coordinates
        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };
 
        // This is where the curvature is applied
        void vert( inout appdata_full v)
        {
            // Transform the vertex coordinates from model space into world space
            float4 vv = mul( unity_ObjectToWorld, v.vertex );
 
            // Now adjust the coordinates to be relative to the camera position
            vv.xyz -= _WorldSpaceCameraPos.xyz;
 
            // Reduce the y coordinate (i.e. lower the "height") of each vertex based
            // on the square of the distance from the camera in the z axis, multiplied
            // by the chosen curvature factor

            float4 combined = float4( 0.0f, (vv.z * vv.z) * - _Curvature, 0.0f, 0.0f );
            combined += float4( 0.0f,                          (vv.z * vv.x) * - _Curvature2, 0.0f, 0.0f );
            combined += float4( (vv.z * vv.z) * - _Curvature3, 0.0f,                          0.0f, 0.0f );
            combined += float4( (vv.z * vv.x) * - _Curvature4, 0.0f,                          0.0f, 0.0f );

            float wave = sin(vv.z / (1000 * _Curvature5)) * 3;
            combined += float4( 0.0f,     					   wave,                          0.0f, 0.0f );

            vv = combined;
 
            // Now apply the offset back to the vertices in model space
            v.vertex += mul(unity_WorldToObject, vv);
        }
 
        // This is just a default surface shader
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb * _Color.rgb;
            fixed2 customSpec = tex2D(_SpecMap, IN.uv_MainTex).rg; // Sample the new spec texture, reusing the diffuse map's UVs.
            // I realise these next two are assigned what looks like the wrong way round (i.e. spec goes to gloss and gloss goes to to spec)...
            // ...but the lighting function gets them mixed up so we have to do this here to get the correct result.
            o.Gloss = customSpec.r; // Assign the specular value.
            o.Specular = customSpec.g; // Assign the gloss value.
            o.Alpha = tex.a * _Color.a;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
        }
        ENDCG
    }
    // FallBack "Diffuse"
}