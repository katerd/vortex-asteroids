VortexShaderVersion 2

Name "Transparent"
Queue 2500

#BeginPass

    ZWrite False

    #BeginVertex

        #version 120

        uniform mat4 _modelViewProjection;
        uniform mat4 _modelView;
        uniform vec3 _lightNormal;
        uniform bool useLight = true;
        uniform vec4 matDiffuse = vec4(1.0, 1.0, 1.0, 1.0);
        uniform vec4 matAmbient = vec4(1.0, 0.0, 0.0, 0.0);

        void main()
        {
            gl_Position = _modelViewProjection * gl_Vertex;

            vec3 viewLightNormal = (_modelView * vec4(_lightNormal, 0)).xyz;
            vec3 viewNormal = (_modelView * vec4(gl_Normal, 0)).xyz;

            float lt = dot(-_lightNormal.xyz, viewNormal.xyz);

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