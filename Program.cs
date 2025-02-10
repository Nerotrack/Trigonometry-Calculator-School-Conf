using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace TrigonometryCalculator
{

    class Program
    {
        static CircleShape circle = new CircleShape(radius: 200, 100);
        static RectangleShape button = new RectangleShape(new Vector2f(200, 100));
        static void Main(string[] args)
        {

            // Инициализация объектов.
            VideoMode videoMode = new VideoMode(width: 1280, height: 720);
            RenderWindow window = new RenderWindow(videoMode, title: "Тригонометрический калькулятор", Styles.Close);
            window.SetFramerateLimit(60);

            Vector2f windowCenter = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            RectangleShape XAxis = new RectangleShape(new Vector2f(window.Size.X, 2));
            XAxis.Origin = new Vector2f(XAxis.Size.X / 2, XAxis.Size.Y / 2);
            XAxis.Position = windowCenter;

            RectangleShape YAxis = new RectangleShape(new Vector2f(2, window.Size.Y));
            YAxis.Origin = new Vector2f(YAxis.Size.X / 2, YAxis.Size.Y / 2);
            YAxis.Position = windowCenter;

            circle.Origin = new Vector2f(-videoMode.Width / 2 + circle.Radius, -videoMode.Height / 2 + circle.Radius);
            circle.FillColor = Color.Transparent;
            circle.OutlineThickness = 2;
            circle.OutlineColor = Color.White;

            RectangleShape line = new RectangleShape(new Vector2f(400, 1));
            line.Origin = new Vector2f(line.Size.X / 2, line.Size.Y / 2);
            line.Position = windowCenter;

            Sprite sprite = new Sprite(new Texture("C:\\Users\\dsark\\Downloads\\52b7921e533e92e28a5df863510d1656.jpg"),
                                        new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y));
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
            sprite.Position = windowCenter;
            sprite.Color = new Color(40, 40, 40);
            //sprite.Scale += new Vector2f( window.Size.X / sprite.TextureRect.Size.X, window.Size.Y / sprite.TextureRect.Size.Y);
            sprite.Texture.Repeated = true;

            // Button.
            button.Origin = new Vector2f(button.Size.X / 2, button.Size.Y / 2);
            button.Position = new Vector2f(130, 650); 
            button.FillColor = new Color(100, 100, 100);
            button.OutlineThickness = 3;
            button.OutlineColor = new Color(150, 150, 150);
            

            // Подписание всех нужных действий к нужным событиям.
            window.Closed += ClosedEventHandler!;
            window.KeyPressed += KeyPressedEventHandler!;
            window.MouseButtonPressed += MouseButtonPressedHandler!;

            while (window.IsOpen)
            {
                // Обработка всех произошедших событий.
                window.DispatchEvents();

                // Очищение кадра.
                window.Clear();

                // Любая логика, происходящая каждый кадр.

                line.Rotation += 1;

                // Генерация графических объектов.
                window.Draw(sprite);
                
                window.Draw(XAxis);
                window.Draw(YAxis);
                window.Draw(circle);

                window.Draw(line);
                window.Draw(button);

                

                // Отображение сгенерированных объектов.
                window.Display();

            }

        }

        #region Методы, выполняющиеся каждый кадр.
        //static void CircleMove()
        //{
        //    float speed = 10;
        //    if (Keyboard.IsKeyPressed(Keyboard.Key.A)) circle.Position -= new Vector2f(1, 0) * speed;
        //    else if (Keyboard.IsKeyPressed(Keyboard.Key.D)) circle.Position += new Vector2f(1, 0) * speed;
        //    else if (Keyboard.IsKeyPressed(Keyboard.Key.W)) circle.Position += new Vector2f(0, -1) * speed;
        //    else if (Keyboard.IsKeyPressed(Keyboard.Key.S)) circle.Position += new Vector2f(0, 1) * speed;
        //}

        #endregion

        #region Обработчики системных событий.
        static void ClosedEventHandler(object sender, EventArgs e)
        {
            if(sender is RenderWindow window)
                window.Close();
        }
        static void KeyPressedEventHandler(object sender, KeyEventArgs e)
        {
            if(sender is RenderWindow window)
            {
                if (e.Code == Keyboard.Key.Escape)
                    window.Close();

            }
        }
        static void MouseButtonPressedHandler(object sender, MouseButtonEventArgs e)
        {
            if(e.Button == Mouse.Button.Left)
            {
                if (button.GetGlobalBounds().Contains(e.X, e.Y))
                    Console.WriteLine("Нажата кнопка.");

            }
        }

        #endregion
    }

}
