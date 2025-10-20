using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_12_assignment5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Existing Gear fields
        private Gear gear1;

        // Whirlipede fields
        private Model _whirlipedeModel;
        private Whirlipede _whirlipede; 

        private float aspectRatio;
        private Matrix view;
        private Matrix projection;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f), aspectRatio, 1f, 10000f);

            // Set up a view that can see both the Gear and the Whirlipede
            view = Matrix.CreateLookAt(
                new Vector3(0f, 50f, 5000f), Vector3.Zero, Vector3.Up);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // --- Existing Gear Content Loading and Initialization ---
            Model bigModel = Content.Load<Model>("meshes/KlangBigGear");
            Model smallModel = Content.Load<Model>("meshes/KlangSmallGear");

            // First gear instance
            gear1 = new Gear(
                bigModel,
                smallModel,
                new Vector3(0, -50, -5000), // Start position
                new Vector3(200, 0, 0),    // Velocity (assuming this parameter means velocity/movement target)
                50f,                       // Rotation/Animation Speed
                6f,                        // Ease-in/Loop Time
                new Vector3(-1050, 800, 100), // Target position
                -2000f, 2000f              // Min/Max bounds (assuming)
            );
            // --------------------------------------------------------

            // --- Whirlipede Content Loading and Initialization ---
            _whirlipedeModel = Content.Load<Model>("meshes/whirlipede");

            // Load the Texture
            // NOTE: Ensure your texture file is compiled as "Textures/whirlipede.xnb"
            Texture2D whirlipedeTexture = Content.Load<Texture2D>("Textures/whirlipede"); 

            // Position adjusted to be lower (Y=0)
            Vector3 whirlipedeStartPos = new Vector3(2000, -1000, 0); 
            Vector3 whirlipedeTargetPos = new Vector3(-2000, -1000, 0); 
            float easeInTime = 3.0f;
            float animSpeed = 4.0f;
            float loopTime = 0f;
            float desiredScale = 50f; 

            // NOTE: Passing the texture as the 8th argument
            _whirlipede = new Whirlipede(
                _whirlipedeModel, 
                whirlipedeStartPos, 
                whirlipedeTargetPos, 
                easeInTime, 
                animSpeed, 
                loopTime,
                desiredScale,
                whirlipedeTexture // PASSING THE TEXTURE
            );
            // ----------------------------------------------------
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Existing Gear update calls
            gear1.Move(gameTime);
            gear1.Rotate(gameTime);

            // Whirlipede update call
            _whirlipede.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Existing Gear draw call
            gear1.Draw(view, projection);

            // Whirlipede draw call
            _whirlipede.Draw(view, projection);

            base.Draw(gameTime);
        }
    }
}