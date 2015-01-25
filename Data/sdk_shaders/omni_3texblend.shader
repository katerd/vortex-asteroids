VortexShaderVersion 2

Name "Omni Direction Lighting / 3 Textures"

#BeginPass 

    ZWrite True

    #BeginVertex

        #version 120

        uniform mat4 _modelViewProjection;
        uniform mat4 _modelView;
        uniform vec3 lightNormal;
        uniform vec4 matDiffuse = vec4(1.0, 1.0, 1.0, 1.0);
        uniform vec4 matAmbient = vec4(0.0, 0.0, 0.0, 0.0);

        void main()
        {
            gl_Position = _modelViewProjection * gl_Vertex;

            vec3 viewLightNormal = (_modelView * vec4(lightNormal, 0)).xyz;
            vec3 viewNormal = (_modelView * vec4(gl_Normal, 0)).xyz;

            float lt = dot(-lightNormal.xyz, viewNormal.xyz);

            lt = max(0.1, lt);

            gl_FrontColor.rgb = (matDiffuse.rgb * gl_Color.rgb * lt) + matAmbient.rgb;
            gl_FrontColor.a = matDiffuse.a * gl_Color.a;

            gl_TexCoord[0] = gl_MultiTexCoord0;
            gl_TexCoord[1] = gl_MultiTexCoord1;
        }

    #EndVertex

    #BeginFragment

        #version 120

        uniform sampler2D texBlend;
        uniform sampler2D tex0;
        uniform sampler2D tex1;
        uniform sampler2D tex2;
        uniform sampler2D tex3;
        uniform bool performTexLookup = true;

        uniform float tex0scale = 32;
        uniform float tex1scale = 32;
        uniform float tex2scale = 32;
        uniform float tex3scale = 256;

        void main()
        {
            vec4 f = texture2D(texBlend, gl_TexCoord[0].st);

            if (performTexLookup)
            {
                vec4 tfac0 = texture2D(tex0, gl_TexCoord[1].st * tex0scale) * f.r;
                vec4 tfac1 = texture2D(tex1, gl_TexCoord[1].st * tex1scale) * f.g;
                vec4 tfac2 = texture2D(tex2, gl_TexCoord[1].st * tex2scale) * f.b;
                vec4 detail_tfac2 = texture2D(tex3, gl_TexCoord[1].st * tex3scale);

                gl_FragColor.rgb = detail_tfac2.rgb * (tfac0 + tfac1 + tfac2).rgb * gl_Color.rgb;
            }
            else
            {
                gl_FragColor.rgb = gl_Color.rgb;
            }

            gl_FragColor.a = 1.0;
        }

    #EndFragment

#EndPass