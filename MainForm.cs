namespace Calculator
{
    public partial class MainForm : Form
    {
        double firstNum = 0.0;
        double secondNum = 0.0;
        bool isFirstNumValidate = false;
        delegate double Calculate(double firstNumber, double secondNumber);
        Calculate calculate;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillNumberPad();
        }

        private void NumberPanelDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var input = NumberPadDataGridView.CurrentCell.Value.ToString();
            var tag = NumberPadDataGridView.CurrentCell.Tag;
            DoTask(input, tag);
        }

        private void FillNumberPad()
        {
            var signs = new[] { '.', '=', '+', '-', '*', '÷' };
            int number = 9;
            int signCounter = 0;
            for (int i = 0; i < 4; i++)
            {
                NumberPadDataGridView.Rows.Add(new DataGridViewRow { Height = 72 });
                for (int j = 0; j < 4; j++)
                {
                    if (number >= 0 && NumberPadDataGridView.ColumnCount < 9)
                        NumberPadDataGridView.Rows[i].Cells[j].Value = number--;
                    else
                    {
                        NumberPadDataGridView.Rows[i].Cells[j].Tag = "Sign";
                        NumberPadDataGridView.Rows[i].Cells[j].Value = signs[signCounter++];
                        NumberPadDataGridView.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
                    }
                }
            }
            NumberPadDataGridView.CurrentCell = null;
        }

        private void DoTask(string? input, object tag)
        {
            if (tag == "Sign")
            {
                if (!isFirstNumValidate)
                    isFirstNumValidate = double.TryParse(ShowNumberTextBox.Text, out firstNum);

                switch (char.Parse(input))
                {
                     case '+':
                        calculate = (firstNumber, secondNumber) => firstNumber + secondNumber;
                        break;

                    case '-':
                        calculate = (firstNumber, secondNumber) => firstNumber - secondNumber;
                        break;

                    case '*':
                        calculate = (firstNumber, secondNumber) => firstNumber * secondNumber;
                        break;

                    case '÷':
                        calculate = (firstNumber, secondNumber) => firstNumber / secondNumber;
                        break;

                    case '.':
                        if (!ShowNumberTextBox.Text.Contains('.'))
                            ShowNumberTextBox.Text += input;
                        return;

                    case '=':
                        secondNum = double.Parse(ShowNumberTextBox.Text);
                        if (calculate != null)
                        {
                            double result = calculate(firstNum, secondNum);
                            ShowNumberTextBox.Text = result.ToString();
                            isFirstNumValidate = false;
                            firstNum = result;
                            secondNum = 0.0;
                        }
                        return;
                }
                ShowNumberTextBox.Text = "";
            }
            else
                ShowNumberTextBox.Text += input;
        }
    }
}
