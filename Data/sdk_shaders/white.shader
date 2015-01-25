VortexShaderVersion 2

Name "Basic"

#BeginPass

	ZWrite True

	#BeginVertex

		#version 120

		uniform mat4 _modelViewProjection;

		void main()
		{
		    gl_Position = _modelViewProjection * gl_Vertex;

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