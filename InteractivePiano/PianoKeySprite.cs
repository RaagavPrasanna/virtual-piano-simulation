using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace InteractivePiano {
  public class PianoKeySprite : DrawableGameComponent {
    private InteractivePianoGame _game;
    private SpriteBatch _spriteBatch;
    public char Note {get;}
    public string KeyColor {get; private set;}
    private Texture2D _keyTexture;
    public int X {get;}
    public int Y {get;}
    public int Width{get;}
    public int Height {get;}
    private bool _pressed;
    private SpriteFont _font;
    private const string charKeys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ";
    private readonly string[] noteLetters = new string[]{"A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#"};

    public PianoKeySprite(InteractivePianoGame game, char note, string color, int x, int y, int width, int height): base(game){
      _game = game;
      Note = note;
      if(color != "black" && color != "white") {
        throw new ArgumentException("Invalid color");
      }
      KeyColor = color;
      X = x;
      Y = y;

      Width = width;
      Height = height;
      _pressed = false;
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      _font = _game.Content.Load<SpriteFont>("Note");

      if(KeyColor == "white") {
        _keyTexture = _game.Content.Load<Texture2D>("White_Piano_Key");
      } else if(KeyColor == "black") {
        _keyTexture = _game.Content.Load<Texture2D>("Black_Piano_Key");
      }
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
   {  
      _spriteBatch.Begin();
      if(_pressed) {
        string noteLetter = determineNoteLetter();
        _spriteBatch.Draw(_keyTexture, new Rectangle(X, Y, Width, Height), Color.Red);
        if(KeyColor == "white") {
          _spriteBatch.DrawString(_font, noteLetter, new Vector2(X + 80, Y + 150), Color.Black);
        } else {
          _spriteBatch.DrawString(_font, noteLetter, new Vector2(X + 80, Y + 110), Color.Black);
        }
      } else {
        _spriteBatch.Draw(_keyTexture, new Rectangle(X, Y, Width, Height), Color.White);
      }
      _spriteBatch.End();
      base.Draw(gameTime);
    }

    public void Press() { 
      _pressed = true;
    }
    public void Release() {
      _pressed = false;
    }

    private string determineNoteLetter() {
      for(int i =0; i < charKeys.Length; i++) {
        if(Note == charKeys[i]) {
          return noteLetters[i % noteLetters.Length];
        }
      }
      return "";
    }
   }
}