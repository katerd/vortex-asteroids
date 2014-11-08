{
    Name Basic

    Passes
    [
        {
            Name "Basic Pass01"

            ZWrite true

            Vertex ![
                #version 120

                uniform mat4 worldViewProjMat;
                uniform mat4 viewMat;

                void main()
                {
                    gl_Position = worldViewProjMat * gl_Vertex;

                    gl_FrontColor = gl_Color;

                    gl_TexCoord[0] = gl_MultiTexCoord0;
                }
            ]!

            Fragment ![
                #version 120

                uniform sampler2D tex0;

                void main()
                {
                    gl_FragColor = gl_Color * texture2D(tex0, gl_TexCoord[0].st);

                    // testing
                }
            ]!
        }

    ]
}
