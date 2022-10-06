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
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

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
      //_spriteBatch.Draw(_keyTexture, new Vector2(_x, _y), Color.White);
      _spriteBatch.Draw(_keyTexture, new Rectangle(X, Y, Width, Height), Color.White);
      _spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}