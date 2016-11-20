using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using XlRowCol = Microsoft.Office.Interop.Excel.XlRowCol;

namespace ZoDream.Helper.Local
{
    class ExcelHelper
    {
        public string MFilename;
        public Application App;
        public Workbooks Wbs;
        public Workbook Wb;
        public Worksheets Wss;
        public Worksheet Ws;

        public void Create()//创建一个Microsoft.Office.Interop.Excel对象
        {
            App = new Application();
            Wbs = App.Workbooks;
            Wb = Wbs.Add(true);
        }
        public void Open(string fileName)//打开一个Microsoft.Office.Interop.Excel文件
        {
            App = new Application();
            Wbs = App.Workbooks;
            Wb = Wbs.Add(fileName);
            //wb = wbs.Open(FileName, 0, true, 5,"", "", true, XlPlatform.xlWindows, "t", false, false, 0, true,Type.Missing,Type.Missing);
            //wb = wbs.Open(FileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,XlPlatform.xlWindows,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            MFilename = fileName;
        }
        public Worksheet GetSheet(string sheetName)
        //获取一个工作表
        {
            var s = (Worksheet)Wb.Worksheets[sheetName];
            return s;
        }
        public Worksheet AddSheet(string sheetName)
        //添加一个工作表
        {
            Worksheet s = (Worksheet)Wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = sheetName;
            return s;
        }

        public void DelSheet(string sheetName)//删除一个工作表
        {
            ((Worksheet)Wb.Worksheets[sheetName]).Delete();
        }
        public Worksheet ReNameSheet(string oldSheetName, string newSheetName)//重命名一个工作表一
        {
            var s = (Worksheet)Wb.Worksheets[oldSheetName];
            s.Name = newSheetName;
            return s;
        }

        public Worksheet ReNameSheet(Worksheet sheet, string newSheetName)//重命名一个工作表二
        {

            sheet.Name = newSheetName;

            return sheet;
        }

        public void SetCellValue(Worksheet ws, int x, int y, object value)
        //ws：要设值的工作表     X行Y列     value   值
        {
            ws.Cells[x, y] = value;
        }
        public void SetCellValue(string ws, int x, int y, object value)
        //ws：要设值的工作表的名称 X行Y列 value 值
        {

            GetSheet(ws).Cells[x, y] = value;
        }

        public void SetCellProperty(Worksheet ws, int startx, int starty, int endx, int endy, int size, string name, Constants color, Constants horizontalAlignment)
        //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            if (!Enum.IsDefined(typeof(Constants), color))
                throw new InvalidEnumArgumentException(nameof(color), (int) color, typeof(Constants));
            if (!Enum.IsDefined(typeof(Constants), horizontalAlignment))
                throw new InvalidEnumArgumentException(nameof(horizontalAlignment), (int) horizontalAlignment,
                    typeof(Constants));
            name = "宋体";
            size = 12;
            color = Constants.xlAutomatic;
            horizontalAlignment = Constants.xlRight;
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].Font.Name = name;
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].Font.Size = size;
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].Font.Color = color;
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].HorizontalAlignment = horizontalAlignment;
        }

        public void SetCellProperty(string wsn, int startx, int starty, int endx, int endy, int size, string name, Constants color, Constants horizontalAlignment)
        {
            //name = "宋体";
            //size = 12;
            //color = Constants.xlAutomatic;
            //HorizontalAlignment = Constants.xlRight;

            var ws = GetSheet(wsn);
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].Font.Name = name;
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].Font.Size = size;
            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].Font.Color = color;

            ws.Range[ws.Cells[startx, starty], ws.Cells[endx, endy]].HorizontalAlignment = horizontalAlignment;
        }


        public void UniteCells(Worksheet ws, int x1, int y1, int x2, int y2)
        //合并单元格
        {
            ws.Range[ws.Cells[x1, y1], ws.Cells[x2, y2]].Merge(Type.Missing);
        }

        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        //合并单元格
        {
            GetSheet(ws).Range[GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]].Merge(Type.Missing);

        }


        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();

                }

            }

        }
        public void InsertTable(System.Data.DataTable dt, Worksheet ws, int startX, int startY)
        //将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    ws.Cells[startX + i, j + startY] = dt.Rows[i][j];

                }

            }

        }


        public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置一
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];

                }

            }

        }
        public void AddTable(System.Data.DataTable dt, Worksheet ws, int startX, int startY)
        //将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        {


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j];

                }
            }

        }
        public void InsertPictures(string filename, string ws)
        //插入图片操作一
        {
            GetSheet(ws).Shapes.AddPicture(filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
            //后面的数字表示位置
        }

        //public void InsertPictures(string Filename, string ws, int Height, int Width)
        //插入图片操作二
        //{
        //    GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
        //}
        //public void InsertPictures(string Filename, string ws, int left, int top, int Height, int Width)
        //插入图片操作三
        //{

        //    GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementLeft(left);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementTop(top);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
        //}

        public void InsertActiveChart(Microsoft.Office.Interop.Excel.XlChartType chartType, string ws, int dataSourcesX1, int dataSourcesY1, int dataSourcesX2, int dataSourcesY2, XlRowCol chartDataType)
        //插入图表操作
        {
            if (!Enum.IsDefined(typeof(XlRowCol), chartDataType))
                throw new InvalidEnumArgumentException(nameof(chartDataType), (int) chartDataType, typeof(XlRowCol));
            chartDataType = XlRowCol.xlColumns;
            Wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            {
                Wb.ActiveChart.ChartType = chartType;
                Wb.ActiveChart.SetSourceData(GetSheet(ws).Range[GetSheet(ws).Cells[dataSourcesX1, dataSourcesY1], GetSheet(ws).Cells[dataSourcesX2, dataSourcesY2]], chartDataType);
                Wb.ActiveChart.Location(XlChartLocation.xlLocationAsObject, ws);
            }
        }
        public bool Save()
        //保存文档
        {
            if (MFilename == "")
            {
                return false;
            }
            try
            {
                Wb.Save();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        public bool SaveAs(object fileName)
        //文档另存为
        {
            try
            {
                Wb.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;

            }

            catch (Exception)
            {
                return false;

            }
        }
        public void Close()
        //关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        {
            //wb.Save();
            Wb.Close(Type.Missing, Type.Missing, Type.Missing);
            Wbs.Close();
            App.Quit();
            Wb = null;
            Wbs = null;
            App = null;
            GC.Collect();
        }
    }
}
