using System;
using System.Collections.Generic;
using ConsoleMastermindGame.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleMastermindGameUnitTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestInitialise()
        {
            var maximumTries = 4;
            var elementsToGuess = 6;
            var game = new Game(maximumTries, elementsToGuess);
            Assert.AreEqual(game.GetMaximumTries(), maximumTries);
            Assert.AreEqual(game.GetElementsToGuess(), elementsToGuess);

            maximumTries = -2;
            elementsToGuess = -1;
            game = new Game(maximumTries, elementsToGuess);
            Assert.AreEqual(game.GetMaximumTries(), 1);
            Assert.AreEqual(game.GetElementsToGuess(), 1);
        }

        [TestMethod]
        public void TestGenerateCode()
        {
            List<String> colorList = new List<String>() { "R", "J", "B", "O", "V", "N" };

            var maximumTries = 4;
            var elementsToGuess = 6;
            var game = new Game(maximumTries, elementsToGuess);
            game.GenerateSecretCode();
            var secretCode = game.GetSecretCode();
            Assert.IsTrue(!string.IsNullOrEmpty(secretCode));
            Assert.AreEqual(secretCode.Length, elementsToGuess);
            foreach(var codePart in secretCode)
            {
                Assert.IsTrue(colorList.Contains(codePart.ToString()));
            }

            maximumTries = -1;
            elementsToGuess = -1;
            game = new Game(maximumTries, elementsToGuess);
            game.GenerateSecretCode();
            secretCode = game.GetSecretCode();
            Assert.IsTrue(!string.IsNullOrEmpty(secretCode));
            Assert.AreEqual(secretCode.Length, 1);
            foreach (var codePart in secretCode)
            {
                Assert.IsTrue(colorList.Contains(codePart.ToString()));
            }
        }

        [TestMethod]
        public void TestGetInPlaceCount()
        {
            var maximumTries = 4;
            var elementsToGuess = 6;
            var game = new Game(maximumTries, elementsToGuess);
            game.GenerateSecretCode();
            var count = game.GetInPlaceCount("TEST");
            Assert.AreEqual(count, 0);

            var secretCode = game.GetSecretCode();
            count = game.GetInPlaceCount(secretCode);
            Assert.AreEqual(count, 6);
            count = game.GetInPlaceCount(secretCode.Substring(0, 3));
            Assert.AreEqual(count, 3);
        }

        [TestMethod]
        public void TestGetOutOfPlacedCount()
        {
            var maximumTries = 4;
            var elementsToGuess = 6;
            var game = new Game(maximumTries, elementsToGuess);
            game.GenerateSecretCode();
            var count = game.GetOutOfPlaceCount("TEST");
            Assert.AreEqual(count, 0);

            var secretCode = game.GetSecretCode();
            count = game.GetOutOfPlaceCount(secretCode);
            Assert.AreEqual(count, 0);
            count = game.GetOutOfPlaceCount(secretCode.Substring(0, 3));
            Assert.AreEqual(count, 0);
            count = game.GetOutOfPlaceCount(string.Empty);
            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        public void TestUserHasWon()
        {
            var maximumTries = 4;
            var elementsToGuess = 6;
            var game = new Game(maximumTries, elementsToGuess);
            var hasWon = game.UserHasWon();
            Assert.AreEqual(hasWon, false);

            game.GenerateSecretCode();
            var code = game.GetSecretCode();
            game.AddNewMove(code);
            hasWon = game.UserHasWon();
            Assert.AreEqual(hasWon, true);
        }

        [TestMethod]
        public void TestAddNewMove()
        {
            var maximumTries = 4;
            var elementsToGuess = 6;
            var userActionValue = "TEST";
            var game = new Game(maximumTries, elementsToGuess);
            game.AddNewMove(userActionValue);
            var userAction = game.GetLastUserAction();
            Assert.IsNotNull(userAction);
            Assert.AreEqual(userAction.UserTry, userActionValue);
            Assert.AreEqual(userAction.InPlaceCount, 0);
            Assert.AreEqual(userAction.TryNumber, 1);
        }

        [TestMethod]
        public void TestIsGuessCorrectFormat()
        {
            var maximumTries = 4;
            var elementsToGuess = 5;
            var game = new Game(maximumTries, elementsToGuess);
            var leftCount = game.GetLeftRoundsCount();
            Assert.AreEqual(leftCount, maximumTries);

            game.AddNewMove("ACTION1");
            leftCount = game.GetLeftRoundsCount();
            Assert.AreEqual(leftCount, maximumTries - 1);
        }

        [TestMethod]
        public void TestGetLeftRoundsCount()
        {
            var maximumTries = 4;
            var elementsToGuess = 5;
            var game = new Game(maximumTries, elementsToGuess);
            var result1 = game.IsGuessCorrectFormat("TEST");
            var result2 = game.IsGuessCorrectFormat("TESTTEST");
            var result3 = game.IsGuessCorrectFormat("RJBOV");
            var result4 = game.IsGuessCorrectFormat("RJVOP");
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            Assert.IsTrue(result3);
            Assert.IsFalse(result4);
        }
    }
}
