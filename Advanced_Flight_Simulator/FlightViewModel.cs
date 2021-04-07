﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Flight_Simulator
{
    class FlightViewModel : INotifyPropertyChanged
    {
        private IFlightModel model;

        public event PropertyChangedEventHandler PropertyChanged;
        public FlightViewModel(IFlightModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
       
        }


        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        // Properties
        public int VM_FrameId
        {
            get { return model.FrameId; }
            set { model.FrameId = value; }
        }
        public double VM_Rudder
        {
            get { return model.Rudder; }
            set { model.Rudder = value; }
        }
        
        public double VM_Throttle
        {
            get { return model.Throttle; }
            set { model.Throttle = value; }
            
        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
            set { model.Aileron = value; }

        }

        public double VM_Elevator
        {
            get { return model.Elevator; }
            set { model.Elevator = value; }

        }

        public double VM_X
        {
            get { return model.X; }
            set { model.X = value; }

        }
        public double VM_Y
        {
            get { return model.Y; }
            set { model.Y = value; }

        }
        public int VM_RowCount
        {
            get { return model.RowCount;} 
        }

        public bool VM_ShouldStop
        {
            get { return model.ShouldStop; }
            set { model.ShouldStop = value; }
        }
        public void VM_Start()
        {
            model.start();
        }
        public double VM_Direction
        {
            get { return model.Direction; }

            set { model.Direction = value; }
        }
        public double VM_Yaw
        {
            get {
                double r = model.Yaw;
                return model.Yaw; }

            set { model.Yaw = value; }
        }
        public double VM_Roll
        {
            get { return model.Roll; }


        

    }
}
