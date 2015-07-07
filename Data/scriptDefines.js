var Vortex = {}
Vortex.Core = {}
Vortex.Core.Timer = {}
Vortex.Core.Timer.GetTime = function () {
    /// <summary>
    /// Gets the time. yo.
    /// </summary>	
}

var SlimMath = {}
SlimMath = { }

SlimMath.Vector3 = function(x, y, z) {
    /// <summary>
    /// XYZ vector
    /// </summary>

    /// <field name='X'>X coordinate</field>
    this.X = x;

    /// <field name='Y'>Y coordinate</field>
    this.Y = y;

    /// <field name='Z'>Z coordinate</field>
    this.Z = z;

}

SlimMath.Vector3.Add = function (a, b) {
    /// <summary>
    /// Adds two vectors
    /// <param name="a" type="SlimMath.Vector3"></param>
    /// <param name="b" type="SlimMath.Vector3"></param>
    /// </summary>
    return new Vector3(a.X + b.X, a.Y + b.Y, c.X + c.Y);
}

SlimMath.Vector3.Multiply = function (a, b) {
    /// <summary>
    /// Multiplies two vectors
    /// <param name="a" type="SlimMath.Vector3"></param>
    /// <param name="b" type="SlimMath.Vector3"></param>
    /// </summary>
    return new Vector3(a.X * b.X, a.Y * b.Y, c.X * c.Y);
}

function RigidbodyComponent() {
    
}

function Entity() {
    this.Destroy = function() {
        /// <summary>
        /// Does the needful.
        /// </summary>
    }

    /// <field>Rigidbody.</field>
    this.RigidbodyComponent = new RigidbodyComponent();

    /// <field>Local position.</field>
    this.LocalPosition = new SlimMath.Vector3();
}

var entity = new Entity();




