using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;

namespace Trigonometry_Calculator
{
    class Program
    {
        // Объекты, использующиеся в событиях.
        static VideoMode videoMode = new VideoMode(1280, 720);
        static RenderWindow window = new RenderWindow(videoMode, "TestApp");
        static Vector2f windowCenter = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        static CircleShape circle = new CircleShape();
        static Stack<CircleShape> pointStack = new Stack<CircleShape> { };

        // Изменение масштаба камеры.
        static Vector2f MaxZoomCount { get; set; } = new Vector2f(window.Size.X + window.Size.X / 6, window.Size.Y + window.Size.X / 6);
        static Vector2f MinZoomCount { get; set; } = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        static Vector2f ZoomStep { get; set; } = new Vector2f(window.Size.X / 20, window.Size.Y / 20);

        // Интерфейс.
        static View view = new View(windowCenter, new Vector2f(videoMode.Width, videoMode.Height));

        static void Main(string[] args)
        {
            RectangleShape XAxis = new RectangleShape(new Vector2f(window.Size.X * 2, 2));
                XAxis.Origin = new Vector2f(XAxis.Size.X / 2, XAxis.Size.Y / 2);
                XAxis.Position = windowCenter;
                XAxis.FillColor = new Color(180, 180, 180);
            RectangleShape YAxis = new RectangleShape(new Vector2f(2, window.Size.Y * 2));
                YAxis.Origin = new Vector2f(YAxis.Size.X / 2, YAxis.Size.Y / 2);
                YAxis.Position = windowCenter;
                YAxis.FillColor = new Color(180, 180, 180);

            circle = new CircleShape(200, 75);
                circle.Origin = new Vector2f(circle.Radius, circle.Radius);
                circle.Position = windowCenter;
                circle.FillColor = Color.Transparent;
                circle.OutlineThickness = 2;
                circle.OutlineColor = Color.White;


            // Подписка методов к событиям.
            window.Closed += (obj, e) => window.Close();
            window.KeyPressed += KeyPressedHandler!;
            window.MouseWheelScrolled += MouseWheelScrolledHandler!;
            window.MouseButtonPressed += MouseButtonPressedHandler!;
            window.Resized += WindowResizedHandler;

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(new Color(20, 20, 20));

                // Код.

                // Генерация графических объектов.

                window.SetView(view);
                window.Draw(XAxis);
                window.Draw(YAxis);
                window.Draw(circle);
                foreach (var point in pointStack)
                {
                    window.Draw(point);
                }

                //Отрисовка сгенерированных графических объектов.
                window.Display();
            }
        }

        #region Обработчики системных событий.
        private static void MouseWheelScrolledHandler(object sender, MouseWheelScrollEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (view.Size.X < MaxZoomCount.X && view.Size.Y < MaxZoomCount.Y)
                    view.Size += ZoomStep;
            }
            if (e.Delta > 0)
            {
                if (view.Size.X > MinZoomCount.X && view.Size.Y > MinZoomCount.Y)
                    view.Size -= ZoomStep;
            }
        }
        static void KeyPressedHandler(object sender, KeyEventArgs e)
        {
            if (sender is RenderWindow window)
            {
                // Выход из программы.
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }
                // Удаление созданных точек на окружности.
                if (e.Code == Keyboard.Key.Z && pointStack.Count >= 1)
                {
                    pointStack.Pop();
                }
            }
        }
        static void MouseButtonPressedHandler(object sender, MouseButtonEventArgs e)
        {
            // Создание точки на окружности.
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2f mousePos = window.MapPixelToCoords(new Vector2i(e.X, e.Y));

                CircleShape point = new CircleShape(3, 15);
                point.FillColor = Color.Red;
                point.Origin = new Vector2f(point.Radius, point.Radius);

                float angle = MathF.Atan2(mousePos.Y - circle.Origin.Y, mousePos.X - circle.Origin.X);
                float cosAngle = circle.Origin.X + circle.Radius * MathF.Cos(angle);
                float sinAngle = circle.Origin.Y + circle.Radius * MathF.Sin(angle);

                point.Position = new Vector2f(cosAngle, sinAngle);
                pointStack.Push(point);

                if (circle.GetGlobalBounds().Contains(e.X, e.Y))
                {
                    Console.WriteLine("Произошёл клик в области окружности.");
                }
            }
        }
        private static void WindowResizedHandler(object? sender, SizeEventArgs e)
        {
            view.Size = new Vector2f(window.Size.X, window.Size.Y);
        }
        #endregion
    }
}
