using System;
using System.Windows.Forms;

namespace TaskDemo
{
    public partial class TaskDemoForm : Form
    {
        private readonly IncreaseTask _increaseTask;
        public TaskDemoForm()
        {
            InitializeComponent();

            // delegate
            _increaseTask = new IncreaseTask();
            _increaseTask.UpdateLabelVal += SetValLabel;
            _increaseTask.UpdateLabelState += SetStatusLabel;
            _increaseTask.UpdateUi += EnableUi;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _increaseTask.StartIncrease();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _increaseTask.StopIncrease();
        }

        private void SetValLabel(double num)
        {
            Invoke(new Action(() =>
            {
                ValLabel.Text = $@"{num:00.00}";
            }));
        }

        private void SetStatusLabel(string status)
        {
            Invoke(new Action(() =>
            {
                StatusLabel.Text = status;
            }));
        }

        private void EnableUi(bool isEnable)
        {
            Invoke(new Action(() =>
            {
                StopButton.Enabled = !isEnable;
                StopButton.Enabled = isEnable;
            }));
        }
    }
}
