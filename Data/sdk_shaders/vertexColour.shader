{
    Name "Vertex Colour"

    Passes
    [
        {
            Name "Vertex Colour Pass 1"

            Vertex ![

                #version 120

                uniform mat4 _modelViewProjection;

                void main()
                {
                    gl_Position = _modelViewProjection * gl_Vertex;
                    gl_FrontColor = gl_Color;
                }

            ]!

            Fragment ![

                #version 120

                void main()
                {
                    gl_FragColor = gl_Color;
                }

            ]!

        }

    ]

}
