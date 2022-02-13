using System.Globalization;

namespace Calculator
{
    public partial class FormCalculator : Form
    {
        public Operation Mode = Operation.None;
        public string firstNumber = "0", secondNumber = string.Empty;
        bool changed = false;
        History history;
        Calculation calculation;
        Handlers handlers;

        public FormCalculator()
        {
            InitializeComponent();
            valueLb.Parent = pictureBox1;
            historyLb.Parent = pictureBox1;

            handlers = new Handlers(this);
            calculation = new Calculation(this);
            history = new History(this);
        }

        #region Arithmetic operations
        private void plusBt_Click(object sender, EventArgs e) => ModeChanged(Operation.Plus);
        private void minusBt_Click(object sender, EventArgs e)  => ModeChanged(Operation.Minus);
        private void multiplyBt_Click(object sender, EventArgs e) => ModeChanged(Operation.Multiply);
        private void divideBt_Click(object sender, EventArgs e) => ModeChanged(Operation.Divide);
        private void powBt_Click(object sender, EventArgs e)   => ModeChanged(Operation.Power);
        private void rootBt_Click(object sender, EventArgs e) => Insert("sqrt");
        private void cosBt_Click(object sender, EventArgs e) => Insert("cos");
        private void sinBt_Click(object sender, EventArgs e) => Insert("sin");
        private void turnBt_Click(object sender, EventArgs e) => Insert("turn");
        private void factorialBt_Click(object sender, EventArgs e) => Insert("back");
        private void deleteBt_Click(object sender, EventArgs e)
        {
            firstNumber = "0";

            history.Clear();
            valueLb.Text = firstNumber;
            Mode = Operation.None;
        }
        #endregion

        #region Digits
        private void zeroBt_Click(object sender, EventArgs e) => Insert("0");
        private void oneBt_Click(object sender, EventArgs e) => Insert("1");
        private void twoBt_Click(object sender, EventArgs e) => Insert("2");
        private void threeBt_Click(object sender, EventArgs e) => Insert("3");
        private void fourBt_Click(object sender, EventArgs e) => Insert("4");
        private void fiveBt_Click(object sender, EventArgs e) => Insert("5");
        private void sixBt_Click(object sender, EventArgs e) => Insert("6");
        private void sevenBt_Click(object sender, EventArgs e) => Insert("7");
        private void eightBt_Click(object sender, EventArgs e) => Insert("8");
        private void nineBt_Click(object sender, EventArgs e) => Insert("9");
        private void dotBt_Click(object sender, EventArgs e) => Insert(",");
        private void invertBt_Click(object sender, EventArgs e) => Insert("-");
        #endregion

        #region MainLogic
        private void equalBt_Click(object sender, EventArgs e)
        {
            history.BeforeEqual();
            calculation.Equal();   
            calculation.ChangeDisplay(firstNumber);
        }

        private void ModeChanged(Operation operation)
        {

            if (Mode != Operation.Error)
            {
                Mode = operation;
                changed = true;
                valueLb.Text = secondNumber = "0";
                history.AfterOperation();
            }
        }

        private void Insert(string number)
        {
            if (Mode != Operation.Error)
            {
                if (Mode == Operation.None)
                {
                    handlers.HandleInput(number, ref firstNumber,
                        ref changed, ref secondNumber);
                    history.Clear();
                }
                else
                {
                    handlers.HandleInput(number, ref secondNumber,
                        ref changed, ref secondNumber);
                }
            }
        }

        private void FormCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validCharacters = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', (char)Keys.Back };
            var operations = new Dictionary<char, Operation>()
            {
                { '+', Operation.Plus },
                { '-', Operation.Minus },
                { '*', Operation.Multiply },
                { '/', Operation.Divide },
                { '^', Operation.Power }
            };

            var sym = e.KeyChar == '.' ? ',' : e.KeyChar;
            if (validCharacters.Contains(sym))
            {
                var input = sym == (char)Keys.Back ? "back" : sym.ToString();
                Insert(input);
            }
            else if (operations.ContainsKey(sym))
            {
                ModeChanged(operations[sym]);
            }
        }

        #endregion
    }
}