using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Media;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
namespace App5.Class
{
   public class ServiceImage
    {
        /*  public static async Task<MediaFile> TakePicture(bool useCam)
          {
              await CrossMedia.Current.Initialize();

              if (useCam)
              {
                  if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                  {
                      return null;
                  }
              }

              var file = useCam
              ? await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
              {
                  Directory = "Cognitive",
                  Name = "SC_Test.jpg"
              })
              : await CrossMedia.Current.PickPhotoAsync();

              return file;
          }*/


        //*++++++++++++++++++++++++++++++++++++++++++++
        public static async Task<MediaFile> TakePicture(bool useCam)
        {
            await CrossMedia.Current.Initialize();

            if (useCam)
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return null;
                }
            }

            var file = useCam
                ? await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Cognitive",
                    Name = "test.jpg"
                })
                : await CrossMedia.Current.PickPhotoAsync();

            return file;
        }
    }
}
