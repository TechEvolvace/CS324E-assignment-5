using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_12_assignment5
{
    // This class is for the entire Pokemon, Hoppip
    public class Hoppip
    {
        private Model model;
        private Vector3 position;
        private Head head;

        private float horizontalSpeed   = 2.0f;
        private float verticalSpeed     = 1.5f;
        private float verticalAmplitude = 100f; // Increased amplitude for scene scale

        private float elapsedTime    = 0f;
        private float verticalOffset = 0f;

        private float leftBoundary  = -2000f;
        private float rightBoundary =  2000f;

        public Hoppip(Model model, Vector3 startPosition)
        {
            this.model    = model;
            this.position = startPosition;
            this.head     = new Head();
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += dt;

            // Increased horizontal speed factor for visibility
            position.X += horizontalSpeed * dt * 200f; 
            
            if (position.X >= rightBoundary)
            {
                position.X  = leftBoundary;
                elapsedTime = 0f;
            }

            // Vertical oscillation (Floating/Bobbing)
            verticalOffset = (float)Math.Sin(elapsedTime * verticalSpeed) * verticalAmplitude;
            
            head.Update(gameTime);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            // FINAL MODIFIED SCALE: Set to 20f for visibility (was 0.05f originally)
            const float modelScale = 20f; 
            
            var orientationFix = Matrix.CreateRotationX(-MathHelper.PiOver2);
            var world = orientationFix
                      * Matrix.CreateScale(modelScale)
                      * Matrix.CreateTranslation(position.X, position.Y + verticalOffset, position.Z);

            foreach (ModelMesh mesh in model.Meshes)
            {
                string name = mesh.Name.ToLower();
                Vector3 tint;
                // Tinting logic from your friend's code
                if (name.Contains("leaf") || name.Contains("antenna"))
                    tint = new Vector3(0.4f, 0.8f, 0.4f);
                else if (name.Contains("eye"))
                    tint = Color.Black.ToVector3();
                else
                    tint = new Vector3(1f, 0.6f, 0.8f);

                foreach (BasicEffect e in mesh.Effects)
                {
                    // Hierarchical Transform: Apply head rotation *before* the main world transform
                    e.World = head.GetRotationMatrix() * world;
                    e.View = view;
                    e.Projection = projection;

                    // --- VISIBILITY FIXES ---
                    e.LightingEnabled = true; // IMPORTANT: Ensure lighting is ON
                    e.DiffuseColor = tint;
                    // Boosted Emissive for visibility against black background
                    e.EmissiveColor = tint * 3.0f; 
                    e.TextureEnabled = false;
                    
                    // Basic directional light
                    e.DirectionalLight0.Enabled = true;
                    e.DirectionalLight0.Direction = new Vector3(1, -1, -1);
                    e.DirectionalLight0.DiffuseColor = new Vector3(1f);
                }
                mesh.Draw();
            }
        }
    }
}
