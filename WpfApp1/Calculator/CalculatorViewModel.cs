using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class CalculatorViewModel : NotifiableObjectBase
    {
        private double currentNum;
        private double previousNum;
        private string previousOperator;

        public CalculatorViewModel()
        {
            NumComand = new RelayCommand(NumberButton);
            OperatorComand = new RelayCommand(OperatorButton);
            EqualsComand = new RelayCommand((obj) => !string.IsNullOrEmpty(previousOperator), EqualsButton);
            ClearComand = new RelayCommand(ClearButton);
        }

        public double CurrentNum 
        { 
            get 
            {
                return currentNum; 
            }
            set
            { 
                if(currentNum != value)
                {
                    currentNum = value;
                    RaisePropertyChanged();
                }
            } 
        }

        private string PreviousOperator 
        {
            get 
            { 
                return previousOperator;
            }
            set
            {
                if(value != previousOperator)
                {
                    previousOperator = value;
                    EqualsComand.RaiseCanExecuteChange();
                }
            }
        }


        public RelayCommand NumComand { get; set; }
        public RelayCommand OperatorComand { get; set; }
        public RelayCommand EqualsComand { get; set; }
        public RelayCommand ClearComand { get; set; }

        private void NumberButton(object _obj)
        {
            int buttonNumber = int.Parse((string)_obj);
            CurrentNum = currentNum * 10 + buttonNumber;
        }

        private void OperatorButton(object _obj)
        {
            if (!string.IsNullOrEmpty(previousOperator))
            {
                EqualsButton(_obj);
            }
            PreviousOperator = (string)_obj;
            previousNum = currentNum;
            CurrentNum = 0;
        }

        private void EqualsButton(object _obj)
        {
            switch (previousOperator)
            {
                case "+":
                    CurrentNum += previousNum;
                    break;
                case "-":
                    CurrentNum = previousNum - currentNum;
                    break;
                case "*":
                    CurrentNum *= previousNum;
                    break;
                case "/":
                    CurrentNum = previousNum / currentNum;
                    break;
                default:
                    return;
            }
            previousNum = 0;
            PreviousOperator = "";
        }

        private void ClearButton(object _obj)
        {
            previousNum = 0;
            CurrentNum = 0;
            PreviousOperator = "";
        }
    }
}
