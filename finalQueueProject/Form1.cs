using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Media;
using System.Drawing.Drawing2D;

namespace finalQueueProject
{
    public partial class Form1: Form
    {
        LinkedList counter1List = new LinkedList(1000);
        LinkedList counter2List = new LinkedList(2000);
        LinkedList counter3List = new LinkedList(3000);
        LinkedList counter4List = new LinkedList(4000);

        Boolean priority = false;

        public Form1()
        {
            InitializeComponent();
            hidePanelsTimer.Interval = 2000;
            hidePanelsTimer.Tick += hidePanels;
            Speak(" ");

            Panel[] counterPanels = { c1panel, c2panel, c3panel, c4panel, staffNSPanel, c2StaffPanel, c3StaffPanel, c4StaffPanel };
            foreach (Panel panel in counterPanels)
            {
                RoundPanel(panel, 20);
            }
        }


        //-------------TIMER METHODS--------------------------------------------//
        private void hidePanels(object sender, EventArgs e)
        {
            servicePanel.Visible = false;
            numPanel.Visible = false;
            hidePanelsTimer.Stop();
            hidePanelsTimer.Dispose();
            categoryPanel.Visible = true;
            
        }
       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void regularBtn_Click(object sender, EventArgs e)
        {
            priority = false;
            categoryPanel.Visible = false;
            servicePanel.Visible = true;
        }

        private void priorityBtn_Click(object sender, EventArgs e)
        {
            priority = true;
            categoryPanel.Visible = false;
            servicePanel.Visible = true;
        }


        
        //-------------------------------SERVICE BUTTON CLICK EVENT -----------------------------//

        private void generateNumberAndUpdateQueue(DataGridView queueGrid, LinkedList counterList, String queueCol, String categCol)
        {
            SystemSounds.Beep.Play();
            if (priority)
            {
                int num = counterList.priorityEnqueue();

                if (num == -1)
                    return;

                generateNumLbl.Text = num.ToString();
                numPanel.Visible = true;
                int rowsCount = queueGrid.RowCount;

                if(rowsCount <= 0)
                {
                    queueGrid.Rows.Add(num, "Priority", queueCol, categCol);
                }

                int insertAt = 0;

                foreach (DataGridViewRow row in queueGrid.Rows)
                {
                    if(insertAt == rowsCount - 1 && row.Cells[1].Value.ToString() == "Priority") //if all nodes of the list are priority nodes
                    {
                        queueGrid.Rows.Add( num, "Priority", queueCol, categCol); //add at the end of queue
                        hidePanelsTimer.Start();
                        return;
                    }

                    if (row.Cells[1].Value.ToString() == "Regular") //if the current node is regular
                    {
                        queueGrid.Rows.Insert(insertAt, num, "Priority", queueCol, categCol); //insert the priority node before the regular node
                        hidePanelsTimer.Start();
                        return;
                    }

                    insertAt++;
                }


            }
            else
            {
                int num = counterList.enqueue();

                if (num == -1)
                    return;

                generateNumLbl.Text = num.ToString();
                numPanel.Visible = true;
                queueGrid.Rows.Add(num, "Regular", queueCol, categCol);
            }

            hidePanelsTimer.Start();
            counterList.display();
        }

        private void button13_Click(object sender, EventArgs e)
        {

            generateNumberAndUpdateQueue(c1QueueGrid, counter1List, "c1queueCol", "categCol");

        }
        private void accServicesBtn_Click(object sender, EventArgs e)
        {
            generateNumberAndUpdateQueue(c2QueueGrid, counter2List, "c2queueCol", "c2categCol");
        }

        private void LoanCreditBtn_Click(object sender, EventArgs e)
        {
            generateNumberAndUpdateQueue(c3QueueGrid, counter3List, "c3queueCol", "c3categCol");
        }

