using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7
{
  public  class ReadWrite 
    {
       public async Task StoragePermisionWrite(string FileName, string FileContent)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await Application.Current.MainPage.DisplayAlert("Need File", "Gunna need that File", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                    {
                        status = results[Permission.Storage];
                    }

                }

                if (status == PermissionStatus.Granted)
                {
                    Debug.WriteLine("Granted Storage");
                    DependencyService.Get<ISaveAndLoad>().SaveText(FileName, FileContent);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await Application.Current.MainPage.DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
        }
       public async Task<string> StoragePermisionRead(string FileName)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await Application.Current.MainPage.DisplayAlert("Need File", "Gunna need that File", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                    {
                        status = results[Permission.Storage];
                    }

                }

                if (status == PermissionStatus.Granted)
                {
                    Debug.WriteLine("Granted Storage");
                   var x= DependencyService.Get<ISaveAndLoad>().LoadText(FileName);
                    return x;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await Application.Current.MainPage.DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                 
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
            return "";
        }
    }
}
