// CallCPlus.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

void __stdcall UpdateCameraSetting(Camera& camera, CameraSetting& cameraSetting)
{
	Camera currentCamera = camera;
	currentCamera.Setting = cameraSetting;
}


int __stdcall GetCameraImageData(CameraImageData* cameraImageData)
{

	CameraImageData* currentData = cameraImageData;
	currentData->CameraId = 3;

	strcpy(currentData->CameraName, "camera3");

	currentData->Image[0] = 3;
	currentData->Image[1] = 3;
	currentData->Image[2] = 3;

	return 0;
}



