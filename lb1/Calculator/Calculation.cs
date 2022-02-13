
using System.Globalization;

namespace Calculator
{
    class Calculation
    {
        public FormCalculator form;
        public Calculation(FormCalculator form) => this.form = form;

        public void Equal()
        {
            if (form.secondNumber != string.Empty)
            {
                try
                {
                    double answer = 0;
                    double first = double.Parse(form.firstNumber);
                    double second = double.Parse(form.secondNumber);

                    switch (form.Mode)
                    {
                        case Operation.Plus:
                            answer = first + second;
                            break;
                        case Operation.Minus:
                            answer = first - second;
                            break;
                        case Operation.Multiply:
                            answer = first * second;
                            break;
                        case Operation.Divide:
                            if (second != 0)
                            {
                                answer = first / second;
                            }
                            else
                            {
                                form.Mode = Operation.Error;
                                return;
                            }
                            break;

                        case Operation.Power:
                            answer = Math.Pow(first, second);
                            break;
                        case Operation.None:
                            answer = first;
                            break;
                        case Operation.Error:
                            return;
                    }
                    form.firstNumber = answer.ToString();
                    form.Mode = Operation.None;
                }
                catch
                {
                    form.Mode = Operation.Error;
                }
            }
        }

        public void ChangeDisplay(string number)
        {
            if (form.Mode == Operation.Error)
            {
                form.valueLb.Text = "Error";
                return;
            }

            var value = double.Parse(number);
            if (value > 4000000 && value < -2000000)
            {
                form.Mode = Operation.Error;
                form.valueLb.Text = "Out of bounds";
                return;
            }

            if (number.Contains(','))
            {
                var sub = number.Substring(number.IndexOf(','),
                    number.Length - number.IndexOf(','));

                if (sub[sub.Length - 1] == '0' && sub.Length <= 5 || number[number.Length - 1] == ',')
                {
                    form.valueLb.Text = number.Replace(',', '.');
                }
                else
                {
                    form.valueLb.Text = value.ToString("0.####", CultureInfo.InvariantCulture);
                }
            }
            else
            {
                form.valueLb.Text = value.ToString("0.####", CultureInfo.InvariantCulture);
            }
        }
    }
}
