using ForeverBeYoung.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ForeverBeYoung.ViewModels
{
    public class FileHelper
    {
        public static readonly string[] Document = new string[] { ".doc", ".xls", ".ppt", ".docx", ".xlsx", ".pptx", ".pdf", ".txt", ".rtf" };
        public static readonly string[] Image = new string[] { ".jpg", ".png", ".bmp", ".gif", ".tif", ".PNG", };
        public static readonly string[] Music = new string[] { ".mp3", ".wma", ".m4a", ".aac" };

        public static List<string> FileTypes()
        {
            return new List<string> { ".png", ".jpg", "bmp", ".gif", "tif", ".mp3", ".mp4", ".m4a" };
        }

        public static async Task<IReadOnlyList<StorageFile>> GetFilesAsync()
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            folderPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            var folder = await folderPicker.PickSingleFolderAsync();
            IReadOnlyList<StorageFile> files;
            if (folder != null)
            {
                files = await folder.GetFilesAsync();
                return files;
            }
            return null;
        }

        public static async Task<ObservableCollection<PhotoModel>> CreatePhotoModel(IReadOnlyList<StorageFile> files)
        {
            ObservableCollection<PhotoModel> collection = new ObservableCollection<PhotoModel>();
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (FileHelper.Image.Contains(file.FileType))
                    {
                        using (var fileStream = await file.OpenAsync(FileAccessMode.Read))
                        {
                            BitmapImage originBT = new BitmapImage();
                            await originBT.SetSourceAsync(fileStream);

                            var thumbnail = await file.GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.PicturesView);
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.SetSource(thumbnail);
                            var dateTime = file.DateCreated.DateTime;
                            var photo = new PhotoModel() { ImaSource = bitmap, ImageInfo = file.DisplayName, DateTime = dateTime.ToString(), OriginalImage = originBT };
                            collection.Add(photo);
                        }


                    }
                }
            }
            return collection;
        }
    }
}
