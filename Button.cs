using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Trigonometry_Calculator
{
    // Кнопка будет содержать спрайт, (события) и поведение, определяемое для всех кнопок. Все кнопки будут статичными относительно окна.
    class Button : Transformable, Drawable
    {
        public Sprite sprite = new Sprite(new Texture("C:\\Users\\dsark\\OneDrive\\Рабочий стол\\Проект\\Визуальный тригонометрический калькулятор\\Кнопки\\Default_Texture.png"));
        public new Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public IntRect TextureRect
        {
            get => sprite.TextureRect;
            set => sprite.TextureRect = value;
        }


        public Button()
        {
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
        }
        public Button(Sprite sprite) : this()
        {
            this.sprite = sprite;
        }
        public Button(string textureFile) : this()
        {
            sprite.Texture = new Texture(textureFile);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }

}
