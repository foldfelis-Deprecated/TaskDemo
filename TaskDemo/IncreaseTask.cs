using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    internal class IncreaseTask
    {
        /*
         * Vars
         * ========== ========== ========== ========== ========== ========== ========== ========== */
        private Task _taskIncrease;
        private bool _flagOp;

        public event Action<double> UpdateLabelVal;
        public event Action<string> UpdateLabelState;
        public event Action<bool> UpdateUi;

        /*
         * Task Functions
         * ========== ========== ========== ========== ========== ========== ========== ========== */
        public void StartIncrease()
        {
            // Set flag
            _flagOp = true;

            // Disable UI
            DisableUi();

            // Enable main task
            _taskIncrease = new Task(Increase);
            _taskIncrease.Start();

            // Enable task checker
            var taskCheckIncreaseStatus = new Task(CheckIncreaseStatus);
            taskCheckIncreaseStatus.Start();
        }

        public void StopIncrease()
        {
            // Set flag
            _flagOp = false;
        }

        /*
         * Main Functions
         * ========== ========== ========== ========== ========== ========== ========== ========== */
        private void Increase()
        {
            var num = 0.0;

            while (num < 10)
            {
                // Check flag
                if (!_flagOp) return;

                // Update UI
                UpdateLabelVal?.Invoke(num);

                // Time-consuming stuff
                Thread.Sleep(2000);
                num += 2.0;
            }
        }

        private void CheckIncreaseStatus()
        {
            while (_taskIncrease.Status == TaskStatus.Running)
            {
                UpdateLabelState?.Invoke(@"Status: Running");
                Thread.Sleep(1);
            }

            UpdateLabelState?.Invoke(@"Status: Stopped");

            EnableUi();
        }

        private void DisableUi()
        {
            UpdateUi?.Invoke(false);
        }

        private void EnableUi()
        {
            UpdateUi?.Invoke(true);
        }
    }
}
