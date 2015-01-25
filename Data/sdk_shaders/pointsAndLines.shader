{
    Name Basic

    Passes
    [
        {
            Name "PointsAndLines"

            ZWrite true
			PointSize 5

            Vertex ![
                #version 120

                uniform mat4 _modelViewProjection;
				uniform vec4 matAmbient;

                void main()
                {
                    gl_Position = _modelViewProjection * gl_Vertex;
                    gl_FrontColor = gl_Color + matAmbient;
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
