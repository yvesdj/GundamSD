using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.StateManagement
{
    public class GameStateManager
    {
        private static GameStateManager _instance;

        private Stack<GameState> _states = new Stack<GameState>();

        private ContentManager _content;

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        public void SetContent(ContentManager content)
        {
            _content = content;
        }

        public void AddState(GameState gameState)
        {
            _states.Push(gameState);
            _states.Peek().Initialize();

            if (_content != null)
            {
                _states.Peek().LoadContent(_content);
            }
        }

        public void RemoveState()
        {
            if (_states.Count > 0)
            {
                GameState gameState = _states.Peek();
                _states.Pop();
            }
        }

        public void ClearStates()
        {
            while (_states.Count > 0)
            {
                _states.Pop();
            }
        }

        public void ChangeState(GameState gameState)
        {
            //ClearStates();
            AddState(gameState);
        }

        public void Update(GameTime gameTime)
        {
            if (_states.Count > 0)
            {
                _states.Peek().Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_states.Count > 0)
            {
                _states.Peek().Draw(spriteBatch);
            }
        }

        public void UnloadContent()
        {
            _content.Unload();
            foreach (GameState gameState in _states)
            {
                gameState.UnloadContent();
            }
        }
    }
}
