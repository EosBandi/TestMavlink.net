using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using MavLinkNet;

namespace MavlinkMonitorTest
{
    public partial class Form1 : Form
    {
        public static MavLinkSerialPortTransport mluc = new MavLinkSerialPortTransport();


        private MavLinkSerialPortTransport mMavLink;
        private UasAttitude mAttitudeState;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            mluc.SerialPortName = "COM22";
            mluc.BaudRate = 57600;
            mluc.HeartBeatUpdateRateMs = 1000;

            mluc.OnPacketReceived += OnMavLinkPacketReceived;


            mluc.Initialize();
            mluc.BeginHeartBeatLoop();

            pnlConnect.BackColor = Color.ForestGreen;
            lblConnect.Text = "Connected";
            btnConnect.Enabled = false;
        }

        private void OnMavLinkPacketReceived(object sender, MavLinkPacket e)
        {
            PrintPacket(e);
        }

        private void PrintPacket(MavLinkPacket p)
        {
            PrintMessage(p.Message);
        }

        private void PrintMessage(UasMessage m)
        {
            
            UasMessageMetadata md = m.GetMetadata();
            foreach (UasFieldMetadata f in md.Fields)//==================================================================
            {
                GetFieldValue(f.Name, m);//============================================================
            //    //f.Name, GetFieldValue(f.Name, m), f.Description
            }
        }

        private void GetFieldValue(string fieldName, UasMessage m)
        {
            if (fieldName == "Roll")
            {
                PropertyInfo p = m.GetType().GetProperty(fieldName);
                Double degree = Convert.ToDouble(p.GetValue(m, null).ToString()) * (180 / Math.PI);
                lblRoll.Invoke((MethodInvoker)(() => lblRoll.Text = degree.ToString()));
                //lblRoll.Text= p.GetValue(m, null).ToString();
            }
            else if (fieldName == "Pitch")
            {
                PropertyInfo p = m.GetType().GetProperty(fieldName);
                Double degree = Convert.ToDouble(p.GetValue(m, null).ToString()) * (180 / Math.PI);
                lblPitch.Invoke((MethodInvoker)(() => lblPitch.Text = degree.ToString()));
                //lblPitch.Text = p.GetValue(m, null).ToString();
            }
            else if (fieldName == "Yaw")
            {
                PropertyInfo p = m.GetType().GetProperty(fieldName);
                Double degree = Convert.ToDouble(p.GetValue(m, null).ToString())*(180/Math.PI);
                lblYaw.Invoke((MethodInvoker)(() => lblYaw.Text = degree.ToString()));
                //lblYaw.Text = p.GetValue(m, null).ToString();
            }
            //if (fieldName != "Rxerrors" && fieldName != "TimeBootMs" && fieldName != "TimeUsec"&& fieldName != "Chan1Raw" && fieldName != "Fixed" && fieldName != "Chan2Raw" && fieldName!= "VtolState" && fieldName!= "Chan3Raw" && fieldName!= "Rssi" && fieldName!="Q" && fieldName!= "Chan4Raw")
            //{
            //    PropertyInfo p = m.GetType().GetProperty(fieldName);
            //    var resulti = p.GetValue(m,null);
            //    object result = p.GetValue(m, null);
            //}


            //if (p == null)
            //{
            //    WL("MISSING FIELD: {0} on {1}", fieldName, m.GetType());
            //    return "";
            //}



            //if (result is char[]) return new String((char[])result);

            //return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
