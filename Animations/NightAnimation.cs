using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;

namespace WeatherAPP.Animations
{
    public class NightAnimation : GraphicsView
    {
        private readonly IDispatcherTimer _timer;
        private float _starOpacity = 0.5f;
        private bool _increasing = true;

        public NightAnimation()
        {
            Drawable = new NightDrawable(this);
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += (s, e) =>
            {
                if (_increasing)
                {
                    _starOpacity += 0.1f;
                    if (_starOpacity >= 1.0f)
                        _increasing = false;
                }
                else
                {
                    _starOpacity -= 0.1f;
                    if (_starOpacity <= 0.5f)
                        _increasing = true;
                }
                Invalidate();
            };
            _timer.Start();
        }

        private class NightDrawable : IDrawable
        {
            private readonly NightAnimation _parent;

            public NightDrawable(NightAnimation parent)
            {
                _parent = parent;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                // Рисуем небо
                canvas.FillColor = Colors.Black;
                canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

                // Рисуем звезды
                var random = new Random();
                for (int i = 0; i < 50; i++)
                {
                    float x = random.Next(0, (int)dirtyRect.Width);
                    float y = random.Next(0, (int)dirtyRect.Height);
                    float size = random.Next(1, 3);

                    canvas.FillColor = Colors.White.WithAlpha(_parent._starOpacity);
                    canvas.FillCircle(x, y, size);
                }

                // Рисуем луну
                canvas.FillColor = Colors.White;
                canvas.FillCircle(dirtyRect.Width * 0.8f, dirtyRect.Height * 0.2f, 40);
            }
        }
    }
} 