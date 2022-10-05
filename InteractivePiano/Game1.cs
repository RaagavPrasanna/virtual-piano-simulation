using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PianoSimulation;
using System.Threading.Tasks;

namespace InteractivePiano
{
    public class InteractivePianoGame : Game
    {
        private bool newKey;
        private KeyboardState prevKeyboard;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Audio audio;

        private Piano piano; 

        public InteractivePianoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            piano = new Piano();
            audio = Audio.Instance;
            newKey = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //     Debug.WriteLine("entered");
            //     Piano p = new Piano();
            //     using(var audio = Audio.Instance) {
            //     p.StrikeKey('q');
                // for(int i =0; i< 44100; i++) {
                //     audio.Play(p.Play());
                // }
            // }

            Task t = new Task(() => {
                while(true) {
                    if(newKey) {
                        Debug.WriteLine("new Key pressed");
                        audio.Reset();
                    }
                    audio.Play(piano.Play());
                }
            });

            t.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            if(Keyboard.GetState().GetPressedKeyCount() > 0) {
                if(prevKeyboard != Keyboard.GetState()) {
                    newKey = true;
                    Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
                    foreach(Keys key in pressedKeys) {
                        piano.StrikeKey(key.ToString().ToLower()[key.ToString().Length - 1]);
                    }
                }
                else {
                    newKey = false;
                }
            }
            prevKeyboard = Keyboard.GetState();
            // using(var audio = Audio.Instance) {
                // var audio = Audio.Instance;
                // for(int i =0; i< 44100 * 3; i++) {
                //     audio.Play(piano.Play());
                // }
                // Debug.WriteLine(piano.Play());
            // }
                

            // TODO: Add your update logic here
            // if(Keyboard.GetState().GetPressedKeys().Length != 0) {
            //     Debug.WriteLine(Keyboard.GetState().GetPressedKeys()[0].ToString().ToLower().ToCharArray()[0]);
            // }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
