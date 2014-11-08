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
                uniform mat4 worldViewProjMat;
                uniform mat4 viewMat;
                uniform vec4 lightVector;
                uniform vec4 lightColour;
                uniform float lightIntensity;
                uniform float lightRadius;
                uniform vec4 matDiffuse;
                uniform vec4 matAmbient;
                uniform vec4 globalAmbient;

                void main()
                {
                    gl_Position = worldViewProjMat * gl_Vertex;

                    vec3 ambientRGB = globalAmbient.rgb * matAmbient.rgb;

                    if (lightRadius < 0)
                    {
                        // directional light
                        vec3 viewLightNormal = (viewMat * lightVector).xyz;
                        vec3 viewNormal = (viewMat * vec4(gl_Normal, 0)).xyz;

                        float lt = dot(-lightVector.xyz, viewNormal.xyz);
                        lt *= lightIntensity;

                        vec3 diffuseRGB = gl_Color.rgb * matDiffuse.rgb * lightColour.rgb * lt;

                        gl_FrontColor.rgb = ambientRGB + diffuseRGB;
                        gl_FrontColor.a = matDiffuse.a * gl_Color.a;
                    }
                    else
                    {
                        // spotlight
                        vec3 worldPos = (viewMat * gl_Vertex).xyz;
                        float dist = length(worldPos - lightVector.xyz);
                        float radis = dist / lightRadius;
                        float attenuation = clamp((1 / (radis))-1, 0.0, 1.0);

                        vec3 dtl = normalize(lightVector.xyz - worldPos);
                        float lt = clamp(dot(gl_Normal.xyz, dtl), 0.0, 1.0);
                        lt *= lightIntensity;

                        vec3 diffuseRGB = gl_Color.rgb * lightColour.rgb * attenuation * lt;

                        gl_FrontColor.rgb = ambientRGB + diffuseRGB;

                        gl_FrontColor.a = gl_Color.a + (globalAmbient.a * matAmbient.a) + (lightColour.a * attenuation * lt);
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

                uniform mat4 worldViewProjMat;
                uniform mat4 viewMat;
                uniform vec4 lightVector;
                uniform vec4 lightColour;
                uniform float lightIntensity;
                uniform float lightRadius;
                uniform vec4 matDiffuse;

                void main()
                {
                    gl_Position = worldViewProjMat * gl_Vertex;

                    if (lightRadius < 0)
                    {
                        vec3 viewLightNormal = (viewMat * lightVector).xyz;
                        vec3 viewNormal = (viewMat * vec4(gl_Normal, 0)).xyz;

                        float lt = dot(-lightVector.xyz, viewNormal.xyz);
                        lt *= lightIntensity;

                        vec3 diffuse = gl_Color.rgb * matDiffuse.rgb * lightColour.rgb * lt;

                        gl_FrontColor.rgb = diffuse;

                        gl_FrontColor.a = matDiffuse.a * gl_Color.a;
                    }
                    else
                    {
                        vec3 worldPos = (viewMat * gl_Vertex).xyz;
                        float dist = length(worldPos - lightVector.xyz);
                        float radis = dist / lightRadius;
                        float attenuation = clamp((1 / (radis))-1, 0.0, 1.0);

                        vec3 dtl = normalize(lightVector.xyz - worldPos);
                        float lt = clamp(dot(gl_Normal.xyz, dtl), 0.0, 1.0);
                        lt *= lightIntensity;

                        gl_FrontColor.rgba = lightColour.rgba * attenuation * lt;
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
    ]
}
