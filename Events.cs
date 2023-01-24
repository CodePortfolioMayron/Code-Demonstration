using System;
using System.Collections.Generic;
using System.Text;

namespace UnrivaledPractise
{
    class Events
    {

        public void Initialize()
        {

        }

        public void Update(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite.Health == 0)
                {
                    if (sprite.PlayerNum == 1)
                    {

                    }
                }
            }
        }

        public void Draw()
        {

        }
    }
}
