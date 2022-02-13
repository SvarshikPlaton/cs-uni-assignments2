
namespace Calculator
{
    class History
    {
        FormCalculator form;

        public History(FormCalculator form) => this.form = form;

        private string InsertChar(Operation operation)
        {
            switch(operation)
            {
                case Operation.Plus:
                    return "+";
                case Operation.Minus:
                    return "-";
                case Operation.Multiply:
                    return "×";
                case Operation.Divide:
                    return "÷";
                case Operation.Power:
                    return "^";
            }
            return "";
        }

        public void AfterOperation() => form.historyLb.Text = 
            $"{Round(form.firstNumber)} {InsertChar(form.Mode)}";

        public void BeforeEqual()
        {
            if (form.secondNumber != string.Empty)
            {
                form.historyLb.Text =
                    $"{Round(form.firstNumber)} {InsertChar(form.Mode)}" +
                    $" {Brackets(Round(form.secondNumber))} =";
            }
        }
        public void Clear() => form.historyLb.Text = "";

        private string Brackets(string number) => number[0] == '-'? $"({number})": number;

        private string Round(string number) =>  
            Math.Round(double.Parse(number), 4).ToString();

    }
}
