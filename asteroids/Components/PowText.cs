using SlimMath;
using Vortex.Core;
using Vortex.Core.Easing;
using Vortex.Core.Extensions;
using Vortex.Gui;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids.Components
{
    public class PowText : ScriptComponent
    {
        private LabelWidgetComponent _label;
        private LinearFloatEasing _labelSizeEasing;
        private LinearFloatEasing _alphaEasing;
        public string Text { get; set; }
        public Color4 TextColour { get; set; }

        public PowText()
        {
            TextColour = Colours.White;
        }

        public override void Initialize()
        {
            base.Initialize();

            _label = Entity.CreateComponent<LabelWidgetComponent>();
            _label.VerticalAlignment = VerticalAlignment.Middle;
            _label.HorizontalAlignment = HorizontalAlignment.Centre;
            _label.Visible = true;

            _labelSizeEasing = new LinearFloatEasing(12, 40, 2);
            _alphaEasing = new LinearFloatEasing(1.0f, 0, 1);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            _label.Text = Text;

            if (Entity.Destroyed)
                return;

            _labelSizeEasing.Update(delta);
            _alphaEasing.Update(delta);
            
            _label.FontSize = _labelSizeEasing.Value;
            _label.Colour = TextColour.MakeTransparent(_alphaEasing.Value);

            if (_labelSizeEasing.Finished)
                Entity.Destroy();
        }
    }
}