using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Handlers
    {
        Calculation calculation;
        FormCalculator form;

        public Handlers(FormCalculator form)
        {
            this.form = form;
            calculation = new Calculation(form);
        }

        public void HandleInput(string number, ref string value, ref bool changed, ref string secondNumber)
        {
            if (changed && number != "-")
            {
                secondNumber = "0";
                changed = !changed;
            }

            if (value == "0")
            {
                HandlerInitialInput(number, ref value);
                return;
            }

            try
            {
                switch (number)
                {
                    case "-":
                        HandlerMinus(ref value);
                        return;

                    case "sqrt":
                        HandlerSqrt(ref value);
                        break;
                    case "turn":
                        HandlerTurn(ref value);
                        break;
                    case "sin":
                        HandlerSin(ref value);
                        break;
                    case "cos":
                        HandlerCos(ref value);
                        break;
                    case "back":
                        HandlerBack(ref value);
                        break;

                    default:
                        if (number == "," && value.Contains(","))
                        {
                            break;
                        }
                        else
                        {
                            value += number;
                        }
                        break;
                }
            }
            catch
            {
                form.Mode = Operation.Error;
            }

            calculation.ChangeDisplay(value);
        }

        private void HandlerMinus(ref string value)
        {
            if (value == "0")
                return;

            value = value[0] == '-'? value.Trim('-'): value.Insert(0, "-");
            calculation.ChangeDisplay(value);
        }

        private void HandlerSqrt(ref string value)
        {
            double number = double.Parse(value);
            if (number < 0)
                form.Mode = Operation.Error;
            else
                value = Math.Sqrt(number).ToString();
        }
        private void HandlerTurn(ref string value)
        {
            if (value != "0")
                value = (1.0 / double.Parse(value)).ToString();
            else
                form.Mode = Operation.Error;
        }

        private void HandlerSin(ref string value) => value = Math.Sin(double.Parse(value)).ToString();
        private void HandlerCos(ref string value) => value = Math.Cos(double.Parse(value)).ToString();

        private void HandlerBack(ref string value) => 
            value = (value.Length == 1 || value.Length == 2 && value[0] == '-') ?
                "0" : value.Substring(0, value.Length - 1);


        private void HandlerInitialInput(string number, ref string value)
        {
            switch (number)
            {
                case "-":
                    return;
                case "0":
                    return;
                case "back":
                    return;

                case ",":
                    value = value + ",";
                    break; 
                case "sqrt":
                    HandlerSqrt(ref value);
                    break;
                case "turn":
                    HandlerTurn(ref value);
                    break;
                case "sin":
                    HandlerSin(ref value);
                    break;
                case "cos":
                    HandlerCos(ref value);
                    break;

                default:
                    value = number;
                    break;
            }

            calculation.ChangeDisplay(value);
        }
    }
}
