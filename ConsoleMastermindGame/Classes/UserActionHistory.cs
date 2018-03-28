using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMastermindGame.Classes
{
    public class UserActionHistory
    {
        private List<UserAction> _userActions;

        private int _maximumUserTries;

        public UserActionHistory(int maximumUserTries)
        {
            if(maximumUserTries <= 0)
            {
                maximumUserTries = 1;
            }
            _maximumUserTries = maximumUserTries;
            _userActions = new List<UserAction>();
        }

        public void AddUserEntry(string userInput, int wellPlacedEntries, int badPlacedEntries)
        {
            var userAction = new UserAction { UserTry = userInput, InPlaceCount = wellPlacedEntries, OutPlaceCount = badPlacedEntries, TryNumber = _userActions.Count() + 1 };
            _userActions.Add(userAction);
        }

        public UserAction GetLastAction()
        {
            if(!_userActions.Any())
            {
                return null;
            }
            return _userActions[_userActions.Count - 1];
        }

        public UserAction GetUserAction(int index)
        {
            if(index > _userActions.Count - 1 || index < 0)
            {
                return null;
            }
            return _userActions[index];
        }

        public int GetMaximumTries()
        {
            return _maximumUserTries;
        }

        public string ToDisplay()
        {
            var displayString = "-----------------------\n";
            foreach(var userAction in _userActions)
            {
                displayString += "| " + userAction.UserTry + " | " + userAction.InPlaceCount + " | " + userAction.OutPlaceCount + " | " + userAction.TryNumber + "/" + _maximumUserTries + " |\n";
            }
            displayString += "| .... | . | . | " + (_userActions.Count + 1) + "/" + _maximumUserTries + " |\n";
            displayString += "-----------------------\n";
            return displayString;
        }

        public int GetPlayedRounds()
        {
            return _userActions.Count();
        }
    }
}
