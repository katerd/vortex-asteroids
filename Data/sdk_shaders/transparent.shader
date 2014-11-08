VortexShaderVersion 2

Name "Transparent"
Queue 2500

#BeginPass

    ZWrite False

    #BeginVertex

        #version 120

        uniform mat4 worldViewProjMat;
        uniform mat4 viewMat;
        uniform vec3 lightNormal;
        uniform bool useLight = true;
        uniform vec4 matDiffuse = vec4(1.0, 1.0, 1.0, 1.0);
        uniform vec4 matAmbient = vec4(1.0, 0.0, 0.0, 0.0);

        void main()
        {
            gl_Position = worldViewProjMat * gl_Vertex;

            vec3 viewLightNormal = (viewMat * vec4(lightNormal, 0)).xyz;
            vec3 viewNormal = (viewMat * vec4(gl_Normal, 0)).xyz;

            float lt = dot(-lightNormal.xyz, viewNormal.xyz);

            if (useLight)
            {
                lt = max(0.3, lt);
            }
            else
            {
                lt = 1;
            }

            gl_FrontColor.rgb = (matDiffuse.rgb * gl_Color.rgb * lt) + matAmbient.rgb;
            gl_FrontColor.a = matDiffuse.a * gl_Color.a;

            gl_TexCoord[0] = gl_MultiTexCoord0;
        }

    #EndVertex

    #BeginFragment

        #version 120

        uniform sampler2D tex0;

        void main()
        {
            gl_FragColor = gl_Color * texture2D(tex0, gl_TexCoord[0].st);
        }

    #EndFragment

#EndPass