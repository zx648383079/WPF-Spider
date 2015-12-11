using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ZoDream.Explorer.Model;

namespace ZoDream.Explorer.Helper
{
    /// <summary>
    /// 文件处理
    /// </summary>
    public class FileDeal
    {
        /// <summary>
        /// 获取根目录
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ObservableCollection<FileInfoCollection> GetRoot(ref ObservableCollection<FileInfoCollection> args)
        {
            DriveInfo[] infos = DriveInfo.GetDrives();
            foreach (DriveInfo item in infos)
            {
                if (item.DriveType == DriveType.Fixed || item.DriveType == DriveType.Removable)
                {
                    string name = item.Name;
                    args.Add(new FileInfoCollection(string.Format("{0}({1})", item.VolumeLabel, name.TrimEnd('\\')), FileKinds.DIR, name));
                }
            }

            return args;
        }

        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ObservableCollection<FileInfoCollection> GetDir(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            string[] paths = new string[] { };
            try
            {
                paths = Directory.GetDirectories(path);
            }
            catch (System.Exception)
            {
                return null;
            }
            ObservableCollection<FileInfoCollection> files = new ObservableCollection<FileInfoCollection>();
            foreach (string item in paths)
            {
                FileInfoCollection info = new FileInfoCollection(Path.GetFileName(item), FileKinds.DIR, item);
                //info.Children = GetDir(item);
                files.Add(info);
            }
            return files;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFile(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            try
            {
                return Directory.GetFiles(path);
            }
            catch (System.Exception)
            {

                return null;
            }
        }
    }
}
