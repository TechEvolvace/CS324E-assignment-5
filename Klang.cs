using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_12_assignment5
{
    public class Klang
    {
        public Vector3 Position { get; set; }
        public Vector3 Speed { get; set; }
        public float Scale { get; set; }
        protected Model Model;
        private Vector3 OriginalPosition;
        private float XMin, XMax;

        public Klang(Model model, Vector3 position, Vector3 speed, float scale, float xMin, float xMax)
        {
            Model = model;
            Position = position;
            OriginalPosition = position;
            Speed = speed;
            Scale = scale;
            XMin = xMin;
            XMax = xMax;
        }

        public virtual void Move(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Speed * delta;

            if (Position.X > XMax || Position.X < XMin)
                Position = OriginalPosition;
        }

        public virtual void Draw(Matrix view, Matrix projection)
        {
            Matrix world = Matrix.CreateScale(Scale) *
                           Matrix.CreateRotationX(MathHelper.ToRadians(-90)) *
                           Matrix.CreateTranslation(Position);

            Matrix[] transforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (var mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.EmissiveColor = new Vector3(0.6f, 0.6f, 0.6f);
                    effect.World = world * transforms[mesh.ParentBone.Index];
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
    }
}

