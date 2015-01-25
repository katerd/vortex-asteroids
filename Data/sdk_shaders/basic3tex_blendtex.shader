VortexShaderVersion 2

Name "Basic 3 Texture"

#BeginPass

    ZWrite True

    #BeginVertex

        #version 120

        uniform mat4 _modelViewProjection;

        void main()
        {
            gl_Position = _modelViewProjection * gl_Vertex;

            gl_FrontColor = gl_Color;

            gl_TexCoord[0] = gl_MultiTexCoord0;
        }

    #EndVertex

    #BeginFragment

        #version 120

        uniform sampler2D texBlend;
        uniform sampler2D tex0;
        uniform sampler2D tex1;
        uniform sampler2D tex2;

        void main()
        {
            vec4 f = texture2D(texBlend, gl_TexCoord[0].st);
            vec4 tfac0 = texture2D(tex0, gl_TexCoord[0].st) * f.r;
            vec4 tfac1 = texture2D(tex1, gl_TexCoord[0].st) * f.g;
            vec4 tfac2 = texture2D(tex2, gl_TexCoord[0].st) * f.b;

            //gl_FragColor = tfac0 + tfac1 + tfac2;

            gl_FragColor.r = gl_TexCoord[0].s;
            gl_FragColor.g = 0;
            gl_FragColor.b = 0;
        }

    #EndFragment

#EndPass