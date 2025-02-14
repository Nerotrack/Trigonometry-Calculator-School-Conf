using System;
using SFML.Graphics;
using SFML.System;

namespace Trigonometry_Calculator
{
    internal class Point : CircleShape, Drawable
    {
        float angleRad;
        public float AngleRad
        {
            get => angleRad;
            set
            {
                angleRad = MathF.Round(value, 3);
                AngleDeg = - 180 / MathF.PI * angleRad;
                Sin = -MathF.Round(MathF.Sin(angleRad), 3);
                Cos = MathF.Round(MathF.Cos(angleRad), 3);
                Tg = MathF.Round(Sin / Cos, 3);
                Ctg = MathF.Round(Cos / Sin, 3);
                // Изменение позиции относительно окружности после установления значения в angleRad.
                float cosAngle = Program.circle.Position.X + Program.circle.Radius * MathF.Cos(value);
                float sinAngle = Program.circle.Position.Y + Program.circle.Radius * MathF.Sin(value);

                Position = new Vector2f(cosAngle, sinAngle);
            }
        }
        public float AngleDeg { get; private set; }
        public float Sin { get; private set; }
        public float Cos { get; private set; }
        private float tg;
        public float Tg
        {
            get
            {
                if (Cos == 0)
                    Console.WriteLine("Тангенс не определён (cos = 0)");
                return tg;
            }
            private set
            {
                if (Cos == 0)
                    tg = float.NaN;
                else
                    tg = value;
            }
        }
        float ctg;
        public float Ctg
        {
            get
            {
                if (Sin == 0)
                    Console.WriteLine("Котангенс не определён (Sin = 0)");
                return ctg;
            }
            private set
            {
                if (Sin == 0)
                    ctg = float.NaN;
                else
                    ctg = value;
            }
        }

        public Point(float radians) : this(radians, 4, 15)
        { }
        public Point(float radians, Color color) : this(radians, 4, 15)
        => FillColor = color;
        public Point(float radians, float radius, uint pointCount) : base(radius, pointCount)
        {
            AngleRad = radians;

            if (Sin == -0)
            {
                Console.WriteLine("Синус был -0, стал 0.");
                Sin = 0;
            }
            if (Cos == -0)
            {
                Console.WriteLine("Косинус был -0, стал 0.");
                Cos = 0;
            }

            Origin = new Vector2f(Radius, Radius);
        }
    }
}
