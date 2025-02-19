using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Trigonometry_Calculator
{
    class Program
    {
        // Переменные для технических настроек отображения объектов сцены.
        static bool inRadian = true;
        // Объекты, использующиеся в событиях.
        static VideoMode videoMode = new VideoMode(1280, 720);
        public static RenderWindow window = new RenderWindow(videoMode, "Тригонометрический калькулятор", Styles.Close);
        static View view = new View(new Vector2f(), new Vector2f(videoMode.Width, videoMode.Height));
        public static CircleShape circle = new CircleShape();
        public static Stack<Point> circlePoints = new Stack<Point> { };
        static VertexArray pointLine = new VertexArray(PrimitiveType.Lines, 10);
        static VertexArray stripesCos = new VertexArray(PrimitiveType.Lines, 32);
        static VertexArray stripesSin = new VertexArray(PrimitiveType.Lines, 16);
        static VertexArray stripesTg = new VertexArray(PrimitiveType.Lines, 32);
        static VertexArray stripesCtg = new VertexArray(PrimitiveType.Lines, 32);

        // Интерфейс.
        static InputField inputField = new InputField(view.Center + new Vector2f(0, window.Size.Y / 2 - 50), new Vector2f(400, 25), new Font("C:\\Windows\\Fonts\\arialbd.ttf"));
        static RectangleShape inputFieldBG = new RectangleShape(new Vector2f(inputField.Width + 15, inputField.Height + 15));
        static Text inputFieldBGText = new Text("Угол:", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
        static RectangleShape targetCalculationBG = new RectangleShape(new Vector2f(200, 300));
        static PointPropsPanel pointPropsPanel = new PointPropsPanel();
        static Button radDegSwitcher = new Button("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Switcher_Rad.png");
        static RectangleShape displayAtPointBG = new RectangleShape(new Vector2f(300, 40));
        static Text displayAtPointText = new Text("Отобразить у точки:", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
        static Button displaySinButton = new Button("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Sin_Texture.png");
        static Button displayCosButton = new Button("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Cos_Texture.png");
        static Button displayTgButton = new Button("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Tg_Texture.png");
        static Button displayCtgButton = new Button("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Ctg_Texture.png");

        static void Main(string[] args)
        {
            circle = new CircleShape(200, 75);
            circle.Origin = new Vector2f(circle.Radius, circle.Radius);
            circle.FillColor = Color.Transparent;
            circle.OutlineThickness = 1.5f;
            circle.OutlineColor = Color.White;

            RectangleShape XAxis = new RectangleShape(new Vector2f(window.Size.X * 2, 2));
                XAxis.Origin = new Vector2f(XAxis.Size.X / 2, XAxis.Size.Y / 2);
                XAxis.FillColor = new Color(200, 200, 200);
            Text cosText = new Text("cos", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
                cosText.FillColor = new Color(255, 150, 150);
                cosText.Position = new Vector2f(window.Size.X / 2 - 40, -27);
            VertexArray cosAngleLine = new VertexArray(PrimitiveType.Lines, 2);

            RectangleShape YAxis = new RectangleShape(new Vector2f(2, window.Size.Y * 2));
                YAxis.Origin = new Vector2f(YAxis.Size.X / 2, YAxis.Size.Y / 2);
                YAxis.FillColor = new Color(200, 200, 200);
            Text sinText = new Text("sin", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
                sinText.FillColor = new Color(150, 255, 150);
                sinText.Position = new Vector2f(-37, -window.Size.Y / 2);

            RectangleShape TgAxis = new RectangleShape(new Vector2f(2, window.Size.Y * 2));
                TgAxis.Origin = new Vector2f(TgAxis.Size.X / 2, TgAxis.Size.Y / 2);
                TgAxis.Position = new Vector2f(circle.Radius, 0);
                TgAxis.FillColor = new Color(200, 200, 200);
            Text tgText = new Text("tg", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
            tgText.FillColor = new Color(190, 100, 190);
                tgText.Position = TgAxis.Position + new Vector2f(-30, - window.Size.Y / 2);

            RectangleShape CtgAxis = new RectangleShape(new Vector2f(window.Size.X * 2, 2));
                CtgAxis.Origin = new Vector2f(CtgAxis.Size.X / 2, CtgAxis.Size.Y / 2);
                CtgAxis.Position = new Vector2f(0, -circle.Radius);
                CtgAxis.FillColor = new Color(200, 200, 200);
            Text ctgText = new Text("ctg", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
                ctgText.Position = CtgAxis.Position + new Vector2f(-window.Size.X / 2 + 5, - 30);
                ctgText.FillColor = new Color(100, 100, 220);

            for (uint i = 0; i < stripesSin.VertexCount / 2; i++)
            {
                stripesSin[i * 2] = new Vertex(new Vector2f(0, -500) + new Vector2f(-5, circle.Radius / 2 * (i + 1)), new Color(255, 255, 255, 255));
                stripesSin[i * 2 + 1] = new Vertex(stripesSin[i * 2].Position + new Vector2f(10, 0), new Color(255, 255, 255, 255));
            }
            for (uint i = 0; i < stripesCos.VertexCount / 2; i++)
            {
                stripesCos[i * 2] = new Vertex(new Vector2f(-4 * circle.Radius + 1, 0) + new Vector2f(circle.Radius / 2 * (i + 1), 5), new Color(255, 255, 255, 255));
                stripesCos[i * 2 + 1] = new Vertex(stripesCos[i * 2].Position + new Vector2f(0, -10), new Color(255, 255, 255, 255));
            }
            for (uint i = 0; i < stripesCtg.VertexCount / 2; i++)
            {
                stripesCtg[i * 2] = new Vertex(new Vector2f(- 4 * circle.Radius, -circle.Radius) + new Vector2f(circle.Radius / 2 * (i + 1), 5), new Color(255, 255, 255, 255));
                stripesCtg[i * 2 + 1] = new Vertex(stripesCtg[i * 2].Position + new Vector2f(0, -10), new Color(255, 255, 255, 255));
            }
            for (uint i = 0; i < stripesTg.VertexCount / 2; i++)
            {
                stripesTg[i * 2] = new Vertex(new Vector2f(circle.Radius, -3 * circle.Radius) + new Vector2f(-5, circle.Radius / 2 * (i + 1)), new Color(255, 255, 255, 255));
                stripesTg[i * 2 + 1] = new Vertex(stripesTg[i * 2].Position + new Vector2f(10, 0), new Color(255, 255, 255, 255));
            }


            inputFieldBG = new RectangleShape(new Vector2f(inputField.Width + 15, inputField.Height + 15));
            inputFieldBG.Origin = new Vector2f(inputFieldBG.Size.X / 2, inputFieldBG.Size.Y / 2);
            inputFieldBG.FillColor = new Color(100, 100, 100);
            inputFieldBG.OutlineColor = new Color(150, 150, 150);
            inputFieldBG.OutlineThickness = 2;

            inputFieldBGText.Position = inputField.Position + new Vector2f(-inputField.Width / 2 - 70, 0);

            targetCalculationBG.Origin = new Vector2f(targetCalculationBG.Size.X / 2, targetCalculationBG.Size.Y / 2);
            targetCalculationBG.Position = new Vector2f(-window.Size.X / 2 + targetCalculationBG.Size.X / 2 + 15, window.Size.Y / 2 - targetCalculationBG.Size.Y / 2 - 15);
            targetCalculationBG.FillColor = new Color(80, 80, 80);
            targetCalculationBG.OutlineColor = new Color(150, 150, 150);
            targetCalculationBG.OutlineThickness = 2;

            radDegSwitcher.Position = inputField.Position + new Vector2f(250, 10);

            Text targetCalcText = new Text("Цель вычисления:", new Font("C:\\Windows\\Fonts\\arialbd.ttf"), 20);
            targetCalcText.Position = targetCalculationBG.Position + new Vector2f(-targetCalculationBG.Size.X / 2 + 5, -targetCalculationBG.Size.Y / 2);

            displayAtPointBG.Origin = new Vector2f(displayAtPointBG.Size.X / 2, displayAtPointBG.Size.Y / 2);
            displayAtPointBG.Position = new Vector2f(window.Size.X / 2 - displayAtPointBG.Size.X / 2 - 15, 75);
            displayAtPointBG.FillColor = new Color(80, 80, 80);
            displayAtPointBG.OutlineColor = new Color(150, 150, 150);
            displayAtPointBG.OutlineThickness = 2;

            displayAtPointText.Position = displayAtPointBG.Position + new Vector2f(-displayAtPointBG.Size.X / 4 - 30, -13);

            displaySinButton.sprite.TextureRect = new IntRect(new Vector2i(), new Vector2i((int)displaySinButton.sprite.GetGlobalBounds().Width, (int)displaySinButton.sprite.GetGlobalBounds().Height / 2 + 2));
            displaySinButton.Position = new Vector2f(window.Size.X / 2 - 250, 140);

            displayCosButton.sprite.TextureRect = new IntRect(new Vector2i(), new Vector2i((int)displayCosButton.sprite.GetGlobalBounds().Width, (int)displayCosButton.sprite.GetGlobalBounds().Height / 2 + 2));
            displayCosButton.Position = new Vector2f(window.Size.X / 2 - 185, 140);

            displayTgButton.sprite.TextureRect = new IntRect(new Vector2i(), new Vector2i((int)displaySinButton.sprite.GetGlobalBounds().Width, (int)displayTgButton.sprite.GetGlobalBounds().Height / 2 + 2));
            displayTgButton.Position = new Vector2f(window.Size.X / 2 - 120, 140);

            displayCtgButton.sprite.TextureRect = new IntRect(new Vector2i(), new Vector2i((int)displayCtgButton.sprite.GetGlobalBounds().Width, (int)displayCtgButton.sprite.GetGlobalBounds().Height / 2 + 2));
            displayCtgButton.Position = new Vector2f(window.Size.X / 2 - 55, 140);

            // Подписка методов к событиям.
            window.Closed += (obj, e) => window.Close();
            window.KeyPressed += KeyPressedHandler!;
            window.MouseWheelScrolled += MouseWheelScrolledHandler!;
            window.MouseButtonPressed += MouseButtonPressedHandler!;
            window.Resized += WindowResizedHandler;
            window.MouseMoved += MouseMovedHandler;
            window.TextEntered += TextEnteredHandler;

            float vx = 0, vy = 0;

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(new Color(20, 20, 20));

                // Код.

                // Движение камеры.

                // Движение графического интерфейса.
                inputField.Position = view.Center + new Vector2f(0, window.Size.Y - 400);
                inputFieldBG.Position = inputField.Position + new Vector2f();

                // Генерация графических объектов.
                window.SetView(view);

                window.Draw(XAxis);
                window.Draw(YAxis);
                window.Draw(TgAxis);
                window.Draw(CtgAxis);

                window.Draw(stripesCos);
                window.Draw(stripesSin);
                window.Draw(stripesCtg);
                window.Draw(stripesTg);

                window.Draw(cosText);
                window.Draw(sinText);
                window.Draw(tgText);
                window.Draw(ctgText);

                window.Draw(circle);

                    // Отрисовка отрезков, связанных с точкой и её тригонометрической функцией.
                if (circlePoints.ToList().FirstOrDefault(point => point.IsSelected == true) is not null)
                {
                    if (sinLine is not null)                   
                        window.Draw(sinLine);
                    if (cosLine is not null)
                        window.Draw(cosLine);
                    if (tgLine is not null)
                        window.Draw(tgLine);
                    if (ctgLine is not null)
                        window.Draw(ctgLine);
                }

                // Отрисовка линии, проведённой к выбранной точке, если она не пуста.
                if (circlePoints.ToList().FirstOrDefault(point => point.IsSelected == true) is not null)
                    window.Draw(pointLine);

                foreach (var point in circlePoints)
                {
                    window.Draw(point);
                }

                // Отрисовка интерфейса.
                window.Draw(targetCalculationBG);
                window.Draw(targetCalcText);
                window.Draw(inputFieldBG);
                window.Draw(inputFieldBGText);
                inputField.Draw(window);
                window.Draw(radDegSwitcher);
                window.Draw(pointPropsPanel);

                window.Draw(displayAtPointBG);
                window.Draw(displayAtPointText);
                window.Draw(displayCosButton);
                window.Draw(displaySinButton);
                window.Draw(displayTgButton);
                window.Draw(displayCtgButton);
                //Отрисовка сгенерированных графических объектов.
                window.Display();
            }
        }

        #region Обработчики системных событий.
        static void MouseWheelScrolledHandler(object sender, MouseWheelScrollEventArgs e)
        {
            //Vector2f viewSize0 = view.Size;
            //if (e.Delta < 0)
            //{
            //    if (view.Size.X < MaxZoomCount.X && view.Size.Y < MaxZoomCount.Y)
            //    {
            //        view.Size += ZoomStep;
                    
            //    }
            //}
            //if (e.Delta > 0)
            //{
            //    if (view.Size.X > MinZoomCount.X && view.Size.Y > MinZoomCount.Y)
            //    {
            //        view.Size -= ZoomStep;
            //    }                    
            //}
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
                if (e.Code == Keyboard.Key.Z && circlePoints.Count >= 1)
                {
                    circlePoints.Pop();
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Backspace))
                inputField.Backspace();
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
                inputField.HandleInput("π");
            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
                inputField.HandleInput("√3");
            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
                inputField.HandleInput("√2");
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                try
                {
                    inputField.Text.DisplayedString = inputField.Text.DisplayedString.Replace("π", "3.1415");
                    inputField.Text.DisplayedString = inputField.Text.DisplayedString.Replace("√3", "1.732");
                    inputField.Text.DisplayedString = inputField.Text.DisplayedString.Replace("√2", "1.414");

                    inputField.Text.DisplayedString = System.Text.RegularExpressions.Regex.Replace(
                    inputField.Text.DisplayedString, "([a-zA-Z0-9])([a-zA-Z])", "$1 * $2" );

                    float value = Convert.ToSingle(new DataTable().Compute(inputField.Text.DisplayedString, ""));
                    if (inRadian)
                        circlePoints.Push(new Point(-value));
                    else if (!inRadian) // Если в градусах,
                        circlePoints.Push(new Point(MathF.PI / 180 * -value));
                    inputField.Clear();
                }
                catch(Exception ex)
                { Console.WriteLine($"Ошибка. {ex}"); }   
            }
        }
        static void MouseButtonPressedHandler(object sender, MouseButtonEventArgs e)
        {
            Vector2f mousePos = window.MapPixelToCoords(new Vector2i(e.X, e.Y));

            Point? point = circlePoints.ToList().FirstOrDefault(point => point.GetGlobalBounds().Contains(window.MapPixelToCoords(Mouse.GetPosition(window))));
            if (e.Button == Mouse.Button.Left && point is not null)
            {
                foreach (Point p in circlePoints)
                {
                    p.FillColor = Color.Red;
                    p.IsSelected = false;

                    //Отключение всех отрезков тригонометрических функций каждой точки.
                    Switch(p, SwitchSin, false);
                    Switch(p, SwitchCos, false);
                    Switch(p, SwitchTg, false);
                    Switch(p, SwitchCtg, false);

                }
                point.FillColor = Color.Yellow;
                point.IsSelected = true;
                pointLine[0] = new Vertex(circlePoints.ToList().FirstOrDefault(point => point.IsSelected == true)!.Position);
                pointPropsPanel.ShowPointProps(point);

            }
            if(e.Button == Mouse.Button.Left && radDegSwitcher.sprite.GetGlobalBounds().Contains(mousePos))
            {
                if (inRadian == true)
                {
                    inRadian = false;
                    radDegSwitcher.sprite.Texture = new Texture("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Switcher_Deg.png");
                    Console.WriteLine("В радианах: false");
                }
                else
                {
                    inRadian = true;
                    radDegSwitcher.sprite.Texture = new Texture("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Switcher_Rad.png");
                    Console.WriteLine("В радианах: true");
                }   
            }
            if(e.Button == Mouse.Button.Left)
            {
                // Показать отрезок синуса точки, если
                if (displaySinButton.sprite.GetGlobalBounds().Contains(mousePos))
                {
                    Point? p = circlePoints.ToList().FirstOrDefault(p => p.IsSelected == true);
                    if (sinLineOn == false)
                        Switch(p, SwitchSin, true);
                    else if (sinLineOn == true)
                        Switch(p, SwitchSin, false);
                }
                // Показать отрезок косинуса точки, если
                if (displayCosButton.sprite.GetGlobalBounds().Contains(mousePos))
                {
                    Point? p = circlePoints.ToList().FirstOrDefault(p => p.IsSelected == true);
                    if (cosLineOn == false)
                        Switch(p, SwitchCos, true);
                    else if (cosLineOn == true)
                        Switch(p, SwitchCos, false);
                }
                // Показать отрезок тангенса точки, если
                if (displayTgButton.sprite.GetGlobalBounds().Contains(mousePos))
                {
                    Point? p = circlePoints.ToList().FirstOrDefault(p => p.IsSelected == true);
                    if (tgLineOn == false)
                        Switch(p, SwitchTg, true);
                    else if (tgLineOn == true)
                        Switch(p, SwitchTg, false);
                }
                // Показать отрезок котангенса точки, если
                if (displayCtgButton.sprite.GetGlobalBounds().Contains(mousePos))
                {
                    Point? p = circlePoints.ToList().FirstOrDefault(p => p.IsSelected == true);
                    if (ctgLineOn == false)
                        Switch(p, SwitchCtg, true);
                    else if (ctgLineOn == true)
                        Switch(p, SwitchCtg, false);
                }
            }

        }
        static void MouseMovedHandler(object? sender, MouseMoveEventArgs e)
        { }
        static void WindowResizedHandler(object? sender, SizeEventArgs e)
        {
            view.Size = new Vector2f(window.Size.X, window.Size.Y);
        }
        static void TextEnteredHandler(object? sender, TextEventArgs e)
        {
            inputField.HandleInput(e.Unicode.ToString());
        }
        #endregion


        #region Методы отображения отрезков, соединяющих точку и её тригонометрические функции.
        
        delegate void SwitchFDelegate(Point? point, bool onOrOff);
        static void Switch(Point? point, SwitchFDelegate switchMethod, bool onOrOff)
        {
            if (switchMethod == SwitchSin)
                SwitchSin(point, onOrOff);
            else if (switchMethod == SwitchCos)
                SwitchCos(point, onOrOff);
            else if (switchMethod == SwitchTg)
                SwitchTg(point, onOrOff);
            else if (switchMethod == SwitchCtg)
                SwitchCtg(point, onOrOff);
        }

        static bool sinLineOn;
        static VertexArray? sinLine;
        static void SwitchSin(Point? point, bool onOrOff)
        {
            if (point is not null)
            {
                sinLine = new VertexArray(PrimitiveType.Lines, 2);

                if (onOrOff == true)
                {
                    sinLine[0] = new Vertex(point.Position, new Color(40, 255, 40));
                    sinLine[1] = new Vertex(new Vector2f(0, -circle.Radius * point.Sin), new Color(130, 255, 130));
                    sinLineOn = true;
                }
                else
                {
                    sinLine.Resize(0);
                    sinLine.Resize(2);
                    sinLineOn = false;
                }
            }
            else Console.WriteLine("Не выбрана точка.");
        }

        static bool cosLineOn;
        static VertexArray? cosLine;
        static void SwitchCos(Point? point, bool onOrOff)
        {
            if (point is not null)
            {
                cosLine = new VertexArray(PrimitiveType.Lines, 2);
                if (onOrOff == true)
                {
                    cosLine[0] = new Vertex(point.Position, new Color(255, 50, 50));
                    cosLine[1] = new Vertex(new Vector2f(circle.Radius * point.Cos, 0), new Color(255, 130, 130));
                    window.Draw(cosLine);
                    cosLineOn = true;
                }
                else
                {
                    cosLine.Resize(0);
                    cosLine.Resize(2);
                    cosLineOn = false;
                }
            }
            else Console.WriteLine("Не выбрана точка.");
        }

        static bool tgLineOn;
        static VertexArray? tgLine;
        static void SwitchTg(Point? point, bool onOrOff)
        {
            if (point is not null)
            {
                tgLine = new VertexArray(PrimitiveType.Lines, 2);
                if (onOrOff == true)
                {
                    tgLine[0] = new Vertex(new Vector2f(0, 0), new Color(180, 40, 180));
                    tgLine[1] = new Vertex(new Vector2f(circle.Radius, -circle.Radius * point.Tg), new Color(220, 80, 220));
                    window.Draw(tgLine);
                    tgLineOn = true;
                }
                else
                {
                    tgLine.Resize(0);
                    tgLine.Resize(2);
                    tgLineOn = false;
                }
            }
            else Console.WriteLine("Не выбрана точка.");
        }

        static bool ctgLineOn;
        static VertexArray? ctgLine;
        static void SwitchCtg(Point? point, bool onOrOff)
        {
            if (point is not null)
            {
                ctgLine = new VertexArray(PrimitiveType.Lines, 2);
                if (onOrOff == true)
                {
                    ctgLine[0] = new Vertex(new Vector2f(), new Color(100, 100, 220));
                    ctgLine[1] = new Vertex(new Vector2f(circle.Radius * point.Ctg, -circle.Radius), new Color(100, 100, 220));
                    window.Draw(ctgLine);
                    ctgLineOn = true;
                }
                else
                {
                    ctgLine.Resize(0);
                    ctgLine.Resize(2);
                    ctgLineOn = false;
                }
            }
            else Console.WriteLine("Не выбрана точка.");
        }
        #endregion
    }
}
