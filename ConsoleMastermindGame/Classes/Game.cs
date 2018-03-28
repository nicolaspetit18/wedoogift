using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMastermindGame.Classes
{
    public class Game
    {
        private List<String> _colorList = new List<String>() { "R", "J", "B", "O", "V", "N" };

        private int _elementsToGuess = 4;

        private string _resultString = String.Empty;

        private UserActionHistory _userActionHistory;

        public Game(int maximumUserTries, int elementsToGuess)
        {
            if(elementsToGuess < 0)
            {
                elementsToGuess = 1;
            }
            _elementsToGuess = elementsToGuess;
            _userActionHistory = new UserActionHistory(maximumUserTries);
        }

        public void GenerateSecretCode()
        {
            _resultString = string.Empty;
            for(int i = 0; i < _elementsToGuess; ++i)
            {
                _resultString += _colorList.OrderBy(s => Guid.NewGuid()).First();
            }
        }

        public int GetInPlaceCount(string userInput)
        {
            var inPlaceGuess = 0;
            for (int i = 0; i < _resultString.Length; i++)
            {
                if (userInput.Count() > i)
                {
                    if (userInput[i] == _resultString[i])
                    {
                        inPlaceGuess++;
                    }
                }
            }
            return inPlaceGuess;
        }

        public int GetOutOfPlaceCount(string userInput)
        {
            var outPlaceGuess = 0;
            for (int i = 0; i < _resultString.Length; i++)
            {
                if (userInput.Count() > i)
                {
                    if (userInput[i] != _resultString[i])
                    {
                        if (_resultString.Contains(userInput[i]))
                        {
                            outPlaceGuess++;
                        }
                    }
                }
            }
            return outPlaceGuess;
        }

        public bool UserHasWon()
        {
            var lastUserAction = _userActionHistory.GetLastAction();
            if (lastUserAction != null && lastUserAction.InPlaceCount == _elementsToGuess)
            {
                return true;
            }
            return false;
        }

        public void AddNewMove(string userInput)
        {
            var wellPlaced = GetInPlaceCount(userInput);
            var badPlaced = GetOutOfPlaceCount(userInput);
            _userActionHistory.AddUserEntry(userInput, wellPlaced, badPlaced);
        }

        public bool IsGuessCorrectFormat(string userInput)
        {
            if(userInput.Length < 1 || userInput.Length > _elementsToGuess)
            {
                return false;
            }

            bool isCorrect = true;
            for(int i = 0; i < userInput.Length; ++i)
            {
                if(!_colorList.Contains(userInput[i].ToString()))
                {
                    return false;
                }
            }
            return isCorrect;
        }
        
        public int GetLeftRoundsCount()
        {
            return _userActionHistory.GetMaximumTries() - _userActionHistory.GetPlayedRounds();
        }

        public int GetMaximumTries()
        {
            return _userActionHistory.GetMaximumTries();
        }

        public int GetElementsToGuess()
        {
            return _elementsToGuess;
        }

        public string GetSecretCode()
        {
            return _resultString;
        }

        public UserAction GetLastUserAction()
        {
            return _userActionHistory.GetLastAction();
        }

        public string ToDisplay()
        {
            return _userActionHistory.ToDisplay();
        }
    }
}
