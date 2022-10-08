using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PianoSimulation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

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
            piano = new Piano();
            audio = Audio.Instance;
            newKey = false;
            pianoKeys = new List<PianoKeySprite>();
        }


        private bool isBlack(int i) {
            if(i == 1 || i == 4 || i == 6 || i == 9 || i == 11 || i == 13 || i == 16 || i == 18 || i == 21 || 
                i == 23 || i == 25 || i == 28 || i == 30 || i == 33 || i == 35) 
            {
                return true;
            }
            return false;
        }

        protected override void Initialize()
        {


            // TODO: Add your initialization logic here
            int incrementPos = 0;
            char[] pianoChars = piano.Keys.ToCharArray();
            for(int i =0; i<pianoChars.Length; i++) {
                PianoKeySprite ps;
                if(isBlack(i)) {
                    ps = new PianoKeySprite(this, pianoChars[i], "black", incrementPos, 0, 200, 200);
                    incrementPos += ps.Width / 4;
                } else {
                    ps = new PianoKeySprite(this, pianoChars[i], "white", incrementPos, 0, 200, 200);
                    if(!(isBlack(i + 1))) {
                        incrementPos += ps.Width / 4;
                    } 
                }
                pianoKeys.Add(ps);
                Components.Add(ps);
            }


            Task t = new Task(() => {
                while(true) {
                    if(newKey) {
                        audio.Reset();
                    }
                    audio.Play(piano.Play());
                }
            });

            t.Start();

            base.Initialize();

            _graphics.PreferredBackBufferHeight =  GraphicsDevice.DisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
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
                    foreach(PianoKeySprite key in pianoKeys) {
                        key.Release();
                    }
                    newKey = true;
                    Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
                    foreach(Keys key in pressedKeys) {
                        //Debug.WriteLine(key.ToString().ToLower());
                        //char hitKey = key.ToString().ToLower()[key.ToString().Length - 1];
                        char hitKey = determineCharEqv(key.ToString()); 
                        Debug.WriteLine(key.ToString());
                        try {
                            piano.StrikeKey(hitKey);
                        } catch(ArgumentException e) {
                            Debug.WriteLine(e);
                        }
                        foreach(PianoKeySprite pressKey in pianoKeys) {
                            if(pressKey.Note == hitKey) {
                                pressKey.Press();
                            } 
                        }
                    }
                }
                else {
                    newKey = false;
                }
            } else {
                foreach(PianoKeySprite key in pianoKeys) {
                    key.Release();
                }
            }
            prevKeyboard = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public char determineCharEqv(string key) {
            if(key == "OemMinus") {
                return '-';
            } else if(key == "OemOpenBrackets") {
                return '[';
            } else if(key == "OemPlus") {
                return '=';
            } else if(key == "OemPeriod") {
                return '.';
            } else if(key == "OemSemicolon") {
                return ';';
            } else if(key == "OemQuestion") {
                return '/';
            } else if(key == "OemQuotes") {
                return "'"[0];
            } else if(key == "Space") {
                return ' ';
            } else if(key == "OemComma") {
                return ',';
            } else if((new Regex(@"^[D]\d", RegexOptions.Compiled)).IsMatch(key)) {
                return key[1];
            } else if(key.Length > 1) {
                return '3';
            }

            return key.ToString().ToLower()[0];
        }

    }
}
