using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;

namespace WeatherAPP.Animations
{
    public class RainAnimation : GraphicsView
    {
        private readonly IDispatcherTimer _timer;
        private readonly Random _random = new();
        private readonly List<RainDrop> _drops = new();

        public RainAnimation()
        {
            Drawable = new RainDrawable(this);
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += (s, e) =>
            {
                UpdateDrops();
                Invalidate();
            };
            _timer.Start();

            // Создаем начальные капли
            for (int i = 0; i < 50; i++)
            {
                _drops.Add(new RainDrop
                {
                    X = _random.Next(0, 300),
                    Y = _random.Next(-100, 300),
                    Speed = _random.Next(5, 15),
                    Length = _random.Next(10, 20)
                });
            }
        }

        private void UpdateDrops()
        {
            foreach (var drop in _drops)
            {
                drop.Y += drop.Speed;
                if (drop.Y > 400)
                {
                    drop.Y = -20;
                    drop.X = _random.Next(0, 300);
                }
            }
        }

        private class RainDrawable : IDrawable
        {
            private readonly RainAnimation _parent;

            public RainDrawable(RainAnimation parent)
            {
                _parent = parent;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.FillColor = Colors.Gray;
                canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

                foreach (var drop in _parent._drops)
                {
                    canvas.StrokeColor = Colors.LightGray;
                    canvas.StrokeSize = 2;
                    canvas.DrawLine(drop.X, drop.Y, drop.X, drop.Y + drop.Length);
                }
            }
        }

        private class RainDrop
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Speed { get; set; }
            public float Length { get; set; }
        }
    }
} 