        private void customSupportBtn_Click(object sender, EventArgs e)
        {
            generateNumberAndUpdateQueue(c4QueueGrid, counter4List, "c4queueCol", "c4categCol");
        }

       

        //----------------------------NEXT BUTTON CLICK EVENT-------------------------------------//

        private void nextBtnClick(DataGridView queueGrid, Label staffNSLbl, Label counterNSLabel, LinkedList counterList, String counterNumber)
        {
            if (!counterList.isEmpty()) //queueGrid.Rows.Count != 0
            {
                String nowServing = queueGrid.Rows[0].Cells[0].Value.ToString();
                staffNSLbl.Text = nowServing;
                counterNSLabel.Text = nowServing;
                counterList.dequeue();
                queueGrid.Rows.RemoveAt(0);
                counterList.display();
                Speak("Now Serving " + nowServing[0] + " " + nowServing[1] + " " + nowServing[2] + " " + nowServing[3] + " at counter " + counterNumber);

            }
            else
            {
                counterNSLabel.Text = "";
                staffNSLbl.Text = "";
            }
        }

        private void c1NextBtn_Click(object sender, EventArgs e)
        {

            nextBtnClick(c1QueueGrid, c1StaffNSLbl, c1NSLabel, counter1List, "1");

        }
        private void c2NextBtn_Click(object sender, EventArgs e)
        {
            nextBtnClick(c2QueueGrid, c2StaffNSLbl, c2NSLabel, counter2List, "2");
        }

        private void c3NextBtn_Click(object sender, EventArgs e)
        {
            nextBtnClick(c3QueueGrid, c3StaffNSLbl, c3NSLabel, counter3List, "3");

        }

        private void c4NextBtn_Click(object sender, EventArgs e)
        {
            nextBtnClick(c4QueueGrid, c4StaffNSLbl, c4NSLabel, counter4List, "4");

        }

        public void Speak(string text)
        {
           
                var synth = new SpeechSynthesizer();
                synth.SetOutputToDefaultAudioDevice();
                synth.SelectVoice("Microsoft Zira Desktop");  
                synth.SpeakAsync(text);
        }


        // ---------------------------------RECALL CLICK EVENT ---------------------------------------------//
        private void c1RecallBtn_Click(object sender, EventArgs e)
        {
            callBtnClick(c1NSLabel, "1");

        }

        private void c2RecallBtn_Click(object sender, EventArgs e)
        {
            callBtnClick(c2NSLabel, "2");

        }

        private void c3RecallBtn_Click(object sender, EventArgs e)
        {
            callBtnClick(c3NSLabel, "3");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            callBtnClick(c4NSLabel, "4");

        }

        private void callBtnClick(Label NSLabel, String counterNumber)
        {
            if (!string.IsNullOrEmpty(NSLabel.Text))
            {
                String nowServing = NSLabel.Text.ToString();
                Speak("Now Serving " + nowServing[0] + " " + nowServing[1] + " " + nowServing[2] + " " + nowServing[3] + " at counter " + counterNumber);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void hidePanelsTimer_Tick(object sender, EventArgs e)
        {
            //if (progressBar1.Value < progressBar1.Maximum)
            //{
            //    progressBar1.Value = Math.Min(progressBar1.Value + 2, progressBar1.Maximum);
            //}
            //else if (progressBar1.Value >= progressBar1.Maximum)
            //{
            //    hidePanelsTimer.Stop();
            //    hidePanelsTimer.Dispose();
            //    servicePanel.Visible = false;
            //    numPanel.Visible = false;

            //    categoryPanel.Visible = true;
            //    progressBar1.Value = 0;// Stop when full
            //    //MessageBox.Show("Done!");
            //}
        }

        private void panel32_Paint(object sender, PaintEventArgs e)
        {

        }

        //PANEL DESIGNS

        public static void RoundPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(panel.Width - radius, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(panel.Width - radius, panel.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, panel.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);
        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void c4QueueGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
