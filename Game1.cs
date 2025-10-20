using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_12_assignment5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Gear gear1;

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

            view = Matrix.CreateLookAt(
                new Vector3(0f, 50f, 5000f), Vector3.Zero, Vector3.Up);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Model bigModel = Content.Load<Model>("meshes/KlangBigGear");
            Model smallModel = Content.Load<Model>("meshes/KlangSmallGear");

            // First gear instance
            gear1 = new Gear(
                bigModel,
                smallModel,
                new Vector3(0, -50, -5000),
                new Vector3(200, 0, 0),
                50f,
                6f,
                new Vector3(-1050, 800, 100),
                -2000f, 2000f         
            );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Move and rotate gears
            gear1.Move(gameTime);
            gear1.Rotate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            gear1.Draw(view, projection);

            base.Draw(gameTime);
        }
    }
}
