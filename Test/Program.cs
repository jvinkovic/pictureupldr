using ExifLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DateTime dt = DateTime.ParseExact("2010:12:09 03:00:22", "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);

            /*
            var files = new DirectoryInfo(@"C:\Users\jvinkovic.SPAN\Desktop\picstest").GetFiles().Select(f => f.FullName);

            foreach (var file in files)
            {
                Console.WriteLine(file);
                var data = GetEXIFData(file);
                foreach (var d in data)
                {
                    Console.WriteLine(Enum.GetName(typeof(ExifTags), d.Key) + " - " + d.Value);
                }
                Console.WriteLine();
            }
            */

            Console.ReadLine();
        }

        private static Dictionary<ExifTags, object> GetEXIFData(string file)
        {
            var data = new Dictionary<ExifTags, object>() {
                    { ExifTags.DateTimeDigitized, false},
                    { ExifTags.PixelXDimension, false },
                    { ExifTags.PixelYDimension, false},
                    { ExifTags.Make, false },
                    { ExifTags.Model, false },
                    { ExifTags.FNumber, false },
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
                            if (val is double)
                            {
                                int[] rational;
                                if (reader.GetTagValue(data.ElementAt(i).Key, out rational))
                                {
                                    data[data.ElementAt(i).Key] = string.Format("{1}/{2}", data.ElementAt(i).Value, rational[0], rational[1]);
                                }
                            }
                            else
                            {
                                if (ExifTags.MeteringMode == data.ElementAt(i).Key)
                                {
                                    switch ((int)data.ElementAt(i).Value)
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
            }
            catch
            {
                // ništa, sve je null...
            }

            return data;
        }
    }
}