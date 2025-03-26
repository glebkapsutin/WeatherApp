using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;

namespace WeatherAPP.Animations
{
    public class SunAnimation : GraphicsView
    {
        private readonly IDispatcherTimer _timer;
        private float _angle = 0;

        public SunAnimation()
        {
            Drawable = new SunDrawable(this);
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += (s, e) =>
            {
                _angle += 5;
                if (_angle >= 360)
                    _angle = 0;
                Invalidate();
            };
            _timer.Start();
        }

        private class SunDrawable : IDrawable
        {
            private readonly SunAnimation _parent;

            public SunDrawable(SunAnimation parent)
            {
                _parent = parent;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();
                canvas.Translate(dirtyRect.Center.X, dirtyRect.Center.Y);
                canvas.Rotate(_parent._angle, 0, 0);

                // Рисуем солнце
                canvas.FillColor = Colors.Yellow;
                canvas.FillCircle(0, 0, 50);

                // Рисуем лучи
                for (int i = 0; i < 12; i++)
                {
                    float angle = i * 30;
                    canvas.Rotate(angle, 0, 0);
                    canvas.FillColor = Colors.Yellow;
                    canvas.FillRectangle(-5, -60, 10, 40);
                }

                canvas.RestoreState();
            }
        }
    }
} 