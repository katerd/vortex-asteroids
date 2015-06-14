{
    Name "GUI"

    Passes
    [
        {
            Name "GUI Base Pass"

            ZWrite false
            ZTest false

            Vertex ![
                #version 120

                uniform mat4 _modelViewProjection;

                void main()
                {
                    gl_Position = _modelViewProjection * gl_Vertex;

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
                }
            ]!
        }

    ]
}
