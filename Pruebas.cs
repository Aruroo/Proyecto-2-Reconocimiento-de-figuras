using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ProcesadorImagen.Pixel;
using ProcesadorImagen.Figura;
using ProcesadorImagen.ProcesadorImagen;

    ///[TestClass]
    public class PruebaPixel {

        ///[TestMethod]
        public void PruebaConstructorPixel() {
            Pixel pixel = new Pixel(1, 2, Color.Red);
            Assert.AreEqual(1, pixel.ObtenX());
            Assert.AreEqual(2, pixel.ObtenY());
            Assert.AreEqual(Color.Red, pixel.ObtenColor());
        }
    }


    ///[TestClass]
    public class PruebaFigura {

        ///[TestMethod]
        public void PruebaConstructorFigura() {
            List<Pixel> pixeles = new List<Pixel>();
            pixeles.Add(new Pixel(1, 2, Color.Red));
            pixeles.Add(new Pixel(2, 3, Color.Red));
            pixeles.Add(new Pixel(3, 4, Color.Red));
            Bitmap imagen = new Bitmap(5, 5);
            Figura figura = new Figura(pixeles, imagen);
            Assert.AreEqual(Color.Red, figura.ObtenColor());
        }

        ///[TestMethod]
        public void PruebaEncuentraCentro() {
            List<Pixel> pixeles = new List<Pixel>();
            pixeles.Add(new Pixel(1, 2, Color.Red));
            pixeles.Add(new Pixel(2, 3, Color.Red));
            pixeles.Add(new Pixel(3, 4, Color.Red));
            pixeles.Add(new Pixel(4, 5, Color.Red));
            Bitmap imagen = new Bitmap(5, 5);
            Figura figura = new Figura(pixeles, imagen);
            Assert.AreEqual(3, figura.EncuentraCentro().ObtenX());
            Assert.AreEqual(3, figura.EncuentraCentro().ObtenY());
        }

        ///[TestMethod]
        public void PruebaAgregaPixel() {
            List<Pixel> pixeles = new List<Pixel>();
            pixeles.Add(new Pixel(1, 2, Color.Red));
            Bitmap imagen = new Bitmap(5, 5);
            Figura figura = new Figura(pixeles, imagen);
            figura.AgregaPixel(new Pixel(2, 3, Color.Red));
            Assert.AreEqual(2, figura.ObtenPixeles().Count);
            figura.AgregaPixel(new Pixel(3, 4, Color.Red));
            Assert.AreEqual(3, figura.ObtenPixeles().Count);
            figura.AgregaPixel(new Pixel(4, 5, Color.Red));
            Assert.AreEqual(4, figura.ObtenPixeles().Count);
        }
    }

    ///[TestClass]
    public class PruebaProcesadorImagen {

        ///[TestMethod]
        public void PruebaRecorreImagen() {
            Bitmap imagen = new Bitmap(5, 5);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            List<Figura> figuras = procesador.RecorreImagen();
            Assert.AreEqual(1, figuras.Count);
        }

        ///[TestMethod]
        public void PruebaAnexaPixel() {
            Bitmap imagen = new Bitmap(5, 5);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            List<Figura> figuras = procesador.RecorreImagen();
            procesador.AnexaPixel(figuras, new Pixel(1, 2, Color.Red));
            Assert.AreEqual(2, figuras.Count);
        }
    }