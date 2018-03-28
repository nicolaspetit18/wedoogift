using ConsoleMastermindGame.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleMastermindGameUnitTests
{
    [TestClass]
    public class UserActionHistoryTest
    {
        [TestMethod]
        public void TestInitialise()
        {
            var maximumTries = 4;
            var userActionHistory = new UserActionHistory(maximumTries);
            Assert.AreEqual(userActionHistory.GetMaximumTries(), maximumTries);
            Assert.AreEqual(userActionHistory.GetPlayedRounds(), 0);
            Assert.IsNull(userActionHistory.GetLastAction());

            maximumTries = -4;
            userActionHistory = new UserActionHistory(maximumTries);
            Assert.AreEqual(userActionHistory.GetMaximumTries(), 1);
            Assert.AreEqual(userActionHistory.GetPlayedRounds(), 0);
            Assert.IsNull(userActionHistory.GetLastAction());
        }

        [TestMethod]
        public void TestAddUser()
        {
            var maximumTries = 4;
            var userEntry1 = "AAAA";
            var wellPlaced1 = 0;
            var badPlaced1 = 0;
            var userActionHistory = new UserActionHistory(maximumTries);
            userActionHistory.AddUserEntry(userEntry1, wellPlaced1, badPlaced1);
            var userAction = userActionHistory.GetLastAction();
            Assert.AreEqual(userAction.UserTry, userEntry1);
            Assert.AreEqual(userAction.TryNumber, 1);
            Assert.AreEqual(userAction.InPlaceCount, wellPlaced1);
            Assert.AreEqual(userAction.OutPlaceCount, badPlaced1);

            var userEntry2 = "BBBB";
            var wellPlaced2 = 1;
            var badPlaced2 = 2;
            userActionHistory.AddUserEntry(userEntry2, wellPlaced2, badPlaced2);
            userAction = userActionHistory.GetLastAction();
            Assert.AreEqual(userAction.UserTry, userEntry2);
            Assert.AreEqual(userAction.TryNumber, 2);
            Assert.AreEqual(userAction.InPlaceCount, wellPlaced2);
            Assert.AreEqual(userAction.OutPlaceCount, badPlaced2);
        }

        [TestMethod]
        public void TestGetLastAction()
        {
            var maximumTries = 4;
            var userEntry1 = "AAAA";
            var wellPlaced1 = 0;
            var badPlaced1 = 0;
            var userEntry2 = "BBBB";
            var wellPlaced2 = 1;
            var badPlaced2 = 2;
            var userActionHistory = new UserActionHistory(maximumTries);
            userActionHistory.AddUserEntry(userEntry1, wellPlaced1, badPlaced1);
            var userAction1 = userActionHistory.GetLastAction();
            userActionHistory.AddUserEntry(userEntry2, wellPlaced2, badPlaced2);
            var userAction2 = userActionHistory.GetLastAction();

            Assert.AreEqual(userAction1.UserTry, userEntry1);
            Assert.AreEqual(userAction1.TryNumber, 1);
            Assert.AreEqual(userAction1.InPlaceCount, wellPlaced1);
            Assert.AreEqual(userAction1.OutPlaceCount, badPlaced1);
            Assert.AreEqual(userAction2.UserTry, userEntry2);
            Assert.AreEqual(userAction2.TryNumber, 2);
            Assert.AreEqual(userAction2.InPlaceCount, wellPlaced2);
            Assert.AreEqual(userAction2.OutPlaceCount, badPlaced2);
        }

        [TestMethod]
        public void TestGetAction()
        {
            var maximumTries = 4;
            var userEntry = "AAAA";
            var wellPlaced = 0;
            var badPlaced = 0;
            var userActionHistory = new UserActionHistory(maximumTries);
            userActionHistory.AddUserEntry(userEntry, wellPlaced, badPlaced);
            var userAction1 = userActionHistory.GetUserAction(0);
            var userAction2 = userActionHistory.GetUserAction(-1);
            var userAction3 = userActionHistory.GetUserAction(5);

            Assert.IsNotNull(userAction1);
            Assert.AreEqual(userAction1.UserTry, userEntry);
            Assert.AreEqual(userAction1.TryNumber, 1);
            Assert.AreEqual(userAction1.InPlaceCount, wellPlaced);
            Assert.AreEqual(userAction1.OutPlaceCount, badPlaced);

            Assert.IsNull(userAction2);

            Assert.IsNull(userAction3);
        }
    }
}
