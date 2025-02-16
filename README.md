# Flight Control Instruments

A C# library (Flight_Instruments) available as a nuget package providing realistic flight instrument controls for aviation applications using System.Drawing. This project includes two primary flight instruments:

1. Artificial Horizon (Attitude Indicator)
2. Heading Indicator (Directional Gyro)

## Overview

This library offers highly customizable flight instrument controls that can be integrated into Windows Forms or WPF applications. The instruments are designed to provide accurate visual representations of aircraft attitude and heading information, making them suitable for flight simulation, training applications, or any aviation-related software.

## Features

### Artificial Horizon
- Real-time display of pitch and roll attitudes
- Smooth animation transitions
- Configurable colors and markings
- Scale-adjustable pitch ladder
- Realistic aircraft reference symbol

### Heading Indicator
- 360-degree heading display
- Cardinal and ordinal direction markers
- Smooth rotation animation
- Adjustable heading bug
- High-precision rendering

## Prerequisites

- .NET Framework 4.5 or higher
- System.Drawing package
- Windows Forms or WPF development environment

## Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/flight-controls.git
```

2. Add the project to your solution or reference the compiled DLL in your project.

## Usage

### Adding Controls to Your Form

```csharp
 public partial class Form1 : Form
 {

     private float pitch = 0; // Degrees, positive is nose up
     private float roll = 0;  // Degrees, positive is right roll
     public Form1()
     {
         InitializeComponent();
     }
     protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
     {
         switch (keyData)
         {
             case Keys.Up:
                 pitch = Math.Min(pitch + 1, 90);
                 break;
             case Keys.Down:
                 pitch = Math.Max(pitch - 1, -90);
                 break;
             case Keys.Left:
                 roll = Math.Max(roll - 1, -180);
                 break;
             case Keys.Right:
                 roll = Math.Min(roll + 1, 180);
                 break;
             case Keys.Space:
                 pitch = 0;
                 roll = 0;
                 break;
         }
        
         compassControl1.Heading = roll;

         horizonIndicator1.Pitch = pitch;
         horizonIndicator1.Roll = roll;
         return base.ProcessCmdKey(ref msg, keyData);

     }
```



## Customization

Both instruments support extensive customization through their properties:



## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

Special thanks to the aviation community for providing guidance on instrument accuracy and design standards.

## Screenshots

[https://github.com/Ghazanfar373/Flight_Instruments/tree/master/images/compass.jpg][https://github.com/Ghazanfar373/Flight_Instruments/blob/master/images/Hud_Compass.jpg][https://github.com/Ghazanfar373/Flight_Instruments/blob/master/images/horizontal.jpg]

## Contact

[Muhammad Ghazanfar Ali] - [mianali8366@gmail.com]
Project Link: https://github.com/Ghazanfar373/Flight_Instruments
