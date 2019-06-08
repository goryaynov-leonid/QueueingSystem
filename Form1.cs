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
        Double ArrivalLambda, ServiceLambda;
        int PersonInQueue;
        DateTime WhenArriveNext;
        List<Operator> Operators = new List<Operator>();
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        public Form1()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            NumberOfOperators = Convert.ToInt32(operatorsTextBox.Text);
            ArrivalLambda = Convert.ToDouble(arrivalTextBox.Text);
            ServiceLambda = Convert.ToDouble(serviceTextBox.Text);

            Operators.Clear();
            for (int i = 0; i < NumberOfOperators; i++)
            {
                Operators.Add(new Operator { State = State.Free, WorkCoefficient = ServiceLambda });
            }

            PersonInQueue = 0;
            WhenArriveNext = DateTime.Now + new TimeSpan(0, 0, (int)Math.Ceiling(-Math.Log(rnd.NextDouble()) / ArrivalLambda));
            if (!timer1.Enabled)
                timer1.Start();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= WhenArriveNext)
            {
                PersonInQueue++;
                WhenArriveNext += new TimeSpan(0, 0, (int)Math.Ceiling(-Math.Log(rnd.NextDouble()) / ArrivalLambda));
            }

            Operators.ForEach(x => x.CheckIfIsFree());

            if (PersonInQueue > 0 && Operators.Any(x => x.State == State.Free))
            {
                Operators.First(x => x.State == State.Free).AddClient();
                PersonInQueue--;
            }

            label4.Text = $"Person in queue {PersonInQueue}";
            label5.Text = $"Free operators {Operators.Sum(x => (x.State == State.Free ? 1 : 0))}";
        }
    }
}
