/*
  <auto-generated/>
  C# bindings for Simple DirectMedia Layer.
  Original copyright notice of input files:

  Simple DirectMedia Layer
  Copyright (C) 1997-2024 Sam Lantinga <slouken@libsdl.org>

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.
*/

using System.Runtime.InteropServices;

namespace SDL
{
    public partial struct SDL_Camera
    {
    }

    public partial struct SDL_CameraSpec
    {
        public SDL_PixelFormatEnum format;

        public int width;

        public int height;

        public int interval_numerator;

        public int interval_denominator;
    }

    public enum SDL_CameraPosition
    {
        SDL_CAMERA_POSITION_UNKNOWN,
        SDL_CAMERA_POSITION_FRONT_FACING,
        SDL_CAMERA_POSITION_BACK_FACING,
    }

    public static unsafe partial class SDL3
    {
        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_GetNumCameraDrivers();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern byte* SDL_GetCameraDriver(int index);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern byte* SDL_GetCurrentCameraDriver();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("SDL_CameraDeviceID *")]
        public static extern uint* SDL_GetCameraDevices(int* count);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_CameraSpec* SDL_GetCameraDeviceSupportedFormats([NativeTypeName("SDL_CameraDeviceID")] uint devid, int* count);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* SDL_GetCameraDeviceName([NativeTypeName("SDL_CameraDeviceID")] uint instance_id);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_CameraPosition SDL_GetCameraDevicePosition([NativeTypeName("SDL_CameraDeviceID")] uint instance_id);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_Camera* SDL_OpenCameraDevice([NativeTypeName("SDL_CameraDeviceID")] uint instance_id, [NativeTypeName("const SDL_CameraSpec *")] SDL_CameraSpec* spec);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_GetCameraPermissionState(SDL_Camera* camera);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("SDL_CameraDeviceID")]
        public static extern uint SDL_GetCameraInstanceID(SDL_Camera* camera);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("SDL_PropertiesID")]
        public static extern uint SDL_GetCameraProperties(SDL_Camera* camera);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_GetCameraFormat(SDL_Camera* camera, SDL_CameraSpec* spec);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_Surface* SDL_AcquireCameraFrame(SDL_Camera* camera, [NativeTypeName("Uint64 *")] ulong* timestampNS);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_ReleaseCameraFrame(SDL_Camera* camera, SDL_Surface* frame);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SDL_CloseCamera(SDL_Camera* camera);
    }
}