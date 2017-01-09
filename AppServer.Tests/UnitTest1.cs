using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AppServer.Tests
{
    [TestClass]
    public class SimpleUnitTests
    {
        

        private List<News> news = new List<News> {
            new News {likes = new Likes(5), comments = new Comments(2), reposts = new Reposts()},
            new News { likes = new Likes(15), comments = new Comments(0), reposts = new Reposts() },
            new News { likes = new Likes(56), comments = new Comments(5), reposts = new Reposts(9) },
            new News { likes = new Likes(2), comments = new Comments(15), reposts = new Reposts(25) }
        };

        [TestMethod]
        public void calculateCoefCorrectly()
        {
            var coef = Server.CalculateCoefficient(news);
            Assert.AreEqual(33, coef);
        }

        [TestMethod]
        public void parting_priorities()
        {
            var coef = Server.CalculateCoefficient(news);
            // Arrange (добавляем имитированный объект)
            Mock<News> mock = new Mock<News>();
            mock.Setup(m => m.GetPrioritet(It.IsAny<double>()))
                .Returns<double>(total => coef * 2);

            // Act   
            var result = mock.Object.GetPrioritet(coef);
            // Assert
            Assert.AreEqual(coef * 2, result,2);
        }

        [TestMethod]
        public void CheckCorrectGetNameById()
        {
            Mock<AppServer.Server> mock = new Mock<AppServer.Server>();
            
            mock.Setup(m => m.GetNameById(It.IsAny<int>()))
                 .Returns("Кукуруза Петровна");
            mock.Setup(m => m.GetNameById(It.Is<int>(v => v == 0)))
                .Returns("это я");
            mock.Setup(m => m.GetNameById(It.Is<int>(v => v > 100)))
                .Returns("Вася Пупкин");
            mock.Setup(m => m.GetNameById(It.IsInRange(10, 100,Range.Inclusive)))
                .Returns("Петя Васечкин");
                
            Assert.AreEqual("Кукуруза Петровна", mock.Object.GetNameById(7));
            Assert.AreEqual("Вася Пупкин", mock.Object.GetNameById(115));
            Assert.AreEqual("Петя Васечкин", mock.Object.GetNameById(80));
            Assert.AreEqual("это я", mock.Object.GetNameById(0));
        }
    }
}
