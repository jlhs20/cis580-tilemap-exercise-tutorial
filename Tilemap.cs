using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TilemapExercise
{
    public class Tilemap
    {
        private int _tileWidth, _tileHeight, _mapWidth, _mapHeight;
        private Texture2D _tilesetTexture;
        private Rectangle[] _tiles;
        private int[] _map;
        private string _filename;

        public Tilemap(string filename)
        {
            _filename = filename;
        }

        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            string[] lines = data.Split('\n');
            
            //string tilesetFilename = lines[0].Trim();
            _tilesetTexture = content.Load<Texture2D>("tileset");
            
            string[] _tileSize = lines[0].Split(',');
            _tileWidth = int.Parse(_tileSize[0]);
            _tileHeight = int.Parse(_tileSize[1]);
            
            int tilesetColumns = _tilesetTexture.Width / _tileWidth;
            int tilesetRows = _tilesetTexture.Height / _tileHeight;
            _tiles = new Rectangle[tilesetColumns * tilesetRows];

            for (int y = 0; y < tilesetColumns; y++)
            {
                for (int x = 0; x < tilesetRows; x++)
                {
                    int index = y * tilesetColumns + x;
                    _tiles[index] = new Rectangle(x*_tileWidth, y*_tileHeight, _tileWidth, _tileHeight);
                }
            }

            string[] _mapSize = lines[1].Split(',');
            _mapWidth = int.Parse(_mapSize[0]);
            _mapHeight = int.Parse(_mapSize[1]);

            string[] indices = lines[2].Split(',');
            _map = new int[_mapWidth * _mapHeight];
            for (int i = 0; i < _mapHeight*_mapWidth; i++)
            {
                _map[i] = int.Parse(indices[i]);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    int index = _map[y*_mapWidth + x] - 1;
                    if (index == -1) continue;
                    spriteBatch.Draw(_tilesetTexture, new Vector2(x*_tileWidth, y*_tileHeight), _tiles[index], Color.White);
                }
            }
        }
    }
}