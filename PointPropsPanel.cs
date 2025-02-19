using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Trigonometry_Calculator
{
    internal class PointPropsPanel : Drawable
    {
        Point? Point { get; set; } = null;
        public RectangleShape Rectangle { get; set; }
        public Text PropsText { get; set; } = new Text("Свойства точки:\nКликните на созданную\nточку, чтобы показать\nеё свойства. ", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);

        public PointPropsPanel()
        {
            Rectangle = new RectangleShape(new Vector2f(250, 175));
            Rectangle.Origin = new Vector2f(Rectangle.Size.X / 2, Rectangle.Size.Y / 2);
            Rectangle.Position = new Vector2f(Program.window.Size.X / 2 - Rectangle.Size.X / 2 - 15, Program.window.Size.Y / 2 - Rectangle.Size.Y / 2 - 15);
            Rectangle.FillColor = new Color(80, 80, 80);
            Rectangle.OutlineColor = new Color(150, 150, 150);
            Rectangle.OutlineThickness = 2;
            PropsText.Position = Rectangle.Position + new Vector2f(-Rectangle.Size.X / 2 + 5, -Rectangle.Size.Y / 2 + 2);
        }

        public void ShowPointProps(Point point) 
        =>  PropsText.DisplayedString = $"Свойства точки:\n" +
                                        $"Rad = {-point.AngleRad}\n" +
                                        $"Deg = {point.AngleDeg} °  (≈ {MathF.Round(point.AngleDeg)}°)\n" +
                                        $"Sin = {point.Sin}\n" +
                                        $"Cos = {point.Cos}\n" +
                                        $"Tg = {point.Tg}\n" +
                                        $"Ctg = {point.Ctg}\n";


        public void Draw(RenderTarget target, RenderStates states)
        {
            Rectangle.Draw(target, RenderStates.Default);
            PropsText.Draw(target, RenderStates.Default);
        }
    }
}
