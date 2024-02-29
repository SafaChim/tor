using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace Project1
{
    public class Game : GameWindow
    {
        private Color4 firstColor, CircleColor;
        private float colorstepG = 0.00004f;
        private float colorstepB = 0.000015f;
        double theta = 0.0;

        float frameTime = 0.0f;
        int fps = 0;
        public Game(int w, int h, string title) : base(w, h, GraphicsMode.Default, title)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            firstColor = new Color4(0.2f, 0.3f, 0.3f, 0.5f);
            GL.ClearColor(firstColor);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            frameTime += (float)e.Time;
            fps++;
            if(frameTime >= 1.0f)
            {
                Title = $" Donat, ur FPS = {fps}";
                frameTime = 0.0f;
                fps = 0;
            }

            firstColor.G += colorstepG;
            firstColor.B -= colorstepB;
            if (firstColor.G > 1 || firstColor.G < 0.0f)
            {
                colorstepG = -colorstepG;
            }
            if (firstColor.B > 1 || firstColor.B < 0.0f)
            {
                colorstepB = -colorstepB;
            }

            GL.ClearColor(firstColor);
            base.OnUpdateFrame(e);
        }

        protected void DrawCircle(float x_pos, float y_pos, float rad)
        {
            GL.LineWidth(2f);
            CircleColor = new Color4(0.6f, 0.4f, 0.1f, 0.9f);
            GL.Color4(CircleColor);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // Сброс текущей матрицы
            
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 600/600, 1f, 100f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadIdentity();
            GL.Translate(0, 0, -12.0f);
            GL.Rotate(theta, 1, 0, 0);
            GL.Rotate(theta, 1, 0, 1);

            GL.Begin(PrimitiveType.LineLoop);

            int gridSize = 60;
            double start = 0;
            double end = 2f * Math.PI;

            double[] u = Enumerable.Range(0, gridSize).Select(i => start + (end - start) * i / (gridSize - 1)).ToArray();
            double[] v = Enumerable.Range(0, gridSize).Select(i => start + (end - start) * i / (gridSize - 1)).ToArray();


            foreach (double i in u)
            {

                foreach (double j in v)
                {
                    double x = (3 + Math.Sin(i))* Math.Cos(j);
                    double y = (3 + Math.Sin(i))* Math.Sin(j);
                    double z = Math.Cos(i);
                    GL.Vertex3(x, y, z);
                }
            }

            GL.End();
            GL.Disable(EnableCap.Blend);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            DrawCircle(0f , 0f, 2f);



            SwapBuffers();
            theta += 0.025;
            if (theta > 360)
            {
                theta = -360;
            }

            base.OnRenderFrame(e);
        }

    }


    class Class1
    {
        public static void Main(string[] args)
        {
            int width = 800;
            int height = 800;
            string title = "donut";
            using (Game game = new Game(width, height, title))
            {
                game.Run();
            }

        }
    }
}
