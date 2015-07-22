using System;

// ReSharper disable InconsistentNaming

namespace WinApiWrapper.Unsafe
{
    public static partial class NativeMethods
    {
        public class Constants
        {
            public const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
            public const uint STANDARD_RIGHTS_READ = 0x00020000;


            // Window Styles
            public const uint WS_OVERLAPPED = 0;
            public const uint WS_POPUP = 0x80000000;
            public const uint WS_CHILD = 0x40000000;
            public const uint WS_MINIMIZE = 0x20000000;
            public const uint WS_VISIBLE = 0x10000000;
            public const uint WS_DISABLED = 0x8000000;
            public const uint WS_CLIPSIBLINGS = 0x4000000;
            public const uint WS_CLIPCHILDREN = 0x2000000;
            public const uint WS_MAXIMIZE = 0x1000000;
            public const uint WS_CAPTION = 0xC00000; // WS_BORDER or WS_DLGFRAME  
            public const uint WS_BORDER = 0x800000;
            public const uint WS_DLGFRAME = 0x400000;
            public const uint WS_VSCROLL = 0x200000;
            public const uint WS_HSCROLL = 0x100000;
            public const uint WS_SYSMENU = 0x80000;
            public const uint WS_THICKFRAME = 0x40000;
            public const uint WS_GROUP = 0x20000;
            public const uint WS_TABSTOP = 0x10000;
            public const uint WS_MINIMIZEBOX = 0x20000;
            public const uint WS_MAXIMIZEBOX = 0x10000;
            public const uint WS_TILED = WS_OVERLAPPED;
            public const uint WS_ICONIC = WS_MINIMIZE;
            public const uint WS_SIZEBOX = WS_THICKFRAME;

            // Extended Window Styles
            public const uint WS_EX_DLGMODALFRAME = 0x0001;
            public const uint WS_EX_NOPARENTNOTIFY = 0x0004;
            public const uint WS_EX_TOPMOST = 0x0008;
            public const uint WS_EX_ACCEPTFILES = 0x0010;
            public const uint WS_EX_TRANSPARENT = 0x0020;
            public const uint WS_EX_MDICHILD = 0x0040;
            public const uint WS_EX_TOOLWINDOW = 0x0080;
            public const uint WS_EX_WINDOWEDGE = 0x0100;
            public const uint WS_EX_CLIENTEDGE = 0x0200;
            public const uint WS_EX_CONTEXTHELP = 0x0400;
            public const uint WS_EX_RIGHT = 0x1000;
            public const uint WS_EX_LEFT = 0x0000;
            public const uint WS_EX_RTLREADING = 0x2000;
            public const uint WS_EX_LTRREADING = 0x0000;
            public const uint WS_EX_LEFTSCROLLBAR = 0x4000;
            public const uint WS_EX_RIGHTSCROLLBAR = 0x0000;
            public const uint WS_EX_CONTROLPARENT = 0x10000;
            public const uint WS_EX_STATICEDGE = 0x20000;
            public const uint WS_EX_APPWINDOW = 0x40000;
            public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
            public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
            public const uint WS_EX_LAYERED = 0x00080000;
            public const uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
            public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
            public const uint WS_EX_COMPOSITED = 0x02000000;
            public const uint WS_EX_NOACTIVATE = 0x08000000;

            // Windows Messages
            public const uint WM_GETTEXT = 0x000D;
            public const uint WM_SETTEXT = 0x000C;


            public const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
            public const uint SE_PRIVILEGE_ENABLED = 0x00000002;
            public const uint SE_PRIVILEGE_REMOVED = 0x00000004;
            public const uint SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

            public const Int32 ANYSIZE_ARRAY = 1;

            public const uint FILE_SHARE_READ = 0x00000001;
            public const uint FILE_SHARE_WRITE = 0x00000002;
            public const uint FILE_SHARE_DELETE = 0x00000004;

            public const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
            public const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
            public const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
            public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
            public const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
            public const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
            public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
            public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
            public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
            public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
            public const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
            public const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
            public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
            public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;

            public const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
            public const string SE_AUDIT_NAME = "SeAuditPrivilege";
            public const string SE_BACKUP_NAME = "SeBackupPrivilege";
            public const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
            public const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
            public const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
            public const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
            public const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
            public const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
            public const string SE_DEBUG_NAME = "SeDebugPrivilege";
            public const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
            public const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
            public const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
            public const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
            public const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
            public const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
            public const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
            public const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
            public const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
            public const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
            public const string SE_RELABEL_NAME = "SeRelabelPrivilege";
            public const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
            public const string SE_RESTORE_NAME = "SeRestorePrivilege";
            public const string SE_SECURITY_NAME = "SeSecurityPrivilege";
            public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            public const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
            public const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
            public const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
            public const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
            public const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
            public const string SE_TCB_NAME = "SeTcbPrivilege";
            public const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
            public const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
            public const string SE_UNDOCK_NAME = "SeUndockPrivilege";
            public const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";

            public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
            public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
            public static readonly IntPtr HWND_TOP = new IntPtr(0);
            public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

            public const Int32 CURSOR_SHOWING = 0x00000001;
        }
    }
}
