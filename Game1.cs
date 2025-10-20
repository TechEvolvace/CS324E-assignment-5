using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System; 

namespace group_12_assignment5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Gear gear1;

        private Model _whirlipedeModel;
        private Whirlipede _whirlipede; 
        
        private Model _hoppipModel;
        private Hoppip _hoppip; 

        private float aspectRatio;
        private Matrix view;
        private Matrix projection;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f), aspectRatio, 1f, 10000f);

            view = Matrix.CreateLookAt(
                new Vector3(0f, 50f, 5000f), Vector3.Zero, Vector3.Up);

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Model bigModel = Content.Load<Model>("meshes/KlangBigGear");
            Model smallModel = Content.Load<Model>("meshes/KlangSmallGear");

            gear1 = new Gear(
                bigModel,
                smallModel,
                new Vector3(0, -50, -5000),
                new Vector3(200, 0, 0),   
                50f, 6f,                       
                new Vector3(-1050, 800, 100), 
                -2000f, 2000f              
            );

            _whirlipedeModel = Content.Load<Model>("meshes/whirlipede");

            Texture2D whirlipedeTexture = Content.Load<Texture2D>("Textures/whirlipede"); 

            Vector3 whirlipedeStartPos = new Vector3(2000, -1000, 0); 
            Vector3 whirlipedeTargetPos = new Vector3(-2000, -1000, 0); 
            float desiredScale = 50f; 

            _whirlipede = new Whirlipede(
                _whirlipedeModel, 
                whirlipedeStartPos, 
                whirlipedeTargetPos, 
                3.0f, 4.0f, 0f,
                desiredScale,
                whirlipedeTexture 
            );
            
            _hoppipModel = Content.Load<Model>("meshes/tinker");
            
            Vector3 hoppipStartPos = new Vector3(0, 50, 0); 
            
            _hoppip = new Hoppip(_hoppipModel, hoppipStartPos);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gear1.Move(gameTime);
            gear1.Rotate(gameTime);

            _whirlipede.Update(gameTime);
            
            _hoppip.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

            gear1.Draw(view, projection);

            _whirlipede.Draw(view, projection);
            
            _hoppip.Draw(view, projection);

            base.Draw(gameTime);
        }
    } 
    
}