using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// DashSliderHorizontal
    /// </summary>
    public class DashSliderHorizontal : DashSlider
    {
        #region Private Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of DashSliderHorizontal
        /// </summary>
        public DashSliderHorizontal()
            : base(Orientation.Horizontal)
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bounding rectangle for this slider.
        /// </summary>
        public override RectangleF Bounds
        {
            get
            {
                RectangleF result = new RectangleF();
                if (Image != null)
                {
                    result.X = Position.X - Image.Width / 2;
                    result.Y = 0;
                    result.Width = Image.Width;
                    result.Height = Image.Height;
                }
                else
                {
                    result.X = Position.X - Size.Width / 2;
                    result.Y = 0;
                    result.Width = Size.Width;
                    result.Height = Size.Height * 3 / 2;
                }
                return result;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Draws the dash slider
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        public override void Draw(Graphics g, Rectangle clipRectangle)
        {
            DrawHorizontal(g, clipRectangle);
        }

        #endregion

        #region Private Functions

        private void DrawHorizontal(Graphics g, Rectangle clipRectangle)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (Image != null)
            {
                g.DrawImage(Image, new PointF(Position.X - Image.Width / 2, 0));
            }
            else
            {
                float x = Position.X;
                float dx = Size.Width / 2;
                float dy = Size.Height;

                PointF[] trianglePoints = new PointF[4];
                trianglePoints[0] = new PointF(x, dy);
                trianglePoints[1] = new PointF(x - dx, 0);
                trianglePoints[2] = new PointF(x + dx, 0);
                trianglePoints[3] = new PointF(x, dy);
                LinearGradientBrush br = CreateGradientBrush(Color, new PointF(x - dx, 0), new PointF(x + dx, dy));
                g.FillPolygon(br, trianglePoints);
                br.Dispose();
                g.DrawPolygon(Pens.Black, trianglePoints);
            }
        }

        #endregion
    }
}