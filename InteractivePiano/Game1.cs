using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PianoSimulation;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        private List<PianoKeySprite> pianoKeys;
        public InteractivePianoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            piano = new Piano("q2we4r5ty7u8i9op-[=");
            audio = Audio.Instance;
            newKey = false;
            pianoKeys = new List<PianoKeySprite>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int incrementPos = 0;
            char[] pianoChars = piano.Keys.ToCharArray();
            for(int i =0; i<pianoChars.Length; i++) {
                PianoKeySprite ps;
                if(i % 2 == 0) {
                    ps = new PianoKeySprite(this, pianoChars[i], "white", incrementPos, 0, 200, 200);
                } else {
                    ps =new PianoKeySprite(this, pianoChars[i], "black", incrementPos, 0, 200, 200);
                    incrementPos += ps.Width / 4; 
                }
                pianoKeys.Add(ps);
                Components.Add(ps);
            }

            // PianoKeySprite ps = new PianoKeySprite(this, 'q', "white", 0, 0, 200, 200);
            // PianoKeySprite ss = new PianoKeySprite(this, 'q', "black", 0, 0, 200, 200);
            // PianoKeySprite ds = new PianoKeySprite(this, 'q', "white", 50, 0, 200,200);
            // PianoKeySprite ks = new PianoKeySprite(this, 'q', "white", 100, 0, 200,200);
            // Components.Add(ps);
            // Components.Add(ss);
            // Components.Add(ds);
            // Components.Add(ks);

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
