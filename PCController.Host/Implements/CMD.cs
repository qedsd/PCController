using PCController.Core.MsgParameter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Host.Implements
{
    internal static class CMD
    {
        public static void ExcuteBat(object para) => ExcuteBat(para as BatParameter);
        public static void ExcuteBat(BatParameter parameter)
        {
            if(parameter != null)
            {
                ExcuteBat(parameter.BatPath, parameter.UseShellExecute, parameter.CreateNoWindow, parameter.Admin);
            }
        }
        public static void ExcuteBat(string path, bool shell, bool nowindow, bool admin)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = path;
            p.StartInfo.UseShellExecute = shell;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = false;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = false;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = false;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = nowindow;//不显示程序窗口
            if(admin)
            {
                p.StartInfo.Verb = "runas";
            }
            p.Start();//启动程序
            p.WaitForExit();
            p.Close();
        }
        public static List<string> GetBatList()
        {
            string folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CMD");
            if(System.IO.Directory.Exists(folder))
            {
                var files = System.IO.Directory.GetFiles(folder);
                if(files != null)
                {
                    //返回文件名
                    return files.Where(p=>p.EndsWith(".bat", StringComparison.OrdinalIgnoreCase)).Select(p=>System.IO.Path.GetFileNameWithoutExtension(p)).ToList();
                }
            }
            return null;
        }
        public static string ExcuteCMD(string cmd)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            //p.StartInfo.Verb = "runas";
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(cmd);

            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return output;
        }
        public static string ExcuteCMD(object para) => ExcuteCMD(para as CMDParameter);
        public static string ExcuteCMD(CMDParameter parameter)
        {
            if(parameter == null)
            {
                return null;
            }
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = parameter.FileName;
            p.StartInfo.UseShellExecute = parameter.UseShellExecute;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = parameter.RedirectStandardInput;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = parameter.RedirectStandardOutput;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = parameter.RedirectStandardError;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = parameter.CreateNoWindow;//不显示程序窗口
            if(parameter.Admin)
            {
                p.StartInfo.Verb = "runas";
            }
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            if(!string.IsNullOrEmpty(parameter.CMD))
            {
                p.StandardInput.WriteLine(parameter.CMD);
            }

            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("&exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令


            string output = null;
            //获取cmd窗口的输出信息
            if (p.StartInfo.RedirectStandardInput)
            {
                output = p.StandardOutput.ReadToEnd();
            }
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return output;
        }
    }
}
