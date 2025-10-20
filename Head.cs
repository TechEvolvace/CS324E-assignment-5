using Microsoft.Xna.Framework;
using System;

namespace group_12_assignment5
{
    public class Head
    {
        private float rotationSpeed  = 1.5f;
        private float currentRotation = 0f;

        public void Update(GameTime gameTime)
        {
            currentRotation = (currentRotation + rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                            % MathHelper.TwoPi;
        }

        public Matrix GetRotationMatrix()
        {
            return Matrix.CreateRotationY(currentRotation);
        }
    }
}
