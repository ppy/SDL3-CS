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

using System;
using System.Runtime.InteropServices;

namespace SDL
{
    public enum SDL_Folder
    {
        SDL_FOLDER_HOME,
        SDL_FOLDER_DESKTOP,
        SDL_FOLDER_DOCUMENTS,
        SDL_FOLDER_DOWNLOADS,
        SDL_FOLDER_MUSIC,
        SDL_FOLDER_PICTURES,
        SDL_FOLDER_PUBLICSHARE,
        SDL_FOLDER_SAVEDGAMES,
        SDL_FOLDER_SCREENSHOTS,
        SDL_FOLDER_TEMPLATES,
        SDL_FOLDER_VIDEOS,
    }

    public enum SDL_PathType
    {
        SDL_PATHTYPE_NONE,
        SDL_PATHTYPE_FILE,
        SDL_PATHTYPE_DIRECTORY,
        SDL_PATHTYPE_OTHER,
    }

    public partial struct SDL_PathInfo
    {
        public SDL_PathType type;

        [NativeTypeName("Uint64")]
        public ulong size;

        [NativeTypeName("SDL_Time")]
        public long create_time;

        [NativeTypeName("SDL_Time")]
        public long modify_time;

        [NativeTypeName("SDL_Time")]
        public long access_time;
    }

    public static unsafe partial class SDL3
    {
        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* SDL_GetBasePath();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* SDL_GetPrefPath([NativeTypeName("const char *")] byte* org, [NativeTypeName("const char *")] byte* app);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* SDL_GetUserFolder(SDL_Folder folder);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_CreateDirectory([NativeTypeName("const char *")] byte* path);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_EnumerateDirectory([NativeTypeName("const char *")] byte* path, [NativeTypeName("SDL_EnumerateDirectoryCallback")] delegate* unmanaged[Cdecl]<IntPtr, byte*, byte*, int> callback, [NativeTypeName("void*")] IntPtr userdata);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_RemovePath([NativeTypeName("const char *")] byte* path);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_RenamePath([NativeTypeName("const char *")] byte* oldpath, [NativeTypeName("const char *")] byte* newpath);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_GetPathInfo([NativeTypeName("const char *")] byte* path, SDL_PathInfo* info);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char **")]
        public static extern byte** SDL_GlobDirectory([NativeTypeName("const char *")] byte* path, [NativeTypeName("const char *")] byte* pattern, [NativeTypeName("Uint32")] uint flags, int* count);

        [NativeTypeName("#define SDL_GLOB_CASEINSENSITIVE (1 << 0)")]
        public const int SDL_GLOB_CASEINSENSITIVE = (1 << 0);
    }
}