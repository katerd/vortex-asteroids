VortexShaderVersion 2

Name "Basic 2 Texture"

#BeginPass

	ZWrite True

	#BeginVertex

		#version 120

		uniform mat4 worldViewProjMat;
		uniform mat4 viewMat;

		void main()
		{
		    gl_Position = worldViewProjMat * gl_Vertex;

		    gl_FrontColor = gl_Color;

		    gl_TexCoord[0] = gl_MultiTexCoord0;
		    gl_TexCoord[1] = gl_MultiTexCoord1;
		}

	#EndVertex

	#BeginFragment

		#version 120

		uniform sampler2D tex0;
		uniform sampler2D tex1;

		void main()
		{
		    vec4 tfac0 = texture2D(tex0, gl_TexCoord[0].st) * gl_Color.r;
		    vec4 tfac1 = texture2D(tex1, gl_TexCoord[1].st) * gl_Color.g;

		    gl_FragColor = tfac0 + tfac1;
		}

	#EndFragment

#EndPass