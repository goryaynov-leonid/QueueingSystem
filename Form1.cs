using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueueingSystem
{
    public partial class Form1 : Form
    {
        int NumberOfOperators;
        Logger Logger;
        Double ArrivalLambda, ServiceLambda;
        Queue Queue;
        List<IAgent> Agents= new List<IAgent>();
        public Form1()
        {
            InitializeComponent();
            Logger = new Logger { log = listBox1 };
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            NumberOfOperators = Convert.ToInt32(operatorsTextBox.Text);
            ArrivalLambda = Convert.ToDouble(arrivalTextBox.Text);
            ServiceLambda = Convert.ToDouble(serviceTextBox.Text);

            Agents.Clear();
            Queue = new Queue(ArrivalLambda, Logger);
            Agents.Add(Queue);
            for (int i = 0; i < NumberOfOperators; i++)
            {
               Agents.Add(new Operator { State = State.Free, WorkCoefficient = ServiceLambda, Logger = Logger, Queue = Queue });
            }


            if (!timer1.Enabled)
                timer1.Start();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Agents.ForEach(x => x.Tick());

            label4.Text = $"Person in queue {Queue.size}";
            label5.Text = $"Free operators {Agents.Sum(x => (x is Operator &&  (x as Operator).State == State.Free ? 1 : 0))}";
        }
    }
}
