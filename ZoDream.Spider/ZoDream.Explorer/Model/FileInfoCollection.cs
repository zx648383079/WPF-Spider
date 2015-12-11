using System.Collections.ObjectModel;
using System.IO;
using ZoDream.Explorer.Helper;

namespace ZoDream.Explorer.Model
{
    /// <summary>
    /// 文件信息类
    /// </summary>
    public class FileInfoCollection
    {
        private string _name;
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private FileKinds _kind;
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileKinds Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        private string _path;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private bool _isExpanded = false;
        /// <summary>
        /// 控制展开
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set {
                if(value && !isGetGrandChildren)
                {
                    _getGrandChildren();
                    isGetGrandChildren = true;
                }
                _isExpanded = value;
            }
        }

        private bool isGetGrandChildren = false;
        /// <summary>
        /// 展开时获取孙代
        /// </summary>
        private void _getGrandChildren()
        {
            if (Children != null)
            {
                for (int i = Children.Count - 1; i >= 0; i--)
                {
                    Children[i].Children = FileDeal.GetDir(Children[i].Path);
                }
            }
        }


        private ObservableCollection<FileInfoCollection> _children;
        /// <summary>
        /// 所有的孩子
        /// </summary>
        public ObservableCollection<FileInfoCollection> Children
        {
            get { return _children; }
            set { _children = value; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public FileInfoCollection()
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path"></param>
        public FileInfoCollection(string path)
        {
            if (File.Exists(path))
            {
                Kind = FileKinds.FILE;
            } else
            {
                Kind = FileKinds.DIR;
            }

            Name = System.IO.Path.GetFileName(path);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="kind"></param>
        /// <param name="path"></param>
        public FileInfoCollection(string name, FileKinds kind, string path)
        {
            Name = name;
            Kind = kind;
            Path = path;
        }
    }
}
