using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_12_assignment5
{
    public class Gear : Klang
    {
        private Model smallModel;
        public Vector3 Offset { get; set; }
        public float RotationZ { get; private set; }
        public float AngularVelocity { get; set; }

        public Gear(
            Model bigModel,
            Model smallModel,
            Vector3 position,
            Vector3 speed,
            float scale,
            float angularVelocity,
            Vector3 offset,
            float xMin,
            float xMax
        ) : base(bigModel, position, speed, scale, xMin, xMax)
        {
            this.smallModel = smallModel;
            Offset = offset;
            AngularVelocity = angularVelocity;
            RotationZ = 0f;
        }

        public void Rotate(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            RotationZ += AngularVelocity * delta;
        }

        private Matrix GetSmallGearWorld()
        {
            return Matrix.CreateScale(Scale) *
                   Matrix.CreateRotationX(MathHelper.ToRadians(-90)) *
                   Matrix.CreateRotationZ(-RotationZ) *
                   Matrix.CreateTranslation(Position + Offset);
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            Matrix bigWorld = Matrix.CreateScale(Scale) *
                              Matrix.CreateRotationX(MathHelper.ToRadians(-90)) *
                              Matrix.CreateRotationZ(RotationZ) *
                              Matrix.CreateTranslation(Position);

            Matrix[] transforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (var mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.EmissiveColor = new Vector3(0.6f, 0.6f, 0.6f);
                    effect.World = bigWorld * transforms[mesh.ParentBone.Index];
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }

            Matrix smallWorld = GetSmallGearWorld();
            Matrix[] smallTransforms = new Matrix[smallModel.Bones.Count];
            smallModel.CopyAbsoluteBoneTransformsTo(smallTransforms);

            foreach (var mesh in smallModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.EmissiveColor = new Vector3(0.6f, 0.6f, 0.6f);
                    effect.World = smallWorld * smallTransforms[mesh.ParentBone.Index];
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
    }
}

