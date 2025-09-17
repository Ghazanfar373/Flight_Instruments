using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HUD_Claude
{
    [ToolboxItem(true)]
    public partial class SwarmHud : UserControl
    {
        public SwarmHud()
        {
            InitializeComponent();
        }

        #region Flight Instrument Properties

        /// <summary>
        /// Gets or sets the pitch angle for the horizon indicator
        /// </summary>
        [Browsable(true)]
        [Category("Flight Data")]
        [Description("Pitch angle in degrees (-90 to +90)")]
        public float Pitch
        {
            get { return horizonIndicator1.Pitch; }
            set { horizonIndicator1.Pitch = value; }
        }

        /// <summary>
        /// Gets or sets the roll angle for the horizon indicator
        /// </summary>
        [Browsable(true)]
        [Category("Flight Data")]
        [Description("Roll angle in degrees (-180 to +180)")]
        public float Roll
        {
            get { return horizonIndicator1.Roll; }
            set { horizonIndicator1.Roll = value; }
        }

        /// <summary>
        /// Gets or sets the heading for the compass control
        /// </summary>
        [Browsable(true)]
        [Category("Flight Data")]
        [Description("Heading in degrees (0 to 360)")]
        public float Heading
        {
            get { return compassControl1.Heading; }
            set { compassControl1.Heading = value; }
        }

        #endregion

        #region Vehicle Information Properties

        /// <summary>
        /// Gets or sets the vehicle name/identifier
        /// </summary>
        //[Browsable(true)]
        //[Category("Vehicle Info")]
        //[Description("Vehicle name or identifier")]
        //public string VehicleName
        //{
        //    get { return labelVehicle.Text; }
        //    set { labelVehicle.Text = value; }
        //}

        /// <summary>
        /// Gets or sets the text for the Arm button
        /// </summary>
        [Browsable(true)]
        [Category("Vehicle Info")]
        [Description("Text displayed on the Arm button")]
        public string ArmButtonText
        {
            get { return buttonArm.Text; }
            set { buttonArm.Text = value; }
        }

        [Browsable(true)]
        [Category("Modes")]
        [Description("Autopilot Mode")]
        public string ApMode
        {
            get { return apStatusControl1.AutopilotMode; }
            set { apStatusControl1.AutopilotMode = value; }
        }
        [Browsable(true)]
        [Category("Arm")]
        [Description("Arm Status")]
        public bool isArm
        {
            get { return apStatusControl1.IsArmed; }
            set { apStatusControl1.IsArmed = value; }
        }
        [Browsable(true)]
        [Category("GPS")]
        [Description("GPS Status")]
        public bool GPS
        {
            get { return horizonIndicator1.GpsStatus; }
            set { horizonIndicator1.GpsStatus = value; }
        }

        [Browsable(true)]
        [Category("Vehicle")]
        [Description("Vehicle Info")]
        public string VehicleInfo
        {
            get { return horizonIndicator1.VehicleNumber; }
            set { horizonIndicator1.VehicleNumber = value; }
        }

        /// <summary>
        /// Gets or sets the text for the Loiter button
        /// </summary>
        [Browsable(true)]
        [Category("Vehicle Info")]
        [Description("Text displayed on the Loiter button")]
        public string LoiterButtonText
        {
            get { return buttonLoiter.Text; }
            set { buttonLoiter.Text = value; }
        }

        /// <summary>
        /// Gets or sets whether the Arm button is enabled
        /// </summary>
        [Browsable(true)]
        [Category("Vehicle Info")]
        [Description("Whether the Arm button is enabled")]
        public bool ArmButtonEnabled
        {
            get { return buttonArm.Enabled; }
            set { buttonArm.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the Loiter button is enabled
        /// </summary>
        [Browsable(true)]
        [Category("Vehicle Info")]
        [Description("Whether the Loiter button is enabled")]
        public bool LoiterButtonEnabled
        {
            get { return buttonLoiter.Enabled; }
            set { buttonLoiter.Enabled = value; }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Event fired when the Arm button is clicked
        /// </summary>
        public event EventHandler ArmButtonClick
        {
            add { buttonArm.Click += value; }
            remove { buttonArm.Click -= value; }
        }

        /// <summary>
        /// Event fired when the Loiter button is clicked
        /// </summary>
        public event EventHandler LoiterButtonClick
        {
            add { buttonLoiter.Click += value; }
            remove { buttonLoiter.Click -= value; }
        }

        #endregion

        #region Direct Access Methods (Alternative approach)

        /// <summary>
        /// Gets direct access to the HorizonIndicator control
        /// </summary>
        /// <returns>The HorizonIndicator control</returns>
        public HorizonIndicator GetHorizonIndicator()
        {
            return horizonIndicator1;
        }

        /// <summary>
        /// Gets direct access to the CompassControl
        /// </summary>
        /// <returns>The CompassControl</returns>
        public CompassControl GetCompassControl()
        {
            return compassControl1;
        }

        /// <summary>
        /// Gets direct access to the Vehicle label
        /// </summary>
        /// <returns>The Vehicle label control</returns>
        //public Label GetVehicleLabel()
        //{
        //    return labelVehicle;
        //}

        /// <summary>
        /// Gets direct access to the Arm button
        /// </summary>
        /// <returns>The Arm button control</returns>
        public Button GetArmButton()
        {
            return buttonArm;
        }

        /// <summary>
        /// Gets direct access to the Loiter button
        /// </summary>
        /// <returns>The Loiter button control</returns>
        public Button GetLoiterButton()
        {
            return buttonLoiter;
        }

        #endregion

        #region Bulk Update Methods

        /// <summary>
        /// Updates all flight data at once
        /// </summary>
        /// <param name="pitch">Pitch angle</param>
        /// <param name="roll">Roll angle</param>
        /// <param name="heading">Heading</param>
        public void UpdateFlightData(float pitch, float roll, float heading)
        {
            horizonIndicator1.Pitch = pitch;
            horizonIndicator1.Roll = roll;
            compassControl1.Heading = heading;
        }

        /// <summary>
        /// Updates vehicle information
        /// </summary>
        /// <param name="vehicleName">Vehicle name</param>
        /// <param name="armEnabled">Whether arm button should be enabled</param>
        /// <param name="loiterEnabled">Whether loiter button should be enabled</param>
        public void UpdateVehicleInfo(string vehicleName, bool armEnabled = true, bool loiterEnabled = true)
        {
            //labelVehicle.Text = vehicleName;
            buttonArm.Enabled = armEnabled;
            buttonLoiter.Enabled = loiterEnabled;
        }

        #endregion
    }
}