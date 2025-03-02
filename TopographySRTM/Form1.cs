using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSGeo.GDAL;

namespace TopographySRTM
{
    public partial class Form1 : Form
    {

        //string srcFile = "C:\\Users\\Ghazanfar\\Downloads\\rasters_SRTMGL1Ellip\\output_SRTMGL1Ellip.tif";
        //string tmpFile = "C:\\Users\\Ghazanfar\\Downloads\\rasters_SRTMGL1Ellip\\resampledtif.tif";
        //string destFile = "C:\\Users\\Ghazanfar\\Downloads\\rasters_SRTMGL1Ellip\\tiftosrtm.hgt";


        string srcFile = "C:\\Users\\Ghazanfar\\Downloads\\viz\\output_SRTMGL1Ellip.tif";
        string tmpFile = "C:\\Users\\Ghazanfar\\Downloads\\viz\\resampledEllip_hillshade.tif";
        string destFile = "C:\\Users\\Ghazanfar\\Downloads\\viz\\tiftosrtm.hgt";
        public Form1()
        {
            InitializeComponent();

            //Gdal.AllRegister();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                GdalConfiguration.ConfigureGdal();


                //Resample the image to the desired dimensions (e.g 1201x1201)
                string[] resampleOptions = new string[] { "-ts", "1201", "1201", "-r","bilinear","-verbose" };
                GdalTranslate(tmpFile, srcFile, resampleOptions);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

            Dataset srcDataset = Gdal.Open(srcFile, Access.GA_ReadOnly);
            Driver driver = Gdal.GetDriverByName("SRTMHGT");
            driver.CreateCopy(destFile, srcDataset, 0, null, null, null);
            srcDataset.Dispose();


        }
        void GdalTranslate(string dstFile, string srcFile, string[] options) {

            Dataset srcDataset = Gdal.Open(srcFile, Access.GA_ReadOnly);
            Driver  driver = Gdal.GetDriverByName("GTiff");
            Dataset dstDataset = driver.CreateCopy(dstFile, srcDataset, 0, options, null
                , null);
            dstDataset.Dispose();
            srcDataset.Dispose();
        }
    }
}
