using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pixels;
using System.Drawing;

[TestClass]
    public class PruebaPixel {

        [TestMethod]
        public void PruebaConstructorPixel() {
            Pixel pixel = new Pixel(1, 2, Color.Red);
            Assert.AreEqual(1, pixel.ObtenX());
            Assert.AreEqual(2, pixel.ObtenY());
            Assert.AreEqual(Color.Red, pixel.ObtenColor());
        }
    }