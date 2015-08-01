{
    Name "Standard"

	Defines
	[
		{
			Name "DirectionalLight"
			Text ![
			
				vec3 normal = normalize(mat3(_normalMatrix) * gl_Normal);
			
				float lt = dot(normal, _lightWorldViewVector.xyz);
				lt *= _lightIntensity;
				
				vec3 diffuseRGB = gl_Color.rgb * matDiffuse.rgb * _lightColour.rgb * lt;
				gl_FrontColor.a = matDiffuse.a * gl_Color.a;
			
			
			]!
		}
		
		{
			
			Name "PointLight"
			Text ![
			
				vec3 normal = normalize(mat3(_normalMatrix) * gl_Normal);
				
				vec4 eyeCoords = _modelView * gl_Vertex;
			
				vec3 s = normalize(_lightWorldViewVector.xyz - eyeCoords.xyz);
				float dist = distance(_lightWorldViewVector.xyz, eyeCoords.xyz);
				
				float attenuation = 0;
				attenuation = clamp(1.0 - dist*dist/(_lightRadius*_lightRadius), 0.0, 1.0); 
				attenuation *= attenuation;
				
				float lt = max(dot(s, normal), 0.0);
			
			]!
		}
	]
	
    Passes
    [
        {
            Name "DefaultAmbient_LightPassBase_Medium"

            ZWrite True
            ZTest True
            LightPass Base

            Vertex ![

                #version 120

                uniform mat4 _modelViewProjection;
                uniform mat4 _modelView;
				uniform mat4 _normalMatrix;
				uniform vec3 _world;
                uniform vec4 _lightWorldViewVector;
                uniform vec4 _lightColour;
                uniform float _lightIntensity;
                uniform float _lightRadius;
                uniform vec4 matDiffuse;
                uniform vec4 matAmbient;
                uniform vec4 _lightGlobalAmbient;

                void main()
                {
                    gl_Position = _modelViewProjection * gl_Vertex;

                    vec3 ambientRGB = _lightGlobalAmbient.rgb * matAmbient.rgb;

                    if (_lightRadius < 0)
                    {
                        #DirectionalLight
						gl_FrontColor.rgb = ambientRGB + diffuseRGB;
                    }
                    else
                    {
						#PointLight
                        vec3 diffuseRGB = gl_Color.rgb * matDiffuse.rgb * _lightColour.rgb * attenuation * lt;
                        gl_FrontColor.rgb = ambientRGB + diffuseRGB;
                        gl_FrontColor.a = gl_Color.a + (_lightGlobalAmbient.a * matAmbient.a) + (_lightColour.a * attenuation * lt);
                    }

                    gl_TexCoord[0] = gl_MultiTexCoord0;
                }

            ]!

            Fragment ![

                #version 120

                uniform sampler2D tex0;

                void main()
                {
                    gl_FragColor = gl_Color * texture2D(tex0, gl_TexCoord[0].st);
                }

            ]!

        }

        {
            Name "DefaultAmbient_LightPassAdd_Medium"
            ZWrite False
            ZTest True
            LightPass Add
            SourceBlend One
            DestinationBlend One

            Vertex ![

                #version 120

                uniform mat4 _modelViewProjection;
                uniform mat4 _modelView;
				uniform mat4 _normalMatrix;
				uniform vec3 _world;
                uniform vec4 _lightWorldViewVector;
                uniform vec4 _lightColour;
                uniform float _lightIntensity;
                uniform float _lightRadius;
                uniform vec4 matDiffuse;

                void main()
                {
                    gl_Position = _modelViewProjection * gl_Vertex;

                    if (_lightRadius < 0)
                    {
                        #DirectionalLight
						gl_FrontColor.rgb = diffuseRGB;
                    }
                    else
                    {
                        #PointLight
                        gl_FrontColor.rgba = _lightColour.rgba * attenuation * lt;
                    }

                    gl_TexCoord[0] = gl_MultiTexCoord0;
                }


            ]!

            Fragment ![

                #version 120
                uniform sampler2D tex0;
                uniform float _sinTime;
                uniform float _cosTime;

                void main()
                {
                    gl_FragColor = gl_Color * texture2D(tex0, gl_TexCoord[0].st);
                }

            ]!
        }
    ]
}
