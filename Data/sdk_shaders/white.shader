VortexShaderVersion 2

Name "Basic"

#BeginPass

	ZWrite True

	#BeginVertex

		#version 120

		uniform mat4 worldViewProjMat;
		uniform mat4 viewMat;

		void main()
		{
		    gl_Position = worldViewProjMat * gl_Vertex;

		    gl_FrontColor = vec4(1, 1, 1, 1);
		}


	#EndVertex

	#BeginFragment

		#version 120

		void main()
		{
		    gl_FragColor = gl_Color;

		    // testing
		}


	#EndFragment

#EndPass