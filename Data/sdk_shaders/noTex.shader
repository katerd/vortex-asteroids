{
    Name "No Texture"

    Passes
    [
        {
            Name "No Texture Pass01"


            Vertex ![

                #version 120

                uniform mat4 worldViewProjMat;

                void main()
                {
                    gl_Position = worldViewProjMat * gl_Vertex;
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
