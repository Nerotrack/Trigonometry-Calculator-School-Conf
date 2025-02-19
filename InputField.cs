using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Trigonometry_Calculator
{
    class InputField
    {
        static private RectangleShape _rectangle = new RectangleShape();
        private Text _text;
        private Font _font;

        public float Width => _rectangle.Size.X;
        public float Height => _rectangle.Size.Y;
        public Vector2f Position
        {
            get => _rectangle.Position;
            set
            {
                _rectangle.Position = value;
                _text.Position = value + new Vector2f(-_rectangle.Size.X / 2 + 1, - _rectangle.Size.Y / 2 - 2);
            }
        }
        public Vector2f Scale
        {
            get => _rectangle.Scale;
            set
            {
                _rectangle.Scale = value;
                _text.Scale = value;
            }
        }
        public Text Text
        {
            get => _text;
            set
            {
                _text = value;
            }
        }

        public InputField(Vector2f position, Vector2f size, Font font)
        {
            _rectangle = new RectangleShape(size)
            {
                Position = position,
                Origin = new Vector2f(size.X / 2, size.Y / 2),
                FillColor = Color.White,
                OutlineColor = Color.Black,
                OutlineThickness = 1
            };

            _font = font;
            _text = new Text(string.Empty, _font, 24);
            _text.FillColor = Color.Black;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(_rectangle);
            window.Draw(_text);
        }

        public void HandleInput(string input)
        {
            // Проверка, что вводимые символы - это цифры
            if (IsNumericOrPoint(input))
            {
                // Обновляем текст, если он не превышает размер поля
                if (_text.DisplayedString.Length < MaxCharacterCount())
                {
                    _text.DisplayedString += input;
                }
            }
        }

        public void Backspace()
        {
            if (_text.DisplayedString.Length > 2)
            {
                if (_text.DisplayedString[^1] == '2' && _text.DisplayedString[^2] == '√')
                    _text.DisplayedString = _text.DisplayedString.Substring(0, _text.DisplayedString.Length - 2);
                if (_text.DisplayedString[^1] == '3' && _text.DisplayedString[^2] == '√')
                    _text.DisplayedString = _text.DisplayedString.Substring(0, _text.DisplayedString.Length - 2);
            }
            if (_text.DisplayedString.Length > 0)
            {
                _text.DisplayedString = _text.DisplayedString.Substring(0, _text.DisplayedString.Length - 1);
            }
        }

        private bool IsNumericOrPoint(string input)
        {
            return int.TryParse(input, out _) ||
                input.Contains(' ') ||
                input.Contains('.') || // При необходимости поменять на запятую.
                input.Contains('+') ||
                input.Contains('-') ||
                input.Contains('*') ||
                input.Contains('/') ||
                input.Contains('(') ||
                input.Contains(')') ||
                input.Contains('π') || 
                input.Contains("√2") ||
                input.Contains("√3");

        }

        private int MaxCharacterCount()
        {
            return (int)(Width / 13); // 13 - приблизительная ширина одной цифры
        }

        public void Clear()
        {
            _text.DisplayedString = string.Empty;
        }
    }
}
