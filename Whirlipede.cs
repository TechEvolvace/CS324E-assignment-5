using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_12_assignment5
{
    public class Whirlipede
    {
        private Model whirlipedeModel;
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Vector3 currentPosition;
        
        private float rotationAngle;
        private float animationSpeed;
        private float easeInDuration;
        private float currentTime;
        private float Scale; 
        private Texture2D texture; // Added Texture field

        public Whirlipede(Model model, Vector3 startPos, Vector3 targetPos, float easeInTime, float animSpeed, float loopTime, float scale, Texture2D tex)
        {
            whirlipedeModel = model;
            startPosition = startPos;
            targetPosition = targetPos;
            currentPosition = startPos;
            easeInDuration = easeInTime;
            animationSpeed = animSpeed;
            
            rotationAngle = 0f;
            currentTime = 0f;
            Scale = scale; 
            texture = tex; // Initialized Texture
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculate ease-in progress using quadratic ease-in
            float t = MathHelper.Clamp(currentTime / easeInDuration, 0f, 1f);
            float easedT = t * t; 

            // Update current position using Lerp
            currentPosition = Vector3.Lerp(startPosition, targetPosition, easedT);

            // Check if the Whirlipede has reached the target position
            if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
            {
                // Restart movement: Reset time and position
                currentTime = 0f;
                currentPosition = startPosition;
            }
            else
            {
                // Continue updating time for movement
                currentTime += deltaTime;
            }

            // Continuous rotation: Update rotation angle for spinning
            rotationAngle += animationSpeed * deltaTime;

            // Keep rotation angle bounded between 0 and 2π
            if (rotationAngle > MathHelper.TwoPi)
            {
                rotationAngle -= MathHelper.TwoPi;
            }
            else if (rotationAngle < 0)
            {
                rotationAngle += MathHelper.TwoPi;
            }
        }

        public void Draw(Matrix view, Matrix projection)
        {
            if (whirlipedeModel == null)
                return;

            Matrix scaleMatrix = Matrix.CreateScale(Scale);
            
            // X-rotation to orient model, Z-rotation for rolling
            Matrix rotation = Matrix.CreateRotationX(MathHelper.PiOver2) * Matrix.CreateRotationZ(rotationAngle);

            Matrix world = scaleMatrix * rotation * Matrix.CreateTranslation(currentPosition);

            foreach (ModelMesh mesh in whirlipedeModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    
                    // --- Manual Texture Assignment ---
                    effect.TextureEnabled = true;
                    effect.Texture = texture;
                    // ---------------------------------
                    
                    // Adjust lighting configuration
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    
                    effect.AmbientLightColor = new Vector3(0.4f, 0.4f, 0.4f);
                    
                    // Directional light settings
                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.Direction = new Vector3(-1, -1, -1);
                    effect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f);
                    effect.DirectionalLight0.SpecularColor = new Vector3(1.0f, 1.0f, 1.0f);

                    // Optional secondary light source
                    effect.DirectionalLight1.Enabled = true;
                    effect.DirectionalLight1.Direction = new Vector3(1, -1, 0);
                    effect.DirectionalLight1.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);

                    effect.SpecularPower = 64f;
                }
                mesh.Draw();
            }
        }
        
        public Vector3 Position => currentPosition;
    }
}