using ExifLib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PictureUPLDR
{
    public class Uploader
    {
        private string _dest;
        private string[] _files;

        public Uploader(string[] files, string destinationDir)
        {
            _dest = destinationDir;
            _files = files;
        }

        public int Upload()
        {
            int br = 0;

            using (var conn = new MySqlConnection(DBConfig.DB_CONN))
            {
                conn.Open();
                conn.Close();
            }

            int filSize = _files.Length;

            var destpaths = new string[filSize];
            for (int i = 0; i < filSize; i++)
            {
                var file = _files[i];
                var fileName = file.Substring(file.LastIndexOf("\\") + 1);

                var destFile = Path.Combine(_dest, fileName);

                if (File.Exists(destFile))
                {
                    destFile = destFile.Insert(destFile.LastIndexOf("."), "-" + Guid.NewGuid());
                }

                destpaths[i] = destFile;
            }

            var s = _files.Length;
            Parallel.For(0, s, i =>
             {
                 try

                 {
                     var file = _files[i];
                     var destFile = destpaths[i];

                     File.Copy(file, destFile, true);

                     br++;
                 }
                 catch (Exception ex)
                 {
                     System.Diagnostics.Debug.WriteLine("###############################");
                     System.Diagnostics.Debug.WriteLine(ex);
                     System.Diagnostics.Debug.WriteLine("###############################");
                     destpaths[i] = null;
                 }
             });

            GetAndSavePicturesEXIFDataToDB(destpaths);

            return br;
        }

        private void GetAndSavePicturesEXIFDataToDB(string[] inFiles)
        {
            var dictForDB = new Dictionary<string, Dictionary<ExifTags, object>>();
            int filesSize = inFiles.Length;
            for (int i = 0; i < filesSize; i++)
            {
                var file = inFiles[i];
                if (string.IsNullOrEmpty(file))
                    continue;

                while (!File.Exists(file))
                {
                    Thread.Sleep(150);
                }

                var picExif = GetEXIFData(file);

                dictForDB.Add(file, picExif);
            }

            SaveAllToDB(dictForDB);
        }

        private void SaveAllToDB(Dictionary<string, Dictionary<ExifTags, object>> dictForDB)
        {
            var query = new StringBuilder();
            string insertFormat = "('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}',{16})";
            foreach (var f in dictForDB)
            {
                query.Append(" INSERT INTO PhotoData (LocationOfFile, DateTaken, Width, Height, CameraMaker, CameraModel");
                query.Append(", Fstop, ExposureTime, ISOSpeed, ExposureBias, FocalLength, MaxAperture");
                query.Append(", MeteringMode, 35mmFocalLength, Latitude, Longitude, Altitude) ");
                query.Append(" VALUES ");

                var width = f.Value[ExifTags.PixelXDimension] is bool ? 0 : f.Value[ExifTags.PixelXDimension];
                var height = f.Value[ExifTags.PixelYDimension] is bool ? 0 : f.Value[ExifTags.PixelYDimension];

                query.Append(string.Format(insertFormat, f.Key, f.Value[ExifTags.DateTimeDigitized], width
                                    , height, f.Value[ExifTags.Make], f.Value[ExifTags.Model], f.Value[ExifTags.FNumber].ToString().Replace(",", ".")
                                    , f.Value[ExifTags.ExposureTime], f.Value[ExifTags.ISOSpeedRatings], f.Value[ExifTags.ExposureBiasValue]
                                    , f.Value[ExifTags.FocalLength], f.Value[ExifTags.MaxApertureValue], f.Value[ExifTags.MeteringMode]
                                    , f.Value[ExifTags.FocalLengthIn35mmFilm], f.Value[ExifTags.GPSLatitude].ToString().Replace(",", ".")
                                    , f.Value[ExifTags.GPSLongitude].ToString().Replace(",", ".")
                                    , f.Value[ExifTags.GPSAltitude].ToString().Replace(",", ".")));
                query.AppendLine(";");
            }

            var commandText = query.ToString().Replace("False", "NULL").Replace(@"\", @"\\").Trim();

            if (string.IsNullOrEmpty(commandText)) return;

            // make connection
            using (var conn = new MySqlConnection(DBConfig.DB_CONN))
            {
                conn.Open();
                // execute query
                using (MySqlCommand comm = new MySqlCommand(commandText, conn))
                {
                    comm.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private Dictionary<ExifTags, object> GetEXIFData(string file)
        {
            var data = new Dictionary<ExifTags, object>() {
                    { ExifTags.DateTimeDigitized, false},
                    { ExifTags.PixelXDimension, false }, // width
                    { ExifTags.PixelYDimension, false}, // height
                    { ExifTags.Make, false },
                    { ExifTags.Model, false },
                    { ExifTags.FNumber, false }, // FStop
                    { ExifTags.ExposureTime, false },
                    { ExifTags.ISOSpeedRatings, false },
                    { ExifTags.ExposureBiasValue, false },
                    { ExifTags.FocalLength, false },
                    { ExifTags.MaxApertureValue, false },
                    { ExifTags.MeteringMode, false },
                    { ExifTags.FocalLengthIn35mmFilm, false },
                    { ExifTags.GPSLatitude, false },
                    { ExifTags.GPSLongitude, false },
                    { ExifTags.GPSAltitude, false }
                };

            try
            {
                using (var reader = new ExifReader(file))
                {
                    var dataDictSize = data.Count;
                    for (int i = 0; i < dataDictSize; i++)
                    {
                        object val;
                        if (reader.GetTagValue(data.ElementAt(i).Key, out val))
                        {
                            if (data.ElementAt(i).Key == ExifTags.DateTimeDigitized)
                            {
                                var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;

                                val = DateTime.ParseExact((string)val, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                                data[data.ElementAt(i).Key] = val;
                            }
                            else if (val is double && data.ElementAt(i).Key != ExifTags.GPSAltitude && data.ElementAt(i).Key != ExifTags.GPSLatitude
                                                   && data.ElementAt(i).Key != ExifTags.GPSLongitude && data.ElementAt(i).Key != ExifTags.FNumber)
                            {
                                int[] rational;
                                if (reader.GetTagValue(data.ElementAt(i).Key, out rational))
                                {
                                    data[data.ElementAt(i).Key] = string.Format("{1}/{2}", data.ElementAt(i).Value, rational[0], rational[1]);
                                }
                            }
                            else if (val is double[])
                            {
                                string strVal = "";
                                foreach (var v in val as double[])
                                {
                                    strVal += v + "; ";
                                }

                                strVal = strVal.Remove(strVal.Length - 2);
                                data[data.ElementAt(i).Key] = strVal;
                            }
                            else if (data.ElementAt(i).Key == ExifTags.FNumber)
                            {
                                data[data.ElementAt(i).Key] = "f/" + val;
                            }
                            else if (data.ElementAt(i).Key == ExifTags.PixelXDimension)
                            {
                                if (val is bool || (val).ToString() == "0")
                                {
                                    int newVal = 0;
                                    reader.GetTagValue(ExifTags.ImageWidth, out newVal);

                                    data[data.ElementAt(i).Key] = newVal;
                                }else
                                {
                                    data[data.ElementAt(i).Key] = val;
                                }
                            }
                            else if (data.ElementAt(i).Key == ExifTags.PixelYDimension)
                            {
                                if (val is bool || (val).ToString() == "0")
                                {
                                    int newVal = 0;
                                    reader.GetTagValue(ExifTags.ImageLength, out newVal);

                                    data[data.ElementAt(i).Key] = newVal;
                                }
                                else
                                {
                                    data[data.ElementAt(i).Key] = val;
                                }
                            }
                            else if (ExifTags.MeteringMode == data.ElementAt(i).Key)
                            {
                                switch ((ushort)val)
                                {
                                    case 0:
                                        data[data.ElementAt(i).Key] = "Unknown";
                                        break;

                                    case 1:
                                        data[data.ElementAt(i).Key] = "Average";
                                        break;

                                    case 2:
                                        data[data.ElementAt(i).Key] = "Center Weighted Average";
                                        break;

                                    case 3:
                                        data[data.ElementAt(i).Key] = "Spot";
                                        break;

                                    case 4:
                                        data[data.ElementAt(i).Key] = "Multi Spot";
                                        break;

                                    case 5:
                                        data[data.ElementAt(i).Key] = "Pattern";
                                        break;

                                    case 6:
                                        data[data.ElementAt(i).Key] = "Partial";
                                        break;

                                    default:
                                        data[data.ElementAt(i).Key] = val;
                                        break;
                                }
                            }
                            else
                            {
                                data[data.ElementAt(i).Key] = val;
                            }
                        }
                    }
                }
            }
            catch
            {
                // there is no exif but we could at least read image dimensions, brute force way
                CheckWidthAndHeight(file, ref data);
            }

            // width and height check once more
            if (data[ExifTags.PixelXDimension] is bool || data[ExifTags.PixelYDimension] is bool
                || "0" == data[ExifTags.PixelXDimension].ToString() || "0" == data[ExifTags.PixelYDimension].ToString())
            {
                CheckWidthAndHeight(file, ref data);
            }

            return data;
        }

        private void CheckWidthAndHeight(string file, ref Dictionary<ExifTags, object> data)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (Image tif = Image.FromStream(stream: fs, useEmbeddedColorManagement: false, validateImageData: false))
                    {
                        long width = (long)tif.PhysicalDimension.Width;
                        long height = (long)tif.PhysicalDimension.Height;

                        // save width
                        data[ExifTags.PixelXDimension] = width;

                        // save height
                        data[ExifTags.PixelYDimension] = height;
                    }
                }
            }
            catch
            {
                // nothing
            }
        }
    }
}