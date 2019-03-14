#pragma once

// 包括 SDKDDKVer.h 将定义可用的最高版本的 Windows 平台。

// 如果要为以前的 Windows 平台生成应用程序，请包括 WinSDKVer.h，并
// 将 _WIN32_WINNT 宏设置为要支持的平台，然后再包括 SDKDDKVer.h。

#include <SDKDDKVer.h>

//相机设置
typedef struct CameraSetting
{
	int Setting1;
	bool Setting2;
	char Setting3[20];
};

//相机
typedef struct Camera
{
	int CameraId;
	char CameraName[20];
	CameraSetting Setting;
};

//相机图片
typedef struct CameraImageData
{
public:
	int CameraId;
	char CameraName[20];
	BYTE Image[50000];
	CameraSetting Setting;
};


//更新相机设置
extern "C" _declspec(dllexport)  void __stdcall UpdateCameraSetting(Camera& camera, CameraSetting& cameraSetting);
//获取相机传回的图片
extern "C" _declspec(dllexport)  int __stdcall GetCameraImageData(CameraImageData* cameraImageData);