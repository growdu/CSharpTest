using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
 
namespace CSharpCallCPlusPlus
{
    class Program
    {
        [DllImport(@"../../../Debug/CppTester.dll", EntryPoint = "UpdateCameraSetting", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static void UpdateCameraSetting(ref Camera camera, ref CameraSetting cameraSetting);

        [DllImport(@"../../../Debug/CppTester.dll", EntryPoint = "GetCameraImageData", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static int GetCameraImageData(ref CameraImageData cameraImageData);

        static void Main(string[] args)
        {
            Camera camera1 = new CSharpCallCPlusPlus.Program.Camera();
            camera1.CameraId = 1;
            camera1.CameraName = "MyCamera001";

            CameraSetting cameraSetting1 = new CameraSetting();
            cameraSetting1.Setting1 = 10;
            cameraSetting1.Setting2 = true;
            cameraSetting1.Setting3 = "MySetting001";

            UpdateCameraSetting(ref camera1, ref cameraSetting1);

            CameraImageData cameraImageData = new CameraImageData();
            GetCameraImageData(ref cameraImageData);

        }


        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
        public struct Camera
        {
            public int CameraId;
            public CameraSetting Setting;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string CameraName;
        }

        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
        public struct CameraSetting
        {
            public int Setting1;
            public bool Setting2;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string Setting3;
        }

        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
        public struct CameraImageData
        {
            public int CameraId;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string CameraName;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValArray, SizeConst = 50000)]
            public byte[] Image;
            public CameraSetting Setting;
        };

    }
}
