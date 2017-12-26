using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App7.Droid;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(SaveAndLoad))]
namespace App7.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        //we can only acess the ExternalStorageDirectory not the appdata of android 
        //special folder is not working so we end up acessing the ExternalStorageDirectory aka still the phone then plus the path to the files folder

        public void SaveText(string filename, string text)
        {
            var Packagename = Application.Context.PackageName;
            var documentsPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Android/data/"+ Packagename+"/files";
   
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            Console.WriteLine("-------------"+ documentsPath);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            var Packagename = Application.Context.PackageName;
            var documentsPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Android/data/" + Packagename + "/files";

            var filePath = System.IO.Path.Combine(documentsPath, filename);
            Console.WriteLine("-------------" + documentsPath);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}