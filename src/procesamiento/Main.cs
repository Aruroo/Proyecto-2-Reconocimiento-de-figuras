using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using Pixels;
using ReconocimientoFiguras;
using Procesador;

public class MainClass {
        public static void Main(string[] args) {
            Console.WriteLine("Introduce la dirección de la imagen");
            try {
                string nombreImagen = Console.ReadLine();
                Bitmap imagen = new Bitmap(nombreImagen);
                ProcesadorImagen procesador = new ProcesadorImagen(imagen);
                List<Figura> figuras = procesador.ObtenFiguras();
                Console.WriteLine("La imagen tiene {0} figuras", figuras.Count);
                foreach (Figura figura in figuras) {
                    Console.WriteLine("{1} = {0}" , figura.ObtenNombre() , figura.ObtenColorHex());
                }
            } catch (System.Exception) {
                Console.WriteLine("Error al leer la imagen, por favor intenta de nuevo.");
                Main(args);
            }
        }
}