using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.StateManagement
{
    public abstract class GameState : IGameState
    {
        protected GraphicsDevice _graphicsDevice;
        protected GraphicsDeviceManager _graphicsDeviceManager;

        public GameState(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphicsDevice = graphicsDevice;
            _graphicsDeviceManager = graphicsDeviceManager;
        }

        public abstract void Initialize();

        public abstract void LoadContent(ContentManager content);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
