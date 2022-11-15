using System;

namespace ReconocimientoFiguras

{

    public class Pixel

    {

        private int x;

        private int y;

        private Color color;
        
        /// <summary>
        /// Constructor de la clase Pixel
        /// </summary>
        /// <param name="x">Coordenada x del pixel</param>
        /// <param name="y">Coordenada y del pixel</param>
        /// <param name="color">Color del pixel</param>
        public Pixel(int x, int y, Color color)
        {

            this.x = x;

            this.y = y;

            this.color = color;

        }

        public Color getColor(){
            return color;
        }

        /// <summary>
        /// Comparador de igualdad entre dos pixeles
        /// </summary>
        /// <param name="obj">Pixel a comparar</param>
        /// <returns>True si son iguales, false si no lo son</returns>

        public boolean mismoColor(Pixel pixel)
        {

            return this.color == pixel.color;

        }

        public int getX(){
            return x;
        }

        public int getY(){
            return y;
        }



    }

}