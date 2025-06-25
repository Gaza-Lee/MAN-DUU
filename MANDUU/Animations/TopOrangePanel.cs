using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Animations;

public class TopOrangePanel : IDrawable
{
    public static TopOrangePanel Instance { get; } = new TopOrangePanel();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.FromArgb("#FF7B1C");

        float scaleX = dirtyRect.Width / 412f;
        float scaleY = dirtyRect.Height / 917f;

        var path = new PathF();

        path.MoveTo(0 * scaleX, 0 * scaleY);                    
        path.LineTo(412 * scaleX, 0 * scaleY);                    
        path.LineTo(412 * scaleX, 144.88f * scaleY);              

        path.CurveTo(
            351.917f * scaleX, 123.398f * scaleY,
            305.567f * scaleX, 144.88f * scaleY,
            305.567f * scaleX, 144.88f * scaleY);

        path.CurveTo(
            259.217f * scaleX, 166.363f * scaleY,
            246.056f * scaleX, 121.4f * scaleY,
            206f * scaleX, 144.88f * scaleY);

        path.CurveTo(
            165.944f * scaleX, 168.361f * scaleY,
            151.479f * scaleX, 116.838f * scaleY,
            112.728f * scaleX, 114.905f * scaleY);

        path.CurveTo(
            66.7832f * scaleX, 112.614f * scaleY,
            0 * scaleX, 144.88f * scaleY,
            0 * scaleX, 144.88f * scaleY);

        path.LineTo(0 * scaleX, 0 * scaleY);
        path.Close();

        canvas.FillPath(path);
    }
}
