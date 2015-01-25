{
    Name "DefaultAmbient"

    Passes
    [
        {
            Name "DefaultAmbient_LightPassBase_Medium"

            ZWrite True
            ZTest True
            LightPass Base

            Vertex ![

                #version 120

                uniform vec4 worldVec;
                uniform mat4 _modelViewProjection;
                uniform mat4 _modelView;
                uniform vec4 _lightWorldVector;
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
                        // directional light
                        vec3 viewLightNormal = (_modelView * _lightWorldVector).xyz;
                        vec3 viewNormal = (_modelView * vec4(gl_Normal, 0)).xyz;

                        float lt = dot(-_lightWorldVector.xyz, viewNormal.xyz);
                        lt *= _lightIntensity;

                        vec3 diffuseRGB = gl_Color.rgb * matDiffuse.rgb * _lightColour.rgb * lt;

                        gl_FrontColor.rgb = ambientRGB + diffuseRGB;
						
                        gl_FrontColor.a = matDiffuse.a * gl_Color.a;
                    }
                    else
                    {
                        // spotlight
                        vec3 worldPos = (_modelView * gl_Vertex).xyz;
                        float dist = length(worldPos - _lightWorldVector.xyz);
                        float radis = dist / _lightRadius;
                        float attenuation = clamp((1 / (radis))-1, 0.0, 1.0);

                        vec3 dtl = normalize(_lightWorldVector.xyz - worldPos);
                        float lt = clamp(dot(gl_Normal.xyz, dtl), 0.0, 1.0);
                        lt *= _lightIntensity;

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
                uniform vec4 _lightWorldVector;
                uniform vec4 _lightColour;
                uniform float _lightIntensity;
                uniform float _lightRadius;
                uniform vec4 matDiffuse;

                void main()
                {
                    gl_Position = _modelViewProjection * gl_Vertex;

                    if (_lightRadius < 0)
                    {
                        vec3 viewLightNormal = (_modelView * _lightWorldVector).xyz;
                        vec3 viewNormal = (_modelView * vec4(gl_Normal, 0)).xyz;

                        float lt = dot(-_lightWorldVector.xyz, viewNormal.xyz);
                        lt *= _lightIntensity;

                        vec3 diffuse = gl_Color.rgb * matDiffuse.rgb * _lightColour.rgb * lt;

                        gl_FrontColor.rgb = diffuse;

                        gl_FrontColor.a = matDiffuse.a * gl_Color.a;
                    }
                    else
                    {
                        vec3 worldPos = (_modelView * gl_Vertex).xyz;
                        float dist = length(worldPos - _lightWorldVector.xyz);
                        float radis = dist / _lightRadius;
                        float attenuation = clamp((1 / (radis))-1, 0.0, 1.0);

                        vec3 dtl = normalize(_lightWorldVector.xyz - worldPos);
                        float lt = clamp(dot(gl_Normal.xyz, dtl), 0.0, 1.0);
                        lt *= _lightIntensity;

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